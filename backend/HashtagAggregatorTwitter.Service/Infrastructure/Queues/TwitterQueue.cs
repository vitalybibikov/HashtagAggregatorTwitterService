using System;
using System.Collections.Generic;
using HashtagAggregatorTwitter.Contracts;
using Tweetinvi.Models;

namespace HashtagAggregatorTwitter.Service.Infrastructure.Queues
{
    public class TwitterQueue : ITwitterQueue
    {
        public void Enqueue(ITweet tweet)
        {
            throw new NotImplementedException();
        }

        public void EnqueueMany(IEnumerable<ITweet> tweet)
        {
            throw new NotImplementedException();
        }
    }
}
