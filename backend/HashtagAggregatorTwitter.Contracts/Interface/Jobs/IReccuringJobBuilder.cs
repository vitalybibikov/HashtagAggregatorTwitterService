using HashtagAggregator.Shared.Common.Infrastructure;

namespace HashtagAggregatorTwitter.Contracts.Interface.Jobs
{
    public interface IReccuringJobBuilder
    {
        IReccuringJobBuilder WithTag(HashTagWord tag);

        IReccuringJobBuilder WithInterval(int interval);

        IJob Build();
    }
}
