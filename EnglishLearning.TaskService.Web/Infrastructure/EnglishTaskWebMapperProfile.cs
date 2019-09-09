using AutoMapper;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Web.Models;
using EnglishLearning.TaskService.Web.Models.Parameters;

namespace EnglishLearning.TaskService.Web.Infrastructure
{
    public class EnglishTaskWebMapperProfile : Profile
    {
        public EnglishTaskWebMapperProfile()
        {
            CreateMap<EnglishTaskCreateModel, EnglishTaskCreateDto>();

            CreateMap<EnglishTaskInfoDto, EnglishTaskInfoModel>();
            CreateMap<EnglishTaskDto, EnglishTaskModel>();

            CreateMap<BaseFilterParameters, BaseFilterModel>();
        }
    }
}
