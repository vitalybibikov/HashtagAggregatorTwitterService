using HashtagAggregator.Shared.Common.Infrastructure;

namespace HashtagAggregatorTwitter.Contracts.Interface.Jobs
{
    public interface IJobTask
    {
        int Interval { get; }

        HashTagWord Tag { get; }

        string JobId { get; }
    }
}
