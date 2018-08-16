using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Contexts;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.TaskService.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.TaskService.Persistence.Configuration
{
    public static class PersistenceSettings
    {
        public static IServiceCollection PersistenceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbConfiguration>(configuration.GetSection("MongoDbConfiguration"));

            services.AddScoped<ITaskDbContext, TaskDbContext>();
            services.AddTransient<IMongoDbRepository<EnglishTask>, EnglishTaskMongoDbRepository>();
            
            return services;
        }
    }
}