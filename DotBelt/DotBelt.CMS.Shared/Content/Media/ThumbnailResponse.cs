namespace DotBelt.CMS.Shared.CMS.Media;

public class ThumbnailResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required UploadResponse Upload { get; set; } 
    public int UploadId { get; set; }
    public DateTimeOffset PublishDate { get; set; }
    
    public required string MimeType { get; set; }
    public required string FileName { get; set; }
    
    public required string RelativeUrl { get; set; }
    
    
    public int Length { get; set; }
}