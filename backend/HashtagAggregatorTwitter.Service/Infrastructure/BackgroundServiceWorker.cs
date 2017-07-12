using System.Threading.Tasks;

using HashtagAggregator.Core.Contracts.Interface.Cqrs.Command;
using HashtagAggregator.Service.Contracts;
using IBackgroundServiceWorker = HashtagAggregatorTwitter.Contracts.Interface.IBackgroundServiceWorker;


namespace HashtagAggregatorTwitter.Service.Infrastructure
{
    public class BackgroundServiceWorker : IBackgroundServiceWorker
    {
        private readonly ISocialJobBalancer jobBalancer;

        public BackgroundServiceWorker(ISocialJobBalancer jobBalancer)
        {
            this.jobBalancer = jobBalancer;
        }

        public async Task<ICommandResult> Start(string tag)
        {
            return await jobBalancer.TryCreateJob(tag);
        }

        public void Stop(string tag)
        {
            jobBalancer.DeleteJob(tag);
        }
    }
}