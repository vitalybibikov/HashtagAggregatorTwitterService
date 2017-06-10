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
        public ICommandResult AddJob(IJob job)
        {
            RecurringJob.AddOrUpdate<IJob>(
                job.JobId,
                x => job.Execute(),
                Cron.MinuteInterval(job.Interval));
            return new CommandResult {Success = true};
        }

        public ICommandResult DeleteJob(IJob job)
        {
            RecurringJob.RemoveIfExists(job.JobId);
            return new CommandResult { Success = true };
        }

        public async Task<ICommandResult> StartNow(IJob job)
        {
            return await job.Execute();
        }

        public ICommandResult ReconfigureJob(IJob job)
        {
            throw new NotImplementedException();
        }
    }
}