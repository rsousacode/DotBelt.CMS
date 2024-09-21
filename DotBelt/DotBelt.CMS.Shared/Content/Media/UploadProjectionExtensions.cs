namespace DotBelt.CMS.Shared.CMS.Media;

public static class UploadProjectionExtensions
{
    public static IQueryable<UploadResponse> ProjectThumbnail(this IQueryable<Upload> query)
    {
        return query
            .Select(x => new UploadResponse()
            {
                Id = x.Id,
                FileName = x.FileName,
                Description = x.Parent!.Description,
                PublishDate = x.Parent!.PublishDate,
                MetaData = x.Parent.MetaData,
                RelativeUrl = x.RelativeUrl,
                MimeType = x.MimeType,
                Length = x.Length,
                ParentId = x.ParentId
            });
    }

    public static UploadResponse ToThumbnailResponse(this Upload upload)
    {
        return new UploadResponse()
        {
            Id = upload.Id,
            FileName = upload.FileName,
            Description = upload.Parent!.Description,
            PublishDate = upload.Parent!.PublishDate,
            MetaData = upload.Parent.MetaData,
            RelativeUrl = upload.RelativeUrl,
            MimeType = upload.MimeType,
            Length = upload.Length,
            ParentId = upload.ParentId
        };
    }
}