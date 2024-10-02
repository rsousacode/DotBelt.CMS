namespace DotBelt.CMS.Shared.CMS.Media;

public class ThumbnailResponse
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public UploadResponse? Upload { get; set; } 
    public int? UploadId { get; set; }
    public DateTimeOffset? PublishDate { get; set; }
    
    public string? MimeType { get; set; }
    public string? FileName { get; set; }
    
    public string? RelativeUrl { get; set; }
    
    
    public int? Length { get; set; }
}