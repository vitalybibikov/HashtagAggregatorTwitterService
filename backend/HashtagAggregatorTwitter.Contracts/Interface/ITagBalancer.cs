using System;
using System.Threading.Tasks;

namespace HashtagAggregatorTwitter.Contracts.Interface
{
    public interface ISocialJobBalancer
    {
        Task<bool> TryCreateJob(string tag);

        void DeleteHashTag(string tag);
    }
}
