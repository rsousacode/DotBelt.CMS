namespace DotBelt.CMS.Shared.CMS.Media;

public static class Mimes
{
    public static string[] Images =
        ["image/apng", "image/avif", "image/giv", "image/jpeg", "image/png", "image/svg+xml", "image/webp"];
    
    
    public static bool IsImage(string mimeType) 
    {
        return Images.Contains(mimeType);
    }
}