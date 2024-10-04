using DotBelt.QueriesMutations;
using HotChocolate.Authorization;

namespace DotBelt.CMS.Shared.Content.Post;


public class DeletedPostResult
{
    public bool Success { get; set; }
}


[ExtendObjectType(typeof(DotBeltMutation))]
public class DeletePost
{
    [Authorize]
    public async Task<DeletedPostResult> DeletePostAsync(int id, ApplicationDbContext context)
    {
        var post = context.Posts.FirstOrDefault(p => p.Id == id);
        if (post == null)
        {
            return new DeletedPostResult()
            {
                Success = false
            };
        }
        
        context.Remove(post);
        
        var success = await context.SaveChangesAsync() >= 0;

        return new DeletedPostResult()
        {
            Success = success
        };

    }
}