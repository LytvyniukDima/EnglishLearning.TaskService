using EnglishLearning.TaskService.Web.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.TaskService.Web.Configuration
{
    public static class WebSettings
    {
        public static IServiceCollection WebConfiguration(this IServiceCollection services)
        {
            services.AddSingleton(new EnglishTaskWebMapper());
            
            return services;
        }
    }
}