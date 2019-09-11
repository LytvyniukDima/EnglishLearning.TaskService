using AutoMapper;

namespace EnglishLearning.TaskService.Application.Infrastructure
{
    public class ApplicationMapper
    {
        public ApplicationMapper()
        {
            Mapper = new MapperConfiguration(x => x
                .AddProfile(new ApplicationMapperProfile()))
             .CreateMapper();
        }

        public IMapper Mapper { get; }
    }
}