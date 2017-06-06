using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacSerilogIntegration;
using HashtagAggregatorTwitter.Service.Configuration.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace HashtagAggregatorTwitter.Service.Configuration
{
    public class AutofacModulesConfigurator
    {
        public IContainer Configure(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterLogger();
            builder.RegisterModule<TwitterModule>();

            builder.Populate(services);
            return builder.Build();
        }
    }
}
