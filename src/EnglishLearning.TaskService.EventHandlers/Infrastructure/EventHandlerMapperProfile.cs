using AutoMapper;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;
using EnglishLearning.Utilities.MessageBrokers.Contracts.TextAnalyze;
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

            CreateMap<SentTokenContract, SentTokenModel>();
        }
    }
}
