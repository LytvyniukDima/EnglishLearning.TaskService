using EnglishLearning.TaskService.EventHandlers.Configuration;
using EnglishLearning.Utilities.MessageBrokers.Contracts.Users;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.TaskService.Host.Infrastructure
{
    public static class MessageBrokerExtensions
    {
        public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEventHandlerConfiguration();

            services.AddKafka(configuration, x =>
            {
                x.AddConsumer(options => options.AddTopic<UserCreatedEvent>());
                x.UseProtoBufSerializer();
            });

            return services;
        }
    }
}
