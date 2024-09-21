namespace DotBelt.CMS.Shared.CMS.Media;

public class Crop
{
    public int Width { get; set; }
    public int? Height { get; set; }
    
    public string Name { get; set; }

    public Crop (string name, int width, int? height = null)
    {
        Width = width;
        Height = height;
        Name = name;
    }
}