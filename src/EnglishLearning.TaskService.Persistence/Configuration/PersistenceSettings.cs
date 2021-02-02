using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.TaskService.Persistence.Entities.TextAnalyze;
using EnglishLearning.TaskService.Persistence.Repositories;
using EnglishLearning.Utilities.Configurations.MongoConfiguration;
using EnglishLearning.Utilities.Persistence.Mongo.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

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
                    options.HasIndex<GrammarFileAnalyzed>(index =>
                    {
                        index.CreateOne(
                            new CreateIndexModel<GrammarFileAnalyzed>(
                                Builders<GrammarFileAnalyzed>.IndexKeys.Ascending(x => x.Name),
                                new CreateIndexOptions { Unique = true }));
                    });
                })
                .AddMongoCollectionNamesProvider(x =>
                {
                    x.Add<EnglishTask>("EnglishTasks");
                    x.Add<UserInformation>("UserInformation");
                    x.Add<ParsedSent>("ParsedSents");
                    x.Add<GrammarFileAnalyzed>("GrammarFileAnalyzed");
                });
            
            services.AddTransient<IEnglishTaskRepository, EnglishTaskMongoDbRepository>();
            services.AddTransient<IUserInformationRepository, UserInformationMongoRepository>();
            services.AddTransient<IEnglishTaskFilterOptionsRepository, EnglishTaskFilterOptionsRepository>();
            services.AddTransient<IParsedSentRepository, ParsedSentRepository>();
            services.AddTransient<IGrammarFileAnalyzedRepository, GrammarFileAnalyzedRepository>();
            
            return services;
        }
    }
}
