using Autofac;

using HahtagAggregatorTwitter.Storage;
using HashtagAggregatorTwitter.Contracts.Interface;
using HashtagAggregatorTwitter.Contracts.Interface.Jobs;
using HashtagAggregatorTwitter.Contracts.Interface.Queues;
using HashtagAggregatorTwitter.Service.Infrastructure;
using HashtagAggregatorTwitter.Service.Infrastructure.Queues;

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