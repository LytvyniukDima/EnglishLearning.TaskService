using EnglishLearning.TaskService.Application.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.TaskService.Application.Configuration
{
    public static class ApplicationSettings
    {
        public static IServiceCollection ApplicationConfiguration(this IServiceCollection services)
        {
            services.AddSingleton(new EnglishTaskServiceMapper());

            return services;
        }
    }
}