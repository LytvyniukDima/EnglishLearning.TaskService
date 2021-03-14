using AutoMapper;

namespace EnglishLearning.TaskService.ExternalServices.Infrastructure
{
    public class ExternalServicesMapper
    {
        public ExternalServicesMapper()
        {
            Mapper = new MapperConfiguration(x => x
                .AddProfile(new ExternalServicesMapperProfile()))
             .CreateMapper();
        }

        public IMapper Mapper { get; }
    }
}