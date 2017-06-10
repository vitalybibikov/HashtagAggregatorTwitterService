using System;
using System.Threading.Tasks;
using Hangfire;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;
using HashtagAggregatorTwitter.Contracts;
using HashtagAggregatorTwitter.Contracts.Interface.Jobs;

namespace HashtagAggregatorTwitter.Service.Infrastructure
{
    public class RecurringJobManager : IJobManager
    {
        private readonly ITwitterJob job;

        public RecurringJobManager(ITwitterJob job)
        {
            this.job = job;
        }

        public ICommandResult AddJob(IJobTask task)
        {
            RecurringJob.AddOrUpdate<ITwitterJob>(
                task.JobId,
                x => x.Execute((TwitterJobTask) task),
                Cron.MinuteInterval(task.Interval));

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