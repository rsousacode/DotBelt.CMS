namespace DotBelt.CMS.Shared.CMS.Media;

public static class Mimes
{
    public const string WEBP = "image/webp";
    public const string APNG = "image/apng";
    public const string PNG = "image/png";
    public const string AVIF = "image/avif";
    public const string GIV = "image/giv";
    public const string JPEG = "image/jpeg";
    public const string SVG = "image/svg+xml";

    public static string[] Images =
        [WEBP, APNG, AVIF, GIV, JPEG, SVG, PNG];


    public static bool IsImage(string mimeType)
    {
        return Images.Contains(mimeType);
    }
}