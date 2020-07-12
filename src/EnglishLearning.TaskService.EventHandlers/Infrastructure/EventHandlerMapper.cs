using AutoMapper;

namespace EnglishLearning.TaskService.EventHandlers.Infrastructure
{
    public class EventHandlerMapper
    {
        public EventHandlerMapper()
        {
            Mapper = new MapperConfiguration(x => x
                    .AddProfile(new EventHandlerMapperProfile()))
                .CreateMapper();
        }

        public IMapper Mapper { get; }
    }
}