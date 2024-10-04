using DotBelt.Queries;
using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.CMS;
using DotBelt.CMS.Shared.CMS.Jobs;
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
        [Service] PostSchedulerService postSchedulerService,
        int postId, 
        PostResponse payload )
    {
        var post = await dbContext
            .Posts
            .Include(p => p.FeaturedImage)
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

        if (payload.Status is PostStatus.Published or PostStatus.Scheduled
            && DateTimeOffset.UtcNow < payload.PublishDate)
        {
            post.Status = PostStatus.Scheduled;
            await postSchedulerService.SchedulePublishPostJob(post.Id, payload.PublishDate!.Value );
        }
        else
        {
            if (post.Status == PostStatus.Scheduled)
            {
                post.Status = PostStatus.Published;
            }
            
            await postSchedulerService.RemoveSchedulePublishPostJob(post.Id);
        }

        await dbContext.SaveChangesAsync();

        return await dbContext
            .Posts
            .Where(p => p.Id == postId)
            .ProjectToPostResponse()
            .FirstOrDefaultAsync();
                
    }
}