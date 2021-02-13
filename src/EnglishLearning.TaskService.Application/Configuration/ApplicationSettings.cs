using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Abstract.TaskGeneration;
using EnglishLearning.TaskService.Application.Abstract.TextAnalyze;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Services;
using EnglishLearning.TaskService.Application.Services.TaskGeneration;
using EnglishLearning.TaskService.Application.Services.TextAnalyze;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.TaskService.Application.Configuration
{
    public static class ApplicationSettings
    {
        public static IServiceCollection ApplicationConfiguration(this IServiceCollection services)
        {
            services.AddSingleton(new ApplicationMapper());

            services.AddTransient<IEnglishTaskService, EnglishTaskService>();
            services.AddTransient<IRandomEnglishTaskService, RandomEnglishTaskService>();
            services.AddTransient<IUserInformationService, UserInformationService>();
            services.AddTransient<IEnglishTaskFilterOptionsService, EnglishTaskFilterOptionsService>();
            services.AddTransient<IStoredTaskInformationAggregateService, StoredTaskInformationAggregateService>();
            services.AddTransient<IUserFilterService, UserFilterService>();
            services.AddTransient<IParsedSentService, ParsedSentService>();
            services.AddTransient<IGrammarFileAnalyzedService, GrammarFileAnalyzedService>();

            services.AddTransient<ITaskGenerationService, TaskGenerationService>();
            
            return services;
        }
    }
}
