using AutoMapper;

namespace EnglishLearning.TaskService.Application.Infrastructure
{
    public class EnglishTaskServiceMapper
    {
        public EnglishTaskServiceMapper()
        {
            Mapper = new MapperConfiguration(x => x.AddProfile(new EnglishTaskServiceMapperProfile()))
                .CreateMapper();
        }

        public IMapper Mapper { get; }
    }
}