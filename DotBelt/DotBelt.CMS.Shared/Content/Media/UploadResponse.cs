using DotBelt.CMS.Shared.Users;

namespace DotBelt.CMS.Shared.CMS.Media;

public class UploadResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public required string FileName { get; set; }
    public required string MimeType { get; set; }
    public int Length { get; set; }
    public required UserResponse Author { get; set; }
    public required string RelativeUrl { get; set; }
    public string? FullUrl { get; set; }
    public DateTimeOffset PublishDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    public string? MetaData { get; set; }
}