using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.CMS;
using DotBelt.CMS.Shared.Content.Post;
using DotBelt.CMS.Shared.GraphQL;
using DotBelt.QueriesMutations;
using HotChocolate;
using HotChocolate.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;

namespace DotBelt.Queries;

[ExtendObjectType<DotBeltQuery>]

public class PostsQueries
{
    // TODO: Insecure: Allow own origin (server) or [Authorize] authorized user 
    //[Authorize]
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
    
     public IQueryable<PostResponse> GetPublishedPages(ApplicationDbContext context)
    {
        return context
            .Posts
            .Where(x => x.Status == PostStatus.Published && x.PostType == PostTypeEnum.Page)
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
  
    [UseProjection]
    public IQueryable<PostResponse> GetHomepage(ApplicationDbContext context)
    {

        return context
            .Tenants
            .Select(x => x.Homepage!)
            .Where(x => x.Status == PostStatus.Published && x.PostType == PostTypeEnum.Page)
            .AsQueryable()
            .ProjectToPostResponse();
    }


}