using System;
using Hangfire;
using HashtagAggregator.Shared.Common.Infrastructure;
using HashtagAggregatorTwitter.Contracts;
using HashtagAggregatorTwitter.Contracts.Jobs;
using HashtagAggregatorTwitter.Service.Settings;
using Microsoft.Extensions.Options;

namespace HashtagAggregatorTwitter.Service.Infrastructure
{
    public class BackgroundServiceWorker : IBackgroundServiceWorker
    {
        private readonly ITwitterBackgroundJob twitterJob;
        private readonly IOptions<TwitterApiSettings> settings;

        public BackgroundServiceWorker(ITwitterBackgroundJob twitterJob, IOptions<TwitterApiSettings> settings)
        {
            this.twitterJob = twitterJob;
            this.settings = settings;
        }

        public async void Start(string tag)
        {
            await twitterJob.Execute(new HashTagWord(tag), TimeSpan.FromDays(30));

            if (!String.IsNullOrEmpty(tag))
            {
                RecurringJob.AddOrUpdate<ITwitterBackgroundJob>(
                    $"twitter-enqueue-{tag}-id",
                    job => job.Execute(new HashTagWord(tag), GetDefaultInterval()),
                    Cron.MinuteInterval(settings.Value.TwitterMessagePublishDelay));
            }
        }

        private TimeSpan GetDefaultInterval()
        {
            return TimeSpan.FromMinutes(settings.Value.TwitterMessagePublishDelay);
        }

        public void Stop(string tag)
        {
            if (!String.IsNullOrEmpty(tag))
            {
                RecurringJob.RemoveIfExists($"twitter-enqueue-{tag}-id");
            }
        }
    }
}