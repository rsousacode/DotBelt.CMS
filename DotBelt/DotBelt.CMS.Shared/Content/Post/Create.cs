using DotBelt.Queries;
using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.CMS;
using DotBelt.QueriesMutations;
using DotBelt.CMS.Shared.CMS.Blocks.Parser;
using HotChocolate.Authorization;

namespace DotBelt.Mutations.Posts.Create;

[ExtendObjectType(typeof(GraphQLMutation))]
public class Create
{
    [Authorize]
    public async Task<Post> CreatePostAsync( ApplicationDbContext dbContext, 
        [Service] BlockParser blockParser,
        PostTypeEnum type, Create_PostRequest payload )
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
            post.ContentHtml = blockParser.GetHtmlFromContent(payload.Content);
        }
        
        dbContext.Posts.Add( post );
        
        await dbContext.SaveChangesAsync();

        return post;
    }
}