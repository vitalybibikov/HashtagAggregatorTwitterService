using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HashtagAggregatorTwitter.Contracts;
using HashtagAggregatorTwitter.Models;
using HashtagAggregatorTwitter.Service.Infrastructure.Mappers;
using HashtagAggregatorTwitter.Service.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace HashtagAggregatorTwitter.Service.Infrastructure.Twitter
{
    public class TwitterMessageServiceFacade : ITwitterMessageFacade
    {
        private readonly IOptions<TwitterApiSettings> settings;
        private readonly ILogger<TwitterMessageServiceFacade> logger;

        public TwitterMessageServiceFacade(IOptions<TwitterApiSettings> settings, ITwitterAuth auth, ILogger<TwitterMessageServiceFacade> logger)
        {
            auth.Authenticate();
            this.settings = settings;
            this.logger = logger;
        }

        public async Task<List<MessageModel>> GetAllAsync(HashTagModel hashtag)
        {
            var tweetsParameters = new SearchTweetsParameters(hashtag.TagWithHash);
            tweetsParameters.TweetSearchType = TweetSearchType.OriginalTweetsOnly;
            IEnumerable<ITweet> tweets = await SearchAsync.SearchTweets(tweetsParameters);

            var fail = ExceptionHandler.GetLastException()?.TwitterDescription;
            if (!String.IsNullOrEmpty(fail))
            {
//                logger.LogError(
//                    LoggingEvents.EXCEPTION_GET_TWITTER_MESSAGE, 
//                    "Failed to get messages by {hashtag} with {error}", 
//                    hashtag, 
//                    fail);
            }
            var mapper = new TwitterMessageResultMapper();
            return mapper.MapBunch(tweets);
        }
    }
}