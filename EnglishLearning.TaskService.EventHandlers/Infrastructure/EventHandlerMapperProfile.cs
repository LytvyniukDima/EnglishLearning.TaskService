using AutoMapper;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.Utilities.MessageBrokers.Contracts.Users;

namespace EnglishLearning.TaskService.EventHandlers.Infrastructure
{
    public class EventHandlerMapperProfile : Profile
    {
        public EventHandlerMapperProfile()
        {
            CreateMap<UserCreatedEvent, UserInformationModel>()
                .ForMember(
                    x => x.FavouriteGrammarParts,
                    m => m.MapFrom(s => s.UserEnglishTaskPreferences.GrammarParts));
        }
    }
}
