using Quartz;
using Quartz.Spi;

namespace DotBelt.CMS.Shared.CMS.Jobs;

[RegisterScoped]
public class PostSchedulerService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IJobFactory _jobFactory;

    public PostSchedulerService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
    {
        _schedulerFactory = schedulerFactory;
        _jobFactory = jobFactory;
    }

    private async Task<IScheduler> GetSchedulerAsync()
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        scheduler.JobFactory = _jobFactory;
        return scheduler;
    }

    public async Task SchedulePublishPostJob(int postId, DateTimeOffset publishDate)
    {
        var scheduler = await GetSchedulerAsync();
        
        var scheduleId = $"publishPostTrigger-{postId}";

        var group = "postPublishing";
        
        var obKey = new JobKey(scheduleId, group);

        var jobDetail = JobBuilder.Create<PublishPostJob>()
            .WithIdentity(obKey)
            .UsingJobData("PostId", postId)
            .Build();

        var date = publishDate.ToLocalTime();
        
        var triggerKey = new TriggerKey(scheduleId, group);
        
        var detail = await scheduler.GetJobDetail(obKey);
        
        var trigger = TriggerBuilder.Create()
            .WithIdentity(scheduleId, group)
            .StartAt(date)
            .Build();
        
        
        if (detail != null)
        {
            await scheduler.RescheduleJob(triggerKey, trigger);
        }
        else
        {
            await scheduler.ScheduleJob(jobDetail, trigger);

        }
    }
    
    public async Task RemoveSchedulePublishPostJob(int postId)
    {
        var scheduler = await GetSchedulerAsync();
        
        var scheduleId = $"publishPostTrigger-{postId}";

        var group = "postPublishing";
        
        var obKey = new JobKey(scheduleId, group);
        
        var detail = await scheduler.GetJobDetail(obKey);

        if (detail != null)
        {
            await scheduler.DeleteJob(obKey);
        }
       
    }
    
    
}