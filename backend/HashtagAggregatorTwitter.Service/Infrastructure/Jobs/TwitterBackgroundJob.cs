using System;
using System.Threading.Tasks;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;
using HashtagAggregator.Shared.Common.Infrastructure;
using HashtagAggregator.Shared.Logging;
using HashtagAggregatorTwitter.Contracts;
using HashtagAggregatorTwitter.Models;
using Microsoft.Extensions.Logging;
using Tweetinvi;
using Tweetinvi.Parameters;

namespace HashtagAggregatorTwitter.Service.Infrastructure.Jobs
{
    public class TwitterBackgroundJob
    {
        private readonly ITwitterQueue queue;
        private readonly ILogger<TwitterBackgroundJob> logger;

        public TwitterBackgroundJob(ITwitterAuth auth, ITwitterQueue queue, ILogger<TwitterBackgroundJob> logger)
        {
            this.queue = queue;
            this.logger = logger;
            auth.Authenticate();
        }

        public async Task<ICommandResult> Execute(HashTagWord hashTag)
        {
            var tweetsParameters = new SearchTweetsParameters(hashTag.TagWithHash);
            tweetsParameters.TweetSearchType = TweetSearchType.OriginalTweetsOnly;
            var tweets = await SearchAsync.SearchTweets(tweetsParameters);


            var fail = ExceptionHandler.GetLastException()?.TwitterDescription;
            if (!String.IsNullOrEmpty(fail))
            {
                logger.LogError(
                    LoggingEvents.EXCEPTION_GET_TWITTER_MESSAGE,
                    "Failed to get messages by {hashtag} with {error}",
                    hashTag.TagWithHash,
                    fail);
            }

            queue.EnqueueMany(tweets);
            return new CommandResult
            {
                Success = true
            };
        }
    }
}