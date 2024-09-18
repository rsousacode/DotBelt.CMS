using DotBelt.QueriesMutations;
using HotChocolate.Authorization;

namespace DotBelt.CMS.Shared.CMS.Media;

[ExtendObjectType<GraphQLQuery>]
public class UploadsQueries
{
    [Authorize]
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<UploadResponse> GetUploads(ApplicationDbContext context)
    {
        return context
            .Uploads
            .ProjectToUploadResponse();
    }

}