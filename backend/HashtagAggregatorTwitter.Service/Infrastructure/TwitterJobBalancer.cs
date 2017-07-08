using System.Threading.Tasks;
using Microsoft.Extensions.Options;

using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;
using HashtagAggregator.Service.Contracts;
using HashtagAggregator.Service.Contracts.Jobs;
using HashtagAggregator.Shared.Common.Infrastructure;
using HashtagAggregatorTwitter.Contracts;
using HashtagAggregatorTwitter.Service.Settings;

namespace HashtagAggregatorTwitter.Service.Infrastructure
{
    public class TwitterJobBalancer : ISocialJobBalancer
    {
        private readonly IStorageAccessor accessor;
        private readonly IOptions<TwitterApiSettings> settings;
        private readonly IOptions<AppSettings> appSettings;
        private readonly IJobManager jobManager;

        public TwitterJobBalancer(IStorageAccessor accessor,
            IOptions<TwitterApiSettings> settings,
            IOptions<AppSettings> appSettings,
            IJobManager jobManager)
        {
            this.accessor = accessor;
            this.settings = settings;
            this.appSettings = appSettings;
            this.jobManager = jobManager;
        }

        public async Task<ICommandResult> TryCreateJob(string tag)
        {
            var isAdded = new CommandResult();
            // todo: move to settings or make review interval
            var intervalMonth = 30 * 60 * 24;
            var initTask = new TwitterJobTask(new HashTagWord(tag), intervalMonth);

            if (!CheckJobLimitExceeded(initTask))
            {
                await jobManager.StartNow(initTask);
                AddJob(initTask);
                isAdded.Success = true;
            }
            else
            {
                isAdded.Message = "Job Limit Exceeded";
            }
            return isAdded;
        }

        private void AddJob(IJobTask task)
        {
            task = new TwitterJobTask(task.Tag, settings.Value.TwitterMessagePublishDelay);
            jobManager.AddJob(task);
        }

        public void DeleteJob(string tag)
        {
            var task = new TwitterJobTask(new HashTagWord(tag), 0);
            jobManager.DeleteJob(task);
        }

        private bool CheckJobLimitExceeded(IJobTask task)
        {
            var list = accessor.GetJobsList();
            var isValid = list.Any(x => x.Id.Equals(task.JobId));
            return isValid || list.Count >= appSettings.Value.MaxReccuringJobsSupported;
        }
    }
}