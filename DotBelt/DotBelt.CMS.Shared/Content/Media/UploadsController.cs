using System.Text.Json;
using System.Text.RegularExpressions;
using DotBelt.CMS.Shared.Tenants;
using MetadataExtractor;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using Directory = System.IO.Directory;
using Path = System.IO.Path;

namespace DotBelt.CMS.Shared.CMS.Media;

[RegisterScoped]
public class UploadsController
{
    private readonly ApplicationDbContext _context;
    private readonly TenantController _tenantController;

    public UploadsController(IDbContextFactory<
            ApplicationDbContext> contextFactory,
        TenantController tenantController
    )
    {
        _context = contextFactory.CreateDbContext();
        _tenantController = tenantController;
    }
    
    public async Task<RemoveUploadsResult> DeleteUploadsAsync(List<int> uploadsToDelete, CancellationToken cancellationToken = default) 
    {
        foreach (var uploadId in uploadsToDelete)
        {
            var upload = await _context
                .Uploads
                .Include(x => x.Thumbnails)
                .FirstOrDefaultAsync(x => x.Id == uploadId, cancellationToken);
          

            if (upload != null)
            {
                var fullPath = GetFullPath(upload.FileName);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                if (upload.Thumbnails != null)
                {
                    _context.Thumbnails.RemoveRange();
                }

                _context.Uploads.Remove(upload);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        return new RemoveUploadsResult(uploadsToDelete);
    }

    public async Task CreateUploadsAsync(IFormFileCollection files, int? userId,
        CancellationToken cancellationToken = default)
    {
        if (userId == null) return;

        var result = new List<Upload>(files.Count);

        foreach (var file in files)
        {
            var upload = await CreateUploadAsync(file, userId.Value, cancellationToken);

            if (upload == null) continue;

            result.Add(upload);

            _context.Uploads.Add(upload);
        }

        await _context.SaveChangesAsync(cancellationToken);

    }

    public string SanitizeFileName(string fileName)
    {
        return fileName.Replace(" ", "_");
    }

    public static string GetRelativePath(string fileName)
    {
        return Path.Combine("uploads", fileName);
    }

    public static string GetFullPath(string fileName)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", GetRelativePath(fileName));
    }

    private string GetFinalFilename(string fileName)
    {
        fileName = SanitizeFileName(fileName);
        var filePath = GetFullPath(fileName);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var extension = Path.GetExtension(fileName);
        var i = 2;

        while (File.Exists(filePath))
        {
            fileName = $"{fileNameWithoutExtension}({i}){extension}";
            filePath = GetFullPath(fileName);
            i++;
        }

        return fileName;
    }
    private static string SanitizeString(string input)
    {
        return input.Replace("\u0000", string.Empty);
    }

    public static string ExtractAdditionalMetadata(Stream fileStream, Dictionary<string, object> metadata)
    {
        var directories = ImageMetadataReader.ReadMetadata(fileStream);

        foreach (var directory in directories)
        {
            foreach (var tag in directory.Tags)
            {
                string key = SanitizeString($"{directory.Name}:{tag.Name}");
                string? value = tag.Description != null ? SanitizeString(tag.Description) : null;
                if (value != null)
                {
                    metadata[key] = value;
                }
            }
        }

        return JsonSerializer.Serialize(metadata);
    }

    public async Task<Upload?> CreateUploadAsync(IFormFile file, int authorId, CancellationToken cancellationToken)
    {
        if (file.Length == 0) return null;

        var allowedMimeTypes = await _tenantController.GetAllowedFileTypes(cancellationToken);

        if (allowedMimeTypes == null || allowedMimeTypes.Length == 0) return null;

        var mime = file.ContentType;

        if (!allowedMimeTypes.Contains(mime)) return null;

        var tenantId = await _context
            .Tenants
            .AsNoTracking()
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (tenantId == 0)
        {
            throw new InvalidOperationException("No tenant found");
        }

        var fileName = GetFinalFilename(file.FileName);

        var fullPath = GetFullPath(fileName);

        var upload = new Upload()
        {
            TenantId = tenantId,
            Tenant = null!,
            FileName = fileName,
            RelativeUrl = GetRelativePath(fileName),
            AuthorId = authorId,
            Thumbnails = new List<Thumbnail>(),
            Author = null!,
            MetaData = string.Empty,
            PublishDate = DateTimeOffset.UtcNow,
            MimeType = mime,
        };

        var imageMetaData = new Dictionary<string, object>();

        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
            
            stream.Seek(0, SeekOrigin.Begin);
            
 
            if (Mimes.IsImage(mime))
            {
                
                // Get image dimensions and size using SkiaSharp
                using (var skStream = new SKManagedStream(stream))
                {
                    using (var skImage = SKBitmap.Decode(skStream))
                    {
                        int width = skImage.Width;
                        int height = skImage.Height;
                        long size = stream.Length; // Size in bytes

                        imageMetaData["Width"] = width;
                        imageMetaData["Height"] = height;
                        imageMetaData["Size"] = size;
                
                        stream.Position = 0; 
                        
                        ExtractAdditionalMetadata(stream, imageMetaData);

                        upload.MetaData = JsonSerializer.Serialize(imageMetaData);
                        
                        foreach (var cropSetting in _context.Crops)
                        {
                            try
                            {
                                var cropFileName = CropProcessor.CropImage(
                                    skImage, 
                                    fileName, 
                                    cropSetting.Width, 
                                    cropSetting.Height, 
                                    false, 
                                    cropSetting.CropPositionX,
                                    cropSetting.CropPositionY);

                                var crop = new Thumbnail()
                                {
                                    Name = cropSetting.Name,
         
                                    Upload = upload,
                                    FileName = cropFileName,
                                    MimeType = Mimes.WEBP,
                                    PublishDate = upload.PublishDate,
                                    RelativeUrl = GetRelativePath(cropFileName),
                   
                                };
                        
                                upload.Thumbnails.Add(crop);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
               
                        }
                        
                    }
                }
             
            }
        }


        return upload;
    }
}