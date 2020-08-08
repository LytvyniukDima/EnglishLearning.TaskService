using AutoMapper;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.Filtering;
using EnglishLearning.TaskService.Web.ViewModels;
using EnglishLearning.TaskService.Web.ViewModels.Parameters;
using EnglishLearning.Utilities.Linq.Extensions;

namespace EnglishLearning.TaskService.Web.Infrastructure
{
    public class WebMapperProfile : Profile
    {
        public WebMapperProfile()
        {
            CreateMap<EnglishTaskCreateViewModel, EnglishTaskCreateModel>();

            CreateMap<EnglishTaskInfoModel, EnglishTaskInfoViewModel>();
            CreateMap<EnglishTaskModel, EnglishTaskViewModel>();
            CreateMap<EnglishTaskModel, EnglishTaskInfoViewModel>();
            
            CreateMap<BaseFilterParameters, BaseFilterModel>();

            CreateMap<EnglishTaskFullFilterModel, EnglishTaskFullFilterViewModel>()
                .ForMember(
                    x => x.EnglishLevelFilterOptions,
                    opt => opt.MapFrom(x => x.EnglishLevelFilterOptions.ConvertToStringKeyDictionary()))
                .ForMember(
                    x => x.TaskTypeFilterOptions,
                    opt => opt.MapFrom(x => x.TaskTypeFilterOptions.ConvertToStringKeyDictionary()));
        }
    }
}
