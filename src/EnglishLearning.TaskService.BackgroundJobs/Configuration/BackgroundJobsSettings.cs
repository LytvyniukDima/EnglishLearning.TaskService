using System;
using EnglishLearning.TaskService.Application.Configuration;
using EnglishLearning.TaskService.BackgroundJobs.Jobs;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.TaskService.BackgroundJobs.Configuration
{
    public static class BackgroundJobsSettings
    {
        public static IServiceCollection BackgroundJobsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var hangfireSettings = new HangfireSettings()
            {
                ConnectionString = configuration.GetValue<string>("Hangfire:ConnectionString"),
                Interval = configuration.GetValue<string>("Hangfire:Interval"),
            };
            services.AddSingleton(hangfireSettings);

            services.AddScoped<IBackgroundJob, FillFilterCacheBackgroundJob>();
            services.AddScoped<FillFilterCacheBackgroundJob>();
            
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

        public static void UseBackgroundJobsServices(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            app.UseHangfireDashboard("/api/tasks/hangfire");

            var hangfireSettings = serviceProvider.GetRequiredService<HangfireSettings>();
            AddRecurringJobs(hangfireSettings);
        }

        private static void AddRecurringJobs(HangfireSettings hangfireSettings)
        {
            RecurringJob.RemoveIfExists(nameof(FillFilterCacheBackgroundJob));
            RecurringJob.AddOrUpdate<FillFilterCacheBackgroundJob>(
                nameof(FillFilterCacheBackgroundJob), 
                job => job.Execute(), 
                hangfireSettings.Interval);
            
            RecurringJob.Trigger(nameof(FillFilterCacheBackgroundJob));
        }
    }
}
