using HashtagAggregator.Shared.Common.Infrastructure;
using HashtagAggregatorTwitter.Contracts.Interface.Jobs;

namespace HashtagAggregatorTwitter.Service.Infrastructure.Jobs
{
    public class SocialJobBuilder : IReccuringJobBuilder
    {
        private readonly IImmutableJob twitterBackgroundJob;

        public SocialJobBuilder(IImmutableJob twitterBackgroundJob)
        {
            this.twitterBackgroundJob = twitterBackgroundJob;
        }

        public IReccuringJobBuilder WithTag(HashTagWord tag)
        {
            twitterBackgroundJob.Tag = tag;
            return this;
        }

        public IReccuringJobBuilder WithInterval(int interval)
        {
            twitterBackgroundJob.Interval = interval;
            return this;
        }

        public IJob Build()
        {
            twitterBackgroundJob.Freeze();
            return twitterBackgroundJob;
        }
    }
}