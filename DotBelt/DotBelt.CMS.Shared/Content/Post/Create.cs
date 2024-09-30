using DotBelt.Queries;
using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.CMS;
using DotBelt.QueriesMutations;
using DotBelt.CMS.Shared.CMS.Blocks.Parser;
using DotBelt.CMS.Shared.Content.Post;
using DotBelt.CMS.Shared.Identity;
using HotChocolate.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DotBelt.Mutations.Posts.Create;

[ExtendObjectType(typeof(DotBeltMutation))]
public class Create
{
    [Authorize]
    public async Task<Post?> CreatePostAsync( 
        ApplicationDbContext dbContext, 
        [Service] IHttpContextAccessor httpContextAccessor,
        [Service] BlockParser blockParser,
        PostTypeEnum type, 
        PostResponse payload )
    {
        var urlName = PostHelpers.SanitizePermalink(payload.RelativeUrl);

        var userId = httpContextAccessor.GetUserId();
        
        if(userId == null)
        {
            return null;
        }
        
        var tenantId = dbContext
            .Tenants
            .AsNoTracking()
            .Select(x => x.Id)
            .FirstOrDefault();
        
        
        var post = new Post()
        {
            Title = payload.Title,
            Content = payload.Content,
            PostType = type,
            RelativeUrl = urlName,
            FullUrl = urlName,
            Description = payload.Description,
            Author = null!,
            AuthorId = userId.Value,
            TenantId = tenantId,
            FeaturedImageId = payload.FeaturedImageId,
            ContentHtml = payload.Content != null ? blockParser.GetHtmlFromContent(payload.Content) : "",
            PublishDate = DateTime.UtcNow,
            Tenant = null!
        };


        dbContext.Posts.Add( post );
        
        await dbContext.SaveChangesAsync();

        return post;
    }
}