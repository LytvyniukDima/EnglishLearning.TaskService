using AutoMapper;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.Filtering;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.TaskService.Persistence.Entities.TextAnalyze;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace EnglishLearning.TaskService.Application.Infrastructure
{
    public class ApplicationMapperProfile : Profile
    {
        public ApplicationMapperProfile()
        {
            CreateMap<EnglishTaskCreateModel, EnglishTask>();

            CreateMap<EnglishTaskModel, EnglishTask>();
            CreateMap<EnglishTask, EnglishTaskModel>()
                .ForMember(
                    x => x.Content,
                    opt => opt.MapFrom(x => x.Content.ToJson(null, null, null, default(BsonSerializationArgs))));
            
            CreateMap<EnglishTask, EnglishTaskInfoModel>();
            CreateMap<EnglishTaskInfo, EnglishTaskInfoModel>();

            CreateMap<UserInformation, UserInformationModel>();
            CreateMap<UserInformationModel, UserInformation>();

            CreateMap<BaseFilterModel, BaseFilter>();

            CreateMap<EnglishTaskFullFilter, EnglishTaskFullFilterModel>();

            CreateMap<PerEnglishLevelTaskInformation, PerEnglishLevelTaskInformationModel>();
            CreateMap<PerEnglishLevelTaskInformationModel, PerEnglishLevelTaskInformation>();

            CreateMap<ParsedSentModel, ParsedSent>();
            CreateMap<ParsedSent, ParsedSentModel>();
            CreateMap<SentTokenModel, SentToken>();
            CreateMap<SentToken, SentTokenModel>();
        }
    }
}
