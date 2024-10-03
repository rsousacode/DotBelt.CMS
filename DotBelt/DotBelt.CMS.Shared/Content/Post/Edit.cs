using DotBelt.Queries;
using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.CMS;
using DotBelt.QueriesMutations;
using DotBelt.CMS.Shared.Content.Post;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DotBelt.Mutations.Posts;

public class EditPostResult 
{
    public bool Success { get; set; }
}

[ExtendObjectType(typeof(DotBeltMutation))]
public class Edit
{
    [Authorize]
    public async Task<PostResponse?> EditPostAsync( ApplicationDbContext dbContext,
        int postId, 
        PostResponse payload )
    {
        var post = await dbContext
            .Posts
            .Where(x => x.Id == postId)
            .FirstOrDefaultAsync();

        if (post == null) return null;
        
        var urlName = PostHelpers.SanitizePermalink(payload.RelativeUrl);

        post.ModifiedDate = DateTime.UtcNow;
        post.Title = payload.Title; 
        post.Content = payload.Content;
        post.Description = payload.Description;
        post.RelativeUrl = urlName;
        post.FeaturedImageId = payload.FeaturedImageId;
        post.Status = payload.Status;
        post.PublishDate = payload.PublishDate?.ToUniversalTime() ?? DateTime.UtcNow;
        
        post.FullUrl = urlName;

        await dbContext.SaveChangesAsync();

        return post.ToPostResponse();
                
    }
}