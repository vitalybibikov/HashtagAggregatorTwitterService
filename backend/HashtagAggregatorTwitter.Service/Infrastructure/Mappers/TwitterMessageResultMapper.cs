using HashtagAggregatorTwitter.Models;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi.Models;

namespace HashtagAggregatorTwitter.Service.Infrastructure.Mappers
{
    public class TwitterMessageResultMapper
    {
        public List<MessageModel> MapBunch(IEnumerable<ITweet> messages)
        {
            var results = new List<MessageModel>();
            if (messages != null)
            {
                foreach (var tweet in messages)
                {
                    var message = MapSingle(tweet);
                    results.Add(message);
                }
            }
            return results;
        }

        public MessageModel MapSingle(ITweet tweet)
        {
            var user = new UserModel
            {
                NetworkId = tweet.CreatedBy.IdStr,
                Url = tweet.Url,
                UserName = tweet.CreatedBy.Name,
                AvatarUrl50 = tweet.CreatedBy.ProfileImageUrl,
                MediaType = SocialMediaType.Twitter
            };

            var tags = tweet.Hashtags.Select(x => new HashTagModel()
            {
                HashTag = new HashTagWord(x.Text),
                IsEnabled = false
            }).ToList();


            var message = new MessageModel(0,
                tweet.Text,
                tags,
                SocialMediaType.Twitter,
                tweet.TweetLocalCreationDate.ToUniversalTime(),
                tweet.IdStr,
                user);
            return message;
        }
    }
}