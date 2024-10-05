using DotBelt.Queries;
using DotBelt.CMS.Shared;
using DotBelt.CMS.Shared.CMS;
using DotBelt.CMS.Shared.CMS.Jobs;
using DotBelt.QueriesMutations;
using DotBelt.CMS.Shared.Content.Post;
using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DotBelt.Mutations.Posts;


[ExtendObjectType(typeof(DotBeltMutation))]
public class Edit
{
    [Authorize]
    public async Task<PostResponse?> EditPostAsync( ApplicationDbContext dbContext,
        [Service] PostSchedulerService postSchedulerService,
        [Service] PermalinkChecker permalinkChecker,
        int postId, 
        PostResponse payload,
        CancellationToken cancellationToken = default)
    {
        var post = await dbContext
            .Posts
            .Include(p => p.FeaturedImage)
            .Where(x => x.Id == postId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (post == null) return null;
        
        var finalRelativeUrl = await permalinkChecker.CheckPermalink(payload.RelativeUrl, postId, cancellationToken);
        var fullUrl = await permalinkChecker.GetFullUrl(finalRelativeUrl, cancellationToken);

        post.ModifiedDate = DateTime.UtcNow;
        post.Title = payload.Title; 
        post.Content = payload.Content;
        post.Description = payload.Description;
        post.RelativeUrl = finalRelativeUrl;
        post.FeaturedImageId = payload.FeaturedImageId;
        post.Status = payload.Status;
        post.PublishDate = payload.PublishDate?.ToUniversalTime() ?? DateTime.UtcNow;
        post.FullUrl = fullUrl;

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

        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext
            .Posts
            .Where(p => p.Id == postId)
            .ProjectToPostResponse()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                
    }
}