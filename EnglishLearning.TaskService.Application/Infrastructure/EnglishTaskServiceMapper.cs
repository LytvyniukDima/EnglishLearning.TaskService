using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;

namespace EnglishLearning.TaskService.Application.Infrastructure
{
    public class EnglishTaskServiceMapper
    {
        public EnglishTaskServiceMapper()
        {
            Mapper = new MapperConfiguration(x => x
                .AddExpressionMapping()
                .AddProfile(new EnglishTaskServiceMapperProfile()))
             .CreateMapper();
        }

        public IMapper Mapper { get; }
    }
}