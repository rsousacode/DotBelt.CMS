using DotBelt.CMS.Shared.CMS.Media;

namespace DotBelt.CMS.Shared.Commands.Seeding;

public static class SeedCrops
{
    public static List<Crop> GetCrops()
    {
        var uploadsLibraryCrop = new Crop
        {
            Name = InternalCrops.UploadsLibraryCrop,
            Width = 100,
            Height = 100,
            CropPositionX = CropPositionX.Left,
            CropPositionY = CropPositionY.Top,
            SoftCrop = true,
            Internal = true
        };
          var featuredImageCrop = new Crop
        {
            Name = InternalCrops.FeaturedImageCrop,
            Width = 291,
            Height = 291,
            CropPositionX = CropPositionX.Center,
            CropPositionY = CropPositionY.Top,
            SoftCrop = true,
            Internal = true
        };
        
        return [uploadsLibraryCrop, featuredImageCrop];

    }
}