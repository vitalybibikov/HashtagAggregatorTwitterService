using System.Collections.Generic;
using System.Threading.Tasks;

using HashtagAggregatorTwitter.Models;

namespace HashtagAggregatorTwitter.Contracts
{
    public interface IMessageServiceFacade 
    {
        Task<List<MessageModel>> GetAllAsync(HashTagModel hashtag);
    }
}
