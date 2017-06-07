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
        private readonly IOptions<TwitterApiSettings> settings;

        public BackgroundServiceWorker(IOptions<TwitterApiSettings> settings)
        {
            this.settings = settings;
        }

        public void Start()
        {
            BackgroundJob.Schedule<ITwitterBackgroundJob>(
                x => x.Execute(new HashTagWord("tag")),
                TimeSpan.FromSeconds(settings.Value.TwitterMessagePublishDelay));
        }
    }
}