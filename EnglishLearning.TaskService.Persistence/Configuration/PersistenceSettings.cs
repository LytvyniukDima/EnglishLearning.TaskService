using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.TaskService.Persistence.Repositories;
using EnglishLearning.Utilities.Configurations.MongoConfiguration;
using EnglishLearning.Utilities.Persistence.Mongo.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;

namespace EnglishLearning.TaskService.Persistence.Configuration
{
    public static class PersistenceSettings
    {
        public static IServiceCollection PersistenceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMongoConfiguration(configuration)
                .AddMongoContext(options =>
                {
                    options.CollectionSettings(settings =>
                    { 
                        settings.GuidRepresentation = GuidRepresentation.Standard;
                    });
                })
                .AddMongoCollectionNamesProvider(x =>
                {
                    x.Add<EnglishTask>("EnglishTasks");
                    x.Add<UserInformation>("UserInformation");
                });
            
            services.AddTransient<IEnglishTaskRepository, EnglishTaskMongoDbRepository>();
            services.AddTransient<IUserInformationRepository, UserInformationMongoRepository>();
            
            return services;
        }
    }
}
