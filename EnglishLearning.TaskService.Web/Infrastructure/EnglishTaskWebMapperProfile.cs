using AutoMapper;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Web.ViewModels;
using EnglishLearning.TaskService.Web.ViewModels.Parameters;

namespace EnglishLearning.TaskService.Web.Infrastructure
{
    public class EnglishTaskWebMapperProfile : Profile
    {
        public EnglishTaskWebMapperProfile()
        {
            CreateMap<EnglishTaskCreateViewModel, EnglishTaskCreateModel>();

            CreateMap<EnglishTaskInfoModel, EnglishTaskInfoViewModel>();
            CreateMap<EnglishTaskModel, EnglishTaskViewModel>();

            CreateMap<BaseFilterParameters, BaseFilterModel>();
        }
    }
}
