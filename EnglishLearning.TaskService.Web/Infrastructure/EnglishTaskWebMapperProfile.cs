using AutoMapper;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Web.ViewModels;
using EnglishLearning.TaskService.Web.ViewModels.Parameters;

namespace EnglishLearning.TaskService.Web.Infrastructure
{
    public class EnglishTaskWebMapperProfile : Profile
    {
        public EnglishTaskWebMapperProfile()
        {
            CreateMap<EnglishTaskCreateViewModel, EnglishTaskCreateDto>();

            CreateMap<EnglishTaskInfoDto, EnglishTaskInfoViewModel>();
            CreateMap<EnglishTaskDto, EnglishTaskViewModel>();

            CreateMap<BaseFilterParameters, BaseFilterModel>();
        }
    }
}
