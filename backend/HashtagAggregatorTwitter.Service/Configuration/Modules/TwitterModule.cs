using Autofac;

using HashtagAggregatorTwitter.Contracts.Interface;
using HashtagAggregatorTwitter.Contracts.Interface.Jobs;
using HashtagAggregatorTwitter.Contracts.Interface.Queues;
using HashtagAggregatorTwitter.Service.Infrastructure.Jobs;
using HashtagAggregatorTwitter.Service.Infrastructure.Queues;
using HashtagAggregatorTwitter.Service.Infrastructure.Twitter;

namespace HashtagAggregatorTwitter.Service.Configuration.Modules
{
    public class TwitterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TwitterJob>().As<ITwitterJob>();
            builder.RegisterType<TwitterAuth>().As<ITwitterAuth>();
            builder.RegisterType<TwitterQueue>().As<ITwitterQueue>();
        }
    }
}