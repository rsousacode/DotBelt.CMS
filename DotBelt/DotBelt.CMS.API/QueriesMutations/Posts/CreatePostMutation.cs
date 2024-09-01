using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.CMS;
using BoilerPlateSSR.QueriesMutations;

namespace BoilerPlateSSR.Queries;

[ExtendObjectType(typeof(GraphQLMutation))]
public class CreatePostMutation
{
    public async Task<Post> CreatePostAsync( ApplicationDbContext dbContext, PostTypeEnum type, EditablePost payload )
    {
        var urlName = PostHelpers.SanitizePermalink(payload.UrlName);
        
        
        var post = new Post()
        {
            Title = payload.Title,
            Content = payload.Content,
            PostType = type,
            UrlName = urlName,
            FullUrl = urlName,
            Description = payload.Description,
        };

        post.PublishDate = DateTime.UtcNow;

        if (payload.Content != null)
        {
            post.ContentHtml = PostHelpers.GetHtmlFromContent(payload.Content);
        }
        
        dbContext.Posts.Add( post );
        
        await dbContext.SaveChangesAsync();

        return post;
    }
}