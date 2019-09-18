using System;
using EnglishLearning.TaskService.Application.Configuration;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace EnglishLearning.TaskService.BackgroundJobs.Configuration
{
    public static class BackgroundJobsSettings
    {
        public static IServiceCollection BackgroundJobsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var hangfireSettings = new HangfireSettings()
            {
                ConnectionString = configuration.GetValue<string>("Hangfire:ConnectionString"),
            };
            services.AddSingleton(hangfireSettings);
            
            services.AddHangfire(conf => conf
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(hangfireSettings.ConnectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true,
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            return services;
        }

        public static void UseBackgroundJobsServices(this IApplicationBuilder app, IBackgroundJobClient backgroundJobs)
        {
            app.UseHangfireDashboard("/api/tasks/hangfire");
            backgroundJobs.Enqueue(() => Log.Error("In hangfire"));
        }
    }
}
