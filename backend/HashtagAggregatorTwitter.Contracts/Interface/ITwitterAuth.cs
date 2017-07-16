using Tweetinvi.Models;

namespace HashtagAggregatorTwitter.Contracts.Interface
{
    public interface ITwitterAuth
    {
        IAuthenticationContext Authenticate();
    }
}
