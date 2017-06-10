using System.Threading.Tasks;
using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;

namespace HashtagAggregatorTwitter.Contracts.Interface
{
    public interface IBackgroundServiceWorker
    {
        Task<ICommandResult> Start(string tag);

        void Stop(string tag);
    }
}