using EnglishLearning.TaskService.EventHandlers.Handlers;
using EnglishLearning.TaskService.EventHandlers.Infrastructure;
using EnglishLearning.Utilities.MessageBrokers.Contracts.Users;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.TaskService.EventHandlers.Configuration
{
    public static class EventHandlerSettings
    {
        public static IServiceCollection AddEventHandlerConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IKafkaMessageHandler<UserCreatedEvent>, UserMessageHandler>();
            services.AddTransient<UserMessageHandler>();

            services.AddSingleton<EventHandlerMapper>();
            
            return services;
        }
    }
}
