using Quartz;
using Quartz.Spi;
using Microsoft.Extensions.DependencyInjection;


namespace DotBelt.CMS.Shared.CMS.Jobs;

[RegisterSingleton()]
public class DotBeltJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DotBeltJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    #nullable disable

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var job = scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
            return job;

        }
    }
    
    #nullable enable

    public void ReturnJob(IJob job)
    {
        var disposable = job as IDisposable;
        disposable?.Dispose();
    }
}