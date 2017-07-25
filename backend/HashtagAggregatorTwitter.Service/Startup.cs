using System;
using Autofac;
using Hangfire;
using HashtagAggregator.Service.Contracts;
using HashtagAggregatorTwitter.Contracts.Settings;
using HashtagAggregatorTwitter.Service.Configuration;
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

            if (env.IsEnvironment("dev"))
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
            }


            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<QueueSettings>(Configuration.GetSection("QueueSettings"));
            services.Configure<TwitterAuthSettings>(Configuration.GetSection("TwitterAuthSettings"));
            services.Configure<TwitterApiSettings>(Configuration.GetSection("TwitterApiSettings"));
            services.Configure<HangfireSettings>(Configuration.GetSection("HangfireSettings"));

            var connectionString = Configuration.GetSection("AppSettings:ConnectionString").Value;

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddHangfire(config => config.UseSqlServerStorage(connectionString));
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc();

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()));

            var container = new AutofacModulesConfigurator().Configure(services);
            GlobalConfiguration.Configuration.UseActivator(new AutofacContainerJobActivator(container));
            services.AddApplicationInsightsTelemetry(Configuration);
            return container.Resolve<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IStorageAccessor accessor)
        {
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();

            var options = new BackgroundJobServerOptions
            {
                ServerName = Configuration.GetSection("HangfireSettings:ServerName").Value
            };
            app.UseHangfireServer(options);
            app.UseHangfireDashboard();
            if (env.IsEnvironment("dev"))
            {
        
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");

            //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //{
            //    Authority = Configuration.GetSection("EndpointSettings:AuthEndpoint").Value,
            //    RequireHttpsMetadata = false, //todo: should be true when enabled https
            //    ApiName = "twitterapiservice",
            //    CacheDuration = TimeSpan.FromMinutes(10)
            //});

            //accessor.CancelRecurringJobs();
            app.UseMvc();
        }
    }
}