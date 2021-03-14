using AutoMapper;
using EnglishLearning.Dictionary.Web.Contracts.Metadata;
using EnglishLearning.TaskService.ExternalServices.Contracts;

namespace EnglishLearning.TaskService.ExternalServices.Infrastructure
{
    public class ExternalServicesMapperProfile : Profile
    {
        public ExternalServicesMapperProfile()
        {
            CreateMap<WordMetadata, WordMetadataContract>();
        }
    }
}