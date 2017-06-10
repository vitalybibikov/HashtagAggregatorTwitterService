using System.Threading.Tasks;
using HashtagAggregatorTwitter.Contracts.Interface;

namespace HashtagAggregatorTwitter.Service.Infrastructure
{
    public class BackgroundServiceWorker : IBackgroundServiceWorker
    {
        private readonly ISocialJobBalancer jobBalancer;

        public BackgroundServiceWorker(ISocialJobBalancer jobBalancer)
        {
            this.jobBalancer = jobBalancer;
        }

        public async Task<bool> Start(string tag)
        {
            return await jobBalancer.TryCreateJob(tag);
        }

        public void Stop(string tag)
        {
            jobBalancer.DeleteHashTag(tag);
        }
    }
}