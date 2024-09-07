using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.CMS;
using DotBelt.QueriesMutations;
using HotChocolate.Data;
using HotChocolate.Types;

namespace DotBelt.Queries;

[ExtendObjectType<GraphQLQuery>]

public class PostsQueries
{
    // TODO: Use DTOs
    
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<Post> GetPosts(ApplicationDbContext context)
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
  
    [UseProjection]

    public IQueryable<Post> GetPostByUrl(string url, ApplicationDbContext context)
    {
        var p = context
            .Posts
            .Where(x => x.UrlName == url)
            .FirstOrDefault();
       
        
        return context
            .Posts
            .Where(x => x.UrlName == url);
    }


}