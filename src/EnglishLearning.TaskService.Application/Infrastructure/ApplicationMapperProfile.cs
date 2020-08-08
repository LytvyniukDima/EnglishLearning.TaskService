using AutoMapper;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.Filtering;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Application.Infrastructure
{
    public class ApplicationMapperProfile : Profile
    {
        public ApplicationMapperProfile()
        {
            CreateMap<EnglishTaskCreateModel, EnglishTask>();
            
            CreateMap<EnglishTaskModel, EnglishTask>();
            CreateMap<EnglishTask, EnglishTaskModel>();
            
            CreateMap<EnglishTask, EnglishTaskInfoModel>();
            CreateMap<EnglishTaskInfo, EnglishTaskInfoModel>();

            CreateMap<UserInformation, UserInformationModel>();
            CreateMap<UserInformationModel, UserInformation>();

            CreateMap<BaseFilterModel, BaseFilter>();

            CreateMap<EnglishTaskFullFilter, EnglishTaskFullFilterModel>();

            CreateMap<PerEnglishLevelTaskInformation, PerEnglishLevelTaskInformationModel>();
            CreateMap<PerEnglishLevelTaskInformationModel, PerEnglishLevelTaskInformation>();
        }
    }
}
