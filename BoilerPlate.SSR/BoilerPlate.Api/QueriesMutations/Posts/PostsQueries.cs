using BoilerPlate.Shared;
using BoilerPlate.Shared.CMS;
using BoilerPlateSSR.QueriesMutations;

namespace BoilerPlateSSR.Queries;

[ExtendObjectType<GraphQLQuery>]

public class PostsQueries
{
    // TODO: Use DTOs
    
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<Post> GetPosts([Service(ServiceKind.Pooled)] ApplicationDbContext context)
    {
        return context
            .Posts;
    }
    
    // TODO: Use DTOs
    
    [UseProjection]

    public IQueryable<Post> GetPostById(int id, ApplicationDbContext context)
    {
        return context
            .Posts
            .Where(x => x.Id == id);
    }


}