using System;
using Autofac;

using Hangfire;
using HashtagAggregator.Service.Contracts;
using HashtagAggregatorTwitter.Service.Configuration;
using HashtagAggregatorTwitter.Service.Settings;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Serilog;

namespace HashtagAggregatorTwitter.Service
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<QueueSettings>(Configuration.GetSection("QueueSettings"));
            services.Configure<TwitterAuthSettings>(Configuration.GetSection("TwitterAuthSettings"));
            services.Configure<TwitterApiSettings>(Configuration.GetSection("TwitterApiSettings"));

            var connectionString = Configuration.GetSection("AppSettings:ConnectionString").Value;

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddHangfire(config => config.UseSqlServerStorage(connectionString));
            services.AddMvc();

            var container = new AutofacModulesConfigurator().Configure(services);
            GlobalConfiguration.Configuration.UseActivator(new AutofacContainerJobActivator(container));

            return container.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IStorageAccessor accessor)
        {
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();

            var options = new BackgroundJobServerOptions
            {
                ServerName = "TwitterServiceServer"
            };
            app.UseHangfireServer(options);

            if (env.IsEnvironment("dev"))
            {
                app.UseHangfireDashboard();
                app.UseDeveloperExceptionPage();
                accessor.CancelRecurringJobs();
            }

            app.UseMvc();
        }
    }
}