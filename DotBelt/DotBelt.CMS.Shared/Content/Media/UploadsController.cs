using System.Text.Json;
using System.Text.RegularExpressions;
using DotBelt.CMS.Shared.Tenants;
using MetadataExtractor;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    
    public async Task DeleteUploadsAsync(List<int> uploadsToDelete, CancellationToken cancellationToken = default) 
    {
        foreach (var uploadId in uploadsToDelete)
        {
            var upload = await _context
                .Uploads
                .FirstOrDefaultAsync(x => x.Id == uploadId, cancellationToken);
          

            if (upload != null)
            {
                var fullPath = GetFullPath(upload.FileName);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                _context.Uploads.Remove(upload);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
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

    public static string ExtractMetadata(Stream fileStream)
    {
        var directories = ImageMetadataReader.ReadMetadata(fileStream);
        var metadata = new Dictionary<string, string?>();

        foreach (var directory in directories)
        {
            foreach (var tag in directory.Tags)
            {
                string key = SanitizeString($"{directory.Name}:{tag.Name}");
                string? value = tag.Description != null ? SanitizeString(tag.Description) : null;
                metadata[key] = value;
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
            Children = new List<Upload>(),
            Author = null!,
            PublishDate = DateTimeOffset.UtcNow,
            MimeType = mime,
        };

        await using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
            
            stream.Seek(0, SeekOrigin.Begin);
            
            if (Mimes.IsImage(mime))
            {
                var metaData = ExtractMetadata(stream);
                upload.MetaData = metaData;

                foreach (var cropSetting in CropsSettings.Internal)
                {
                    try
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        var cropFileName = CropProcessor.CropImage(
                            stream, 
                            fileName, 
                            cropSetting.Width, 
                            cropSetting.Height, false, CropPositionX.Left, CropPositionY.Top);

                        var crop = new Upload()
                        {
                            CropName = cropSetting.Name,
                            Author = null!,
                            AuthorId = authorId,
                            FileName = cropFileName,
                            MimeType = Mimes.WEBP,
                            PublishDate = upload.PublishDate,
                            RelativeUrl = GetRelativePath(cropFileName),
                            TenantId = tenantId,
                            Tenant = null!
                        };
                        
                        upload.Children.Add(crop);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
               
                }
            }
        }


        return upload;
    }
}