namespace DotBelt.CMS.Shared.CMS.Media;

public class CropsSettings
{
    public static Crop UploadsLibraryCrop = new Crop("UploadsLibrary", 100, 100);
    public static List<Crop> Internal = new List<Crop>()
    {
        UploadsLibraryCrop
    };
}