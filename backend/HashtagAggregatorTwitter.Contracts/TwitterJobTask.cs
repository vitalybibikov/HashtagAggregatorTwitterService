using System;
using HashtagAggregator.Shared.Common.Infrastructure;
using HashtagAggregatorTwitter.Contracts.Interface.Jobs;

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
            this.Tag = tag;
            this.Interval = interval;
        }
    }
}
