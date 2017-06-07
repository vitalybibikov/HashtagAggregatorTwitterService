using Autofac;
using HashtagAggregatorTwitter.Contracts;
using HashtagAggregatorTwitter.Contracts.Jobs;
using HashtagAggregatorTwitter.Service.Infrastructure;
using HashtagAggregatorTwitter.Service.Infrastructure.Jobs;
using HashtagAggregatorTwitter.Service.Infrastructure.Queues;
using HashtagAggregatorTwitter.Service.Infrastructure.Twitter;

namespace HashtagAggregatorTwitter.Service.Configuration.Modules
{
    public class TwitterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BackgroundServiceWorker>().As<IBackgroundServiceWorker>();
            builder.RegisterType<TwitterBackgroundJob>().As<ITwitterBackgroundJob>();
            
            builder.RegisterType<TwitterAuth>().As<ITwitterAuth>();
            builder.RegisterType<TwitterQueue>().As<ITwitterQueue>();
        }
    }
}