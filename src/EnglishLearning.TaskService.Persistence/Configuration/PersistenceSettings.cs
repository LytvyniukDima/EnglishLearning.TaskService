﻿using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.TaskService.Persistence.Entities.TextAnalyze;
using EnglishLearning.TaskService.Persistence.Repositories;
using EnglishLearning.Utilities.Configurations.MongoConfiguration;
using EnglishLearning.Utilities.Persistence.Mongo.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EnglishLearning.TaskService.Persistence.Configuration
{
    public static class PersistenceSettings
    {
        public static IServiceCollection PersistenceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // In the future it will be default mode
            BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3; 
            
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
                    
                    options.HasIndex<TaskGeneration>(index =>
                    {
                        index.CreateOne(
                            new CreateIndexModel<TaskGeneration>(
                                Builders<TaskGeneration>.IndexKeys.Ascending(x => x.Name),
                                new CreateIndexOptions { Unique = true }));
                    });
                })
                .AddMongoCollectionNamesProvider(x =>
                {
                    x.Add<EnglishTask>("EnglishTasks");
                    x.Add<UserInformation>("UserInformation");
                    x.Add<ParsedSent>("ParsedSents");
                    x.Add<GrammarFileAnalyzed>("GrammarFileAnalyzed");
                    x.Add<TaskItem>("TaskItems");
                    x.Add<TaskGeneration>("TaskGenerations");
                });
            
            services.AddTransient<IEnglishTaskRepository, EnglishTaskMongoDbRepository>();
            services.AddTransient<IUserInformationRepository, UserInformationMongoRepository>();
            services.AddTransient<IEnglishTaskFilterOptionsRepository, EnglishTaskFilterOptionsRepository>();
            services.AddTransient<IParsedSentRepository, ParsedSentRepository>();
            services.AddTransient<IGrammarFileAnalyzedRepository, GrammarFileAnalyzedRepository>();
            services.AddTransient<ITaskItemRepository, TaskItemRepository>();
            services.AddTransient<ITaskGenerationRepository, TaskGenerationRepository>();
            
            return services;
        }
    }
}
