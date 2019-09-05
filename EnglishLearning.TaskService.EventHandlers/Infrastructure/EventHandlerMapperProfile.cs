using AutoMapper;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.MessageBrokers.Contracts.Users;

namespace EnglishLearning.TaskService.EventHandlers.Infrastructure
{
    public class EventHandlerMapperProfile : Profile
    {
        public EventHandlerMapperProfile()
        {
            CreateMap<UserCreatedEvent, UserInformationDto>()
                .ForMember(
                    x => x.FavouriteGrammarParts,
                    m => m.MapFrom(s => s.UserEnglishTaskPreferences.GrammarParts));
        }
    }
}
