using System.Collections.Generic;
using System.Threading.Tasks;
using HashtagAggregator.Shared.Common.Infrastructure;
using HashtagAggregatorTwitter.Models;

namespace HashtagAggregatorTwitter.Contracts
{
    public interface IMessageServiceFacade 
    {
        Task<List<MessageModel>> GetAllAsync(HashTagWord hashtag);
    }
}
