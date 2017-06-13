using System;
using HashtagAggregator.Service.Contracts.Jobs;
using HashtagAggregator.Shared.Common.Infrastructure;

namespace HashtagAggregatorTwitter.Contracts
{
    public class TwitterJobTask : IJobTask
    {
        private const string JobIdPattern = "twitter-enqueue-{0}-id";

        public HashTagWord Tag { get; }

        public int Interval { get; }

        public string JobId => String.Format(JobIdPattern, Tag.NoHashTag);

        public TwitterJobTask(HashTagWord tag, int interval)
        {
            Tag = tag;
            Interval = interval;
        }
    }
}
