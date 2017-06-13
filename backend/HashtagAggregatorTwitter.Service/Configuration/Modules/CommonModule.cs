using Autofac;

using HahtagAggregatorTwitter.Storage;
using HashtagAggregator.Service.Contracts;
using HashtagAggregator.Service.Contracts.Jobs;
using HashtagAggregator.Service.Contracts.Queues;
using HashtagAggregatorTwitter.Service.Infrastructure;
using HashtagAggregatorTwitter.Service.Infrastructure.Queues;
using IBackgroundServiceWorker = HashtagAggregatorTwitter.Contracts.Interface.IBackgroundServiceWorker;

namespace HashtagAggregatorTwitter.Service.Configuration.Modules
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AzureQueueInitializer>().As<IAzureQueueInitializer>();
            builder.RegisterType<RecurringJobManager>().As<IJobManager>();
            builder.RegisterType<TwitterJobBalancer>().As<ISocialJobBalancer>();
            builder.RegisterType<BackgroundServiceWorker>().As<IBackgroundServiceWorker>();

            builder.RegisterType<HangfireStorageAccessor>().As<IStorageAccessor>().SingleInstance();
        }
    }
}