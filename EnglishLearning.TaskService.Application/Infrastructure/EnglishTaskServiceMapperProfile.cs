using AutoMapper;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Application.Infrastructure
{
    public class EnglishTaskServiceMapperProfile : Profile
    {
        public EnglishTaskServiceMapperProfile()
        {
            CreateMap<EnglishTaskCreateDto, EnglishTask>();
            
            CreateMap<EnglishTaskDto, EnglishTask>();
            CreateMap<EnglishTask, EnglishTaskDto>();
            
            CreateMap<EnglishTaskInfo, EnglishTaskInfoDto>();

            CreateMap<UserInformation, UserInformationDto>();
            CreateMap<UserInformationDto, UserInformation>();

            CreateMap<BaseFilterModel, BaseFilter>();
        }
    }
}
