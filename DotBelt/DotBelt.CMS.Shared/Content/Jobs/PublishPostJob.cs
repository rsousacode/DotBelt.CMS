using DotBelt.CMS.Shared.Content.Post;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace DotBelt.CMS.Shared.CMS.Jobs;

public class PublishPostJob : IJob, IDisposable, IAsyncDisposable
{

    private readonly ApplicationDbContext _context;
    public PublishPostJob(ApplicationDbContext dbContext)
    {
        _context = dbContext; 
    }
    
    public async  Task Execute(IJobExecutionContext context)
    {
        var postId = context.JobDetail.JobDataMap.GetInt("PostId");
        
        await _context
            .Posts
            .Where(p => p.Id == postId)
            .ExecuteUpdateAsync(calls =>
                calls .SetProperty(x => x.Status, PostStatus.Published));
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}