using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.CMS;
using DotBelt.CMS.Shared.Content.Post;
using DotBelt.QueriesMutations;
using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;

namespace DotBelt.Queries;

[ExtendObjectType<GraphQLQuery>]

public class PostsQueries
{
    [Authorize]
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<PostResponse> GetPosts(ApplicationDbContext context)
    {
        return context
            .Posts
            .ProjectToPostResponse();
    }
    
    
    [UseProjection]
    [Authorize]
    public IQueryable<PostResponse> GetPostById(int id, ApplicationDbContext context)
    {
        return context
            .Posts
            .Where(x => x.Id == id)
            .ProjectToPostResponse();
    }
  
    [UseProjection]
    public IQueryable<PostResponse> GetPostByUrl(string url, ApplicationDbContext context)
    {
        return context
            .Posts
            .Where(x => x.RelativeUrl == url)
            .ProjectToPostResponse();
    }


}