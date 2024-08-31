using BoilerPlate.Shared;
using BoilerPlate.Shared.CMS;
using BoilerPlateSSR.QueriesMutations;

namespace BoilerPlateSSR.Queries;

[ExtendObjectType(typeof(GraphQLMutation))]
public class CreatePostMutation
{
    public async Task<Post> CreatePostAsync( ApplicationDbContext dbContext, EditablePost payload )
    {
        var post = new Post()
        {
            Title = payload.Title,
            Content = payload.Content,
            UrlName = PostHelpers.SanitizePermalink(payload.UrlName),
            Description = payload.Description,
        };

        post.PostType = PostTypeEnum.Post;
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