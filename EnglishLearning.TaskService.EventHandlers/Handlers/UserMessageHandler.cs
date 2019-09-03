using System.Threading.Tasks;
using EnglishLearning.Utilities.MessageBrokers.Contracts.Users;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Abstraction;

namespace EnglishLearning.TaskService.EventHandlers.Handlers
{
    public class UserMessageHandler: IKafkaMessageHandler<UserCreatedEvent>
    {
        public async Task OnMessageAsync(UserCreatedEvent message)
        {
            throw new System.NotImplementedException();
        }
    }
}
