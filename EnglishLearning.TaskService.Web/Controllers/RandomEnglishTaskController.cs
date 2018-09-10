using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("api/tasks/random")]
    public class RandomEnglishTaskController : Controller
    {
        private readonly IRandomEnglishTaskService _randomEnglishTaskService;
        private readonly IMapper _mapper;

        public RandomEnglishTaskController(IRandomEnglishTaskService randomEnglishTaskService,
            EnglishTaskWebMapper englishTaskWebMapper)
        {
            _randomEnglishTaskService = randomEnglishTaskService;
            _mapper = englishTaskWebMapper.Mapper;
        }
        
        
    }
}