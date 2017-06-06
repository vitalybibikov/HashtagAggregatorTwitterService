using System.Collections.Generic;
using Tweetinvi.Models;

namespace HashtagAggregatorTwitter.Contracts
{
    public interface ITwitterQueue
    {
        void Enqueue(ITweet tweet);

        void EnqueueMany(IEnumerable<ITweet> tweet);
    }
}
