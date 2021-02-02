using EnglishLearning.TaskService.EventHandlers.Contracts.TextAnalyze;
using EnglishLearning.TaskService.EventHandlers.Handlers;
using EnglishLearning.TaskService.EventHandlers.Infrastructure;
using EnglishLearning.Utilities.MessageBrokers.Contracts.Users;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.TaskService.EventHandlers.Configuration
{
    public static class EventHandlerSettings
    {
        public static IServiceCollection AddEventHandlerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageHandler<UserCreatedEvent, UserMessageHandler>();
            services.AddMessageHandler<GrammarTextAnalyzedEvent, TextAnalyzeMessageHandler>();
            services.AddMessageHandler<GrammarFileAnalyzedEvent, TextAnalyzeMessageHandler>();
            
            services.AddSingleton<EventHandlerMapper>();

            services.AddMessageBroker(configuration);
            
            return services;
        }
        
        private static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddKafka(configuration, x =>
            {
                x.AddConsumer(options => options.AddTopic<UserCreatedEvent>());
                x.AddConsumer(options =>
                {
                    options.AddTopic<GrammarTextAnalyzedEvent>();
                    options.PartitionCount = 2;
                    options.UseJsonSerializer<GrammarTextAnalyzedEvent>();
                });
                x.AddConsumer(options =>
                {
                    options.AddTopic<GrammarFileAnalyzedEvent>();
                    options.UseJsonSerializer<GrammarFileAnalyzedEvent>();
                });
                x.UseProtoBufAsDefaultSerializer();
            });

            return services;
        }
    }
}
