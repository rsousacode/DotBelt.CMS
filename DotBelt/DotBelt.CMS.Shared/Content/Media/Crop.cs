namespace DotBelt.CMS.Shared.CMS.Media;

public class Crop
{
    public int Id { get; set; }
    public bool Internal { get; set; }
    public int Width { get; set; }
    public int? Height { get; set; }
    
    public required string Name { get; set; }
    
    public bool SoftCrop { get; set; }
    
    public CropPositionX CropPositionX { get; set; }
    
    public CropPositionY CropPositionY { get; set; }
    
    public ICollection<Thumbnail>? Thumbnails { get; set; }

}