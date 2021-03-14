using EnglishLearning.TaskService.ExternalServices.Abstract;
using EnglishLearning.TaskService.ExternalServices.Infrastructure;
using EnglishLearning.TaskService.ExternalServices.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.TaskService.ExternalServices.Configuration
{
    public static class ExternalServicesSettings
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddSingleton(new ExternalServicesMapper());
            
            services.AddTransient<IWordMetadataRepository, WordMetadataRepository>();
            return services;
        }
    }
}