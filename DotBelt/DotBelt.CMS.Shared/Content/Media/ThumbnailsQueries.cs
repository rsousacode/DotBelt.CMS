using DotBelt.QueriesMutations;
using HotChocolate.Authorization;

namespace DotBelt.CMS.Shared.CMS.Media;

[ExtendObjectType<DotBeltQuery>]

public class ThumbnailsQueries
{
    [Authorize]
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<ThumbnailResponse> GetThumbnails (ApplicationDbContext context)
    {
        return context
            .Thumbnails
            .Where(x => x.Name == InternalCrops.UploadsLibraryCrop)
            .ProjectToThumbnailResponse();
    }
    
    [Authorize]
    [UseProjection]
    public IQueryable<ThumbnailResponse> GetThumbnailByUploadId (ApplicationDbContext context, int uploadId, string cropName)
    {
        return context
            .Thumbnails
            .Where(x => x.UploadId == uploadId && x.Name == cropName)
            .ProjectToThumbnailResponse();
    }

}