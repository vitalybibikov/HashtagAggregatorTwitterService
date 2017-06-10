using System;
using System.Threading.Tasks;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;

namespace HashtagAggregatorTwitter.Contracts.Interface
{
    public interface ISocialJobBalancer
    {
        Task<ICommandResult> TryCreateJob(string tag);

        void DeleteJob(string tag);
    }
}
