using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;
using HashtagAggregator.Shared.Common.Infrastructure;
using HashtagAggregator.Shared.Logging;
using HashtagAggregatorTwitter.Contracts;
using HashtagAggregatorTwitter.Contracts.Interface;
using HashtagAggregatorTwitter.Contracts.Interface.Jobs;
using HashtagAggregatorTwitter.Contracts.Interface.Queues;
using Tweetinvi;
using Tweetinvi.Parameters;

namespace HashtagAggregatorTwitter.Service.Infrastructure.Jobs
{
    public class TwitterJob : ITwitterJob
    {
        private readonly ITwitterQueue queue;
        private readonly ILogger<TwitterJob> logger;

        public TwitterJob(ITwitterAuth auth,
            ITwitterQueue queue,
            ILogger<TwitterJob> logger)
        {
            this.queue = queue;
            this.logger = logger;
            auth.Authenticate();
        }

        public async Task<ICommandResult> Execute(TwitterJobTask task)
        {
            var tweetsParameters = SearchParams(task.Tag, TimeSpan.FromMinutes(task.Interval));
            var tweets = await SearchAsync.SearchTweets(tweetsParameters);
            var fail = ExceptionHandler.GetLastException()?.TwitterDescription;
            if (!String.IsNullOrEmpty(fail))
            {
                logger.LogError(
                    LoggingEvents.EXCEPTION_GET_TWITTER_MESSAGE,
                    "Failed to get messages by {hashtag} with {error}",
                    task.Tag.TagWithHash,
                    fail);
            }
            return await queue.EnqueueMany(tweets);
        }

        private SearchTweetsParameters SearchParams(HashTagWord hashTag, TimeSpan span)
        {
            var tweetsParameters =
                new SearchTweetsParameters(hashTag.TagWithHash)
                {
                    TweetSearchType = TweetSearchType.OriginalTweetsOnly,
                    Since = DateTime.Now - span
                };
            return tweetsParameters;
        }
    }
}