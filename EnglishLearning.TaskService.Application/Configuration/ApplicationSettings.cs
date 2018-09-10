using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.InternalServices;
using EnglishLearning.TaskService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.TaskService.Application.Configuration
{
    public static class ApplicationSettings
    {
        public static IServiceCollection ApplicationConfiguration(this IServiceCollection services)
        {
            services.AddSingleton(new EnglishTaskServiceMapper());

            services.AddTransient<IEnglishTaskService, EnglishTaskService>();
            services.AddTransient<IRandomEnglishTaskService, RandomEnglishTaskService>();
            
            return services;
        }
    }
}