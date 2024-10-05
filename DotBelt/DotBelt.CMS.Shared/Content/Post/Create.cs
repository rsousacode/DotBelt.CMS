using DotBelt.Queries;
using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.CMS;
using DotBelt.QueriesMutations;
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
    public async Task<PostResponse?> CreatePostAsync( 
        ApplicationDbContext dbContext, 
        [Service] IHttpContextAccessor httpContextAccessor,
        [Service] PermalinkChecker permalinkChecker,
        PostTypeEnum type, 
        PostResponse payload,
        CancellationToken ct = default)
    {

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
        
        var finalRelativeUrl = await permalinkChecker.CheckPermalink(payload.RelativeUrl, null, ct);
        var fullUrl = await permalinkChecker.GetFullUrl(finalRelativeUrl, ct);
        
        
        var post = new Post()
        {
            Title = payload.Title,
            Content = payload.Content,
            PostType = type,
            RelativeUrl = finalRelativeUrl,
            Status = payload.Status,
            FullUrl = fullUrl,
            Description = payload.Description,
            Author = null!,
            AuthorId = userId.Value,
            TenantId = tenantId,
            FeaturedImageId = payload.FeaturedImageId,
            PublishDate = payload.PublishDate?.ToUniversalTime() ?? DateTimeOffset.UtcNow,
            Tenant = null!
        };


        dbContext.Posts.Add( post );
        
        await dbContext.SaveChangesAsync(ct);

        return post.ToPostResponse();
    }
}