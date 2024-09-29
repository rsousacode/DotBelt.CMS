using DotBelt.QueriesMutations;
using HotChocolate.Authorization;

namespace DotBelt.CMS.Shared.CMS.Media;

[ExtendObjectType<DotBeltQuery>]
public class UploadsQueries
{
    [Authorize]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<ThumbnailResponse> GetUploads (ApplicationDbContext context)
    {
        return context
            .Thumbnails
            .Where(x => x.Name == CropsSettings.UploadsLibraryCrop.Name)
            .ProjectToThumbnailResponse();
    }
    
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