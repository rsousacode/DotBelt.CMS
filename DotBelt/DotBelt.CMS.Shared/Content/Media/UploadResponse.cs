using DotBelt.CMS.Shared.Users;

namespace DotBelt.CMS.Shared.CMS.Media;

public class UploadResponse
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    
    public string? AltText { get; set; }
    
    public string? Caption { get; set; }
    public string? FileName { get; set; }
    public string? MimeType { get; set; }
    public int? Length { get; set; }
    public string? RelativeUrl { get; set; }
    public string? FullUrl { get; set; }
    public DateTimeOffset? PublishDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    
    public string? MetaData { get; set; } = null!;
    
    public UserResponse? Author { get; set; }
    
}