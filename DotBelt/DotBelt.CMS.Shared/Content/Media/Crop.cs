namespace DotBelt.CMS.Shared.CMS.Media;

public class Crop
{
    public int? Width { get; set; }
    public int? Height { get; set; }
    
    public string Name { get; set; }

    public Crop(int? width, int? height, string name)
    {
        if(width == null && height == null) 
        {
            throw new InvalidOperationException("Width and Height cannot be both null.");
        }
       
        Width = width;
        Height = height;
        Name = name;
    }
}