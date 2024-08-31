using BoilerPlate.Shared;
using BoilerPlate.Shared.CMS;
using BoilerPlateSSR.QueriesMutations;

namespace BoilerPlateSSR.Queries;

[ExtendObjectType(typeof(GraphQLMutation))]
public class CreatePostMutation
{
    public async Task<Post> CreatePostAsync( ApplicationDbContext dbContext, CreatePost payload )
    {
        var post = new Post()
        {
            Title = payload.Title,
            Content = payload.Content,
            UrlName = payload.UrlName,
            Description = payload.Description,
        };

        post.PostType = PostTypeEnum.Post;
        post.PublishDate = DateTime.UtcNow;
        
        dbContext.Posts.Add( post );
        
        await dbContext.SaveChangesAsync();

        return post;
    }
}