using System.Collections.Generic;
using System.Threading.Tasks;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;
using HashtagAggregatorTwitter.Contracts;
using HashtagAggregatorTwitter.Contracts.Interface.Queues;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Tweetinvi.Models;

namespace HashtagAggregatorTwitter.Service.Infrastructure.Queues
{
    public class TwitterQueue : ITwitterQueue
    {
        private readonly IAzureQueueInitializer initializer;

        public TwitterQueue(IAzureQueueInitializer initializer)
        {
            this.initializer = initializer;
        }

        public async Task<ICommandResult> Enqueue(ITweet tweet)
        {
            var message = JsonConvert.SerializeObject(tweet);
            var result = new CloudQueueMessage(message);
            await initializer.Queue.AddMessageAsync(result);
            return new CommandResult
            {
                Success = true
            };
        }

        public async Task<ICommandResult> EnqueueMany(IEnumerable<ITweet> tweets)
        {
            foreach (var tweet in tweets)
            {
                await Enqueue(tweet);
            }
            return new CommandResult {Success = true};
        }
    }
}