using DotBelt.QueriesMutations;
using HotChocolate.Authorization;

namespace DotBelt.CMS.Shared.CMS.Media;

public static class InternalCrops
{
    public const string UploadsLibraryCrop = "UploadsLibrary";
    public const string FeaturedImageCrop = "FeaturedImage";
    
}

[ExtendObjectType<DotBeltQuery>]
public class UploadsQueries
{

    [Authorize]
    [UseProjection]
    public IQueryable<UploadResponse> GetUploadById(int id, ApplicationDbContext context)
    {
        return context
            .Uploads
            .Where(x => x.Id == id)
            .ProjectToUploadResponse();
    }

}