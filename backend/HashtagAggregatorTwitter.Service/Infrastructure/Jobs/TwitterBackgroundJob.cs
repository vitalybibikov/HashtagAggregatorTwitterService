using System;
using System.Threading.Tasks;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;
using HashtagAggregator.Shared.Common.Infrastructure;
using HashtagAggregator.Shared.Logging;
using HashtagAggregatorTwitter.Contracts.Interface;
using HashtagAggregatorTwitter.Contracts.Interface.Jobs;
using HashtagAggregatorTwitter.Contracts.Interface.Queues;
using HashtagAggregatorTwitter.Service.Infrastructure.Exception;
using Microsoft.Extensions.Logging;
using Tweetinvi;
using Tweetinvi.Parameters;

namespace HashtagAggregatorTwitter.Service.Infrastructure.Jobs
{
    public class ImmutableTwitterJob : ITwitterBackgroundJob
    {
        private const string JobIdPattern = "twitter-enqueue-{0}-id";
        private readonly ITwitterQueue queue;
        private readonly ILogger<ImmutableTwitterJob> logger;
        private HashTagWord tag;
        private int interval;

        public bool Freezed { get; private set; }

        public string JobId => String.Format(JobIdPattern, tag.NoHashTag);

        public int Interval
        {
            get => interval;
            set
            {
                if (interval != 0 && Freezed)
                {
                    throw new ImmutableException();
                }
                interval = value;
            }
        }

        public HashTagWord Tag
        {
            get => tag;
            set
            {
                if (!String.IsNullOrWhiteSpace(tag.ToString()) && Freezed)
                {
                    throw new ImmutableException();
                }
                tag = value;
            }
        }

        public ImmutableTwitterJob(ITwitterAuth auth,
            ITwitterQueue queue,
            ILogger<ImmutableTwitterJob> logger)
        {
            this.queue = queue;
            this.logger = logger;
            auth.Authenticate();
        }

        public async Task<ICommandResult> Execute()
        {
            var tweetsParameters = SearchParams(tag, TimeSpan.FromMinutes(interval));
            var tweets = await SearchAsync.SearchTweets(tweetsParameters);
            var fail = ExceptionHandler.GetLastException()?.TwitterDescription;
            if (!String.IsNullOrEmpty(fail))
            {
                logger.LogError(
                    LoggingEvents.EXCEPTION_GET_TWITTER_MESSAGE,
                    "Failed to get messages by {hashtag} with {error}",
                    tag.TagWithHash,
                    fail);
            }
            return await queue.EnqueueMany(tweets);
        }

        public void Freeze()
        {
            Freezed = true;
        }

        private SearchTweetsParameters SearchParams(HashTagWord hashTag, TimeSpan interval)
        {
            var tweetsParameters =
                new SearchTweetsParameters(hashTag.TagWithHash)
                {
                    TweetSearchType = TweetSearchType.OriginalTweetsOnly,
                    Since = DateTime.Now - interval
                };
            return tweetsParameters;
        }
    }
}