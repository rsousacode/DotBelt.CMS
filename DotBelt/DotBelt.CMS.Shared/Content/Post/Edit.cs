using DotBelt.Queries;
using DotBelt.CMS.Shared;
using DotBelt.QueriesMutations;
using DotBelt.CMS.Shared.CMS.Blocks.Parser;
using DotBelt.CMS.Shared.Content.Post;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<EditPostResult> EditPostAsync( ApplicationDbContext dbContext,
        [FromServices] BlockParser blockParser,
        int postId, PostResponse payload )
    {
        var post = await dbContext
            .Posts
            .Where(x => x.Id == postId)
            .FirstOrDefaultAsync();

        if (post == null) return new EditPostResult() { Success = false };;
        
        var urlName = PostHelpers.SanitizePermalink(payload.RelativeUrl);

        post.ModifiedDate = DateTime.UtcNow;
        post.Title = payload.Title; 
        post.Content = payload.Content;
        post.Description = payload.Description;
        post.RelativeUrl = urlName;
        
        post.FullUrl = urlName;

        if (payload.Content != null)
        {
            post.ContentHtml = blockParser.GetHtmlFromContent(payload.Content);
        }
                
        return new EditPostResult() { Success = await dbContext.SaveChangesAsync() >= 0 };
    }
}