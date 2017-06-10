using System.Linq;
using System.Threading.Tasks;
using HashtagAggregator.Shared.Common.Infrastructure;
using HashtagAggregatorTwitter.Contracts.Interface;
using HashtagAggregatorTwitter.Contracts.Interface.Jobs;
using HashtagAggregatorTwitter.Service.Settings;
using Microsoft.Extensions.Options;

namespace HashtagAggregatorTwitter.Service.Infrastructure
{
    public class TwitterJobBalancer : ISocialJobBalancer
    {
        private readonly IStorageAccessor accessor;
        private readonly IOptions<TwitterApiSettings> settings;
        private readonly IOptions<AppSettings> appSettings;
        private readonly IJobManager jobManager;
        private readonly IReccuringJobBuilder builder;

        public TwitterJobBalancer(IStorageAccessor accessor,
            IOptions<TwitterApiSettings> settings,
            IOptions<AppSettings> appSettings,
            IJobManager jobManager,
            IReccuringJobBuilder builder)
        {
            this.accessor = accessor;
            this.settings = settings;
            this.appSettings = appSettings;
            this.jobManager = jobManager;
            this.builder = builder;
        }

        public async Task<bool> TryCreateJob(string tag)
        {
            bool isAdded = false;

            var initialJob = builder
                .WithInterval(30 * 60 * 24) // todo: move to settings or make review interval
                .WithTag(new HashTagWord(tag))
                .Build();

            if (CheckJobLimit(initialJob))
            {
                await AddJob(tag, initialJob);
                isAdded = true;
            }

            return isAdded;
        }

        private async Task AddJob(string tag, IJob initialJob)
        {
            await jobManager.StartNow(initialJob);

            var reccuringJob = builder
                .WithInterval(settings.Value.TwitterMessagePublishDelay)
                .WithTag(new HashTagWord(tag))
                .Build();

            jobManager.AddJob(reccuringJob);
        }

        public void DeleteHashTag(string tag)
        {
            var job = builder
                .WithTag(new HashTagWord(tag))
                .Build();

            jobManager.DeleteJob(job);
        }

        private bool CheckJobLimit(IJob job)
        {
            var list = accessor.GetJobsList();
            bool isValid = list.Any(x => x.Id.Equals(job.JobId));
            return isValid && list.Count <= appSettings.Value.MaxReccuringJobsSupported;
        }
    }
}