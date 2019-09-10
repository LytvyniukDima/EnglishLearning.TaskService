using AutoMapper;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Application.Infrastructure
{
    public class EnglishTaskServiceMapperProfile : Profile
    {
        public EnglishTaskServiceMapperProfile()
        {
            CreateMap<EnglishTaskCreateModel, EnglishTask>();
            
            CreateMap<EnglishTaskModel, EnglishTask>();
            CreateMap<EnglishTask, EnglishTaskModel>();
            
            CreateMap<EnglishTaskInfo, EnglishTaskInfoModel>();

            CreateMap<UserInformation, UserInformationModel>();
            CreateMap<UserInformationModel, UserInformation>();

            CreateMap<BaseFilterModel, BaseFilter>();
        }
    }
}
