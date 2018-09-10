using AutoMapper;

namespace EnglishLearning.TaskService.Web.Infrastructure
{
    public class EnglishTaskWebMapper
    {
        public EnglishTaskWebMapper()
        {
            Mapper = new MapperConfiguration(x => 
                x.AddProfile(new EnglishTaskWebMapperProfile()))
            .CreateMapper();
        }
        
        public IMapper Mapper { get; }
    }
}