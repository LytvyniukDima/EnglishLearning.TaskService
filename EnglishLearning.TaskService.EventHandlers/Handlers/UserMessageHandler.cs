using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.EventHandlers.Infrastructure;
using EnglishLearning.Utilities.MessageBrokers.Contracts.Users;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Abstraction;

namespace EnglishLearning.TaskService.EventHandlers.Handlers
{
    public class UserMessageHandler : IKafkaMessageHandler<UserCreatedEvent>
    {
        private readonly IUserInformationService _userInformationService;
        private readonly IMapper _mapper;

        public UserMessageHandler(IUserInformationService userInformationService, EventHandlerMapper handlerMapper)
        {
            _userInformationService = userInformationService;
            _mapper = handlerMapper.Mapper;
        }
        
        public async Task OnMessageAsync(UserCreatedEvent message)
        {
            var applicationModel = _mapper.Map<UserInformationDto>(message);
            await _userInformationService.AddUserInfo(applicationModel);
        }
    }
}
