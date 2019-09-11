using AutoMapper;

namespace EnglishLearning.TaskService.Web.Infrastructure
{
    public class WebMapper
    {
        public WebMapper()
        {
            Mapper = new MapperConfiguration(x => 
                x.AddProfile(new WebMapperProfile()))
            .CreateMapper();
        }
        
        public IMapper Mapper { get; }
    }
}