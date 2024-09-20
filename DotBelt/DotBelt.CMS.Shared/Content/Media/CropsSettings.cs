namespace DotBelt.CMS.Shared.CMS.Media;

public class CropsSettings
{
    public static Crop UploadsLibraryCrop = new Crop(100, 100, "UploadsLibrary");
    public static List<Crop> Internal = new List<Crop>()
    {
        UploadsLibraryCrop
    };
}