using BoilerPlate.Shared;
using BoilerPlate.Shared.CMS;
using BoilerPlateSSR.QueriesMutations;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlateSSR.Queries;

[ExtendObjectType(typeof(GraphQLMutation))]

public class EditPostMutation
{
    public async Task<bool> EditPostAsync( ApplicationDbContext dbContext, int postId, EditablePost payload )
    {
        var post = await dbContext
            .Posts
            .Where(x => x.Id == postId)
            .FirstOrDefaultAsync();

        if (post == null) return false;

        post.ModifiedDate = DateTime.UtcNow;
        post.Title = payload.Title; 
        post.Content = payload.Content;
        post.Description = payload.Description;

        if (payload.Content != null)
        {
            post.ContentHtml = PostHelpers.GetHtmlFromContent(payload.Content);
        }
        
        dbContext.Posts.Add( post );
        
        return await dbContext.SaveChangesAsync() >= 0;
    }
}