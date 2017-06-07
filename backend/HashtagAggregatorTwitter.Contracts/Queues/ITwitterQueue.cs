using System.Collections.Generic;
using System.Threading.Tasks;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;
using Tweetinvi.Models;

namespace HashtagAggregatorTwitter.Contracts.Queues
{
    public interface ITwitterQueue
    {
        Task<ICommandResult> Enqueue(ITweet tweet);

        Task<ICommandResult> EnqueueMany(IEnumerable<ITweet> tweet);
    }
}