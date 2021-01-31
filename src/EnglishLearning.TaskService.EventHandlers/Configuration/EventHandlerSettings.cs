using EnglishLearning.TaskService.EventHandlers.Handlers;
using EnglishLearning.TaskService.EventHandlers.Infrastructure;
using EnglishLearning.Utilities.MessageBrokers.Contracts.TextAnalyze;
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
                    options.UseJsonSerializer<GrammarTextAnalyzedEvent>();
                });
                x.UseProtoBufAsDefaultSerializer();
            });

            return services;
        }
    }
}
