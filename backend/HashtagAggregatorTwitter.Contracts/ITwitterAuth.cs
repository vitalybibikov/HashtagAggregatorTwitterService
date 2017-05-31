using Tweetinvi.Models;

namespace HashtagAggregatorTwitter.Contracts
{
    public interface ITwitterAuth
    {
        IAuthenticationContext Authenticate();
    }
}
