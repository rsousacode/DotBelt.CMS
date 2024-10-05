using DotBelt.CMS.Shared.CMS;
using DotBelt.QueriesMutations;
using Microsoft.EntityFrameworkCore;

namespace DotBelt.CMS.Shared.Content.Post;

[ExtendObjectType(typeof(DotBeltMutation))]
public class Clone
{
    public async Task<PostResponse?> ClonePostAsync(int postId, 
        ApplicationDbContext context,
        [Service] PermalinkChecker permalinkChecker,
        CancellationToken cancellationToken = default)
    {
        var postToClone = await context
            .Posts
            .Include(x => x.Taxonomies)
            .Where(x => x.Id == postId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (postToClone == null) return null;

        postToClone.RelativeUrl = await permalinkChecker.CheckPermalink(postToClone.RelativeUrl, null, cancellationToken);
        postToClone.FullUrl = await permalinkChecker.GetFullUrl(postToClone.RelativeUrl, cancellationToken);
        postToClone.Status = PostStatus.Draft;
        postToClone.ModifiedDate = null;
        postToClone.PublishDate = DateTimeOffset.UtcNow;
        postToClone.Id = 0;
        
        context.Posts.Add(postToClone);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return postToClone.ToPostResponse();

    }
}