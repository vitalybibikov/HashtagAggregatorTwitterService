using System;
using System.Threading.Tasks;
using Hangfire;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;
using HashtagAggregator.Service.Contracts.Jobs;
using HashtagAggregatorTwitter.Contracts;
using HashtagAggregatorTwitter.Contracts.Interface.Jobs;
using HashtagAggregatorTwitter.Contracts.Settings;
using Microsoft.Extensions.Options;

namespace HashtagAggregatorTwitter.Service.Infrastructure
{
    public class RecurringJobManager : IJobManager
    {
        private readonly ITwitterJob job;
        private readonly IOptions<HangfireSettings> hangfireOptions;

        public RecurringJobManager(ITwitterJob job, IOptions<HangfireSettings> hangfireOptions)
        {
            this.job = job;
            this.hangfireOptions = hangfireOptions;
        }

        public ICommandResult AddJob(IJobTask task)
        {
            RecurringJob.AddOrUpdate<ITwitterJob>(
                task.JobId,
                x => x.Execute((TwitterJobTask) task),
                Cron.MinuteInterval(task.Interval),
                queue: hangfireOptions.Value.ServerName);
            return new CommandResult {Success = true};
        }

        public ICommandResult DeleteJob(IJobTask task)
        {
            RecurringJob.RemoveIfExists(task.JobId);
            return new CommandResult {Success = true};
        }

        public async Task<ICommandResult> StartNow(IJobTask task)
        {
            return await job.Execute((TwitterJobTask) task);
        }

        public ICommandResult ReconfigureJob(IJobTask task)
        {
            throw new NotImplementedException();
        }
    }
}