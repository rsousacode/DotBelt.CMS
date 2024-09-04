using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.CMS;
using BoilerPlateSSR.QueriesMutations;
using DotBelt.CMS.Shared.CMS.Blocks.Parser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlateSSR.Queries;

public class EditPostResult 
{
    public bool Success { get; set; }
}

[ExtendObjectType(typeof(GraphQLMutation))]

public class EditPostMutation
{
    public async Task<EditPostResult> EditPostAsync( ApplicationDbContext dbContext,
        [FromServices] BlockParser blockParser,
        int postId, EditablePost payload )
    {
        var post = await dbContext
            .Posts
            .Where(x => x.Id == postId)
            .FirstOrDefaultAsync();

        if (post == null) return new EditPostResult() { Success = false };;
        
        var urlName = PostHelpers.SanitizePermalink(payload.UrlName);

        post.ModifiedDate = DateTime.UtcNow;
        post.Title = payload.Title; 
        post.Content = payload.Content;
        post.Description = payload.Description;
        post.UrlName = urlName;
        
        post.FullUrl = urlName;

        if (payload.Content != null)
        {
            post.ContentHtml = blockParser.GetHtmlFromContent(payload.Content);
        }
                
        return new EditPostResult() { Success = await dbContext.SaveChangesAsync() >= 0 };
    }
}