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
            PublishDate = DateTime.UtcNow,
            Tenant = null!
        };


        dbContext.Posts.Add( post );
        
        await dbContext.SaveChangesAsync();

        return post.ToPostResponse();
    }
}