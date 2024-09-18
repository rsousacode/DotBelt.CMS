using System.Text.Json;
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

    public async Task<List<Upload>> CreateUploadsAsync(IFormFileCollection files, int? userId,
        CancellationToken cancellationToken = default)
    {
        if (userId == null) return new List<Upload>();

        var result = new List<Upload>(files.Count);

        foreach (var file in files)
        {
            var upload = await CreateUploadAsync(file, userId.Value, cancellationToken);

            if (upload == null) continue;

            result.Add(upload);

            _context.Uploads.Add(upload);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return result;
    }

    public string SanitizeFileName(string fileName)
    {
        return fileName.Replace(" ", "_");
    }

    public string GetRelativePath(string fileName)
    {
        return Path.Combine("uploads", fileName);
    }

    public string GetFullPath(string fileName)
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

    public static string ExtractMetadata(Stream fileStream)
    {
        var directories = ImageMetadataReader.ReadMetadata(fileStream);
        var metadata = new Dictionary<string, string?>();

        foreach (var directory in directories)
        {
            foreach (var tag in directory.Tags)
            {
                metadata[$"{directory.Name} - {tag.Name}"] = tag.Description;
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

        if (mime == null) return null;

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
            }
        }


        return upload;
    }
}