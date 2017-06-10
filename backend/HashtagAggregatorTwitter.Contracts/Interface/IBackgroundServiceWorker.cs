using System.Threading.Tasks;

namespace HashtagAggregatorTwitter.Contracts.Interface
{
    public interface IBackgroundServiceWorker
    {
        Task<bool> Start(string tag);

        void Stop(string tag);
    }
}