using BoilerPlate.Shared;
using BoilerPlate.Shared.CMS;
using BoilerPlateSSR.QueriesMutations;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlateSSR.Queries;

public class EditPostResult 
{
    public bool Success { get; set; }
}

[ExtendObjectType(typeof(GraphQLMutation))]

public class EditPostMutation
{
    public async Task<EditPostResult> EditPostAsync( ApplicationDbContext dbContext, int postId, EditablePost payload )
    {
        var post = await dbContext
            .Posts
            .Where(x => x.Id == postId)
            .FirstOrDefaultAsync();

        if (post == null) return new EditPostResult() { Success = false };;

        post.ModifiedDate = DateTime.UtcNow;
        post.Title = payload.Title; 
        post.Content = payload.Content;
        post.Description = payload.Description;

        if (payload.Content != null)
        {
            post.ContentHtml = PostHelpers.GetHtmlFromContent(payload.Content);
        }
                
        return new EditPostResult() { Success = await dbContext.SaveChangesAsync() >= 0 };
    }
}