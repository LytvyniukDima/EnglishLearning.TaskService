using AutoMapper;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Web.Models;

namespace EnglishLearning.TaskService.Web.Infrastructure
{
    public class EnglishTaskWebMapperProfile : Profile
    {
        public EnglishTaskWebMapperProfile()
        {
            CreateMap<EnglishTaskCreateModel, EnglishTaskCreateDto>();

            CreateMap<EnglishTaskInfoDto, EnglishTaskInfoModel>();
            CreateMap<EnglishTaskDto, EnglishTaskModel>();
        }
    }
}