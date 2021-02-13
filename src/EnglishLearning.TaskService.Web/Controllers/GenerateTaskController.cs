using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract.TaskGeneration;
using EnglishLearning.TaskService.Application.Models.TaskGeneration;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.ViewModels;
using EnglishLearning.Utilities.Identity.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("/api/tasks/generate")]
    public class GenerateTaskController : Controller
    {
        private readonly ITaskGenerationService _taskGenerationService;

        private readonly IMapper _mapper;

        public GenerateTaskController(
            ITaskGenerationService taskGenerationService,
            WebMapper webMapper)
        {
            _taskGenerationService = taskGenerationService;
            _mapper = webMapper.Mapper;
        }

        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GenerateTaskViewModel generateTask)
        {
            var generateTaskModel = _mapper.Map<GenerateTaskModel>(generateTask);

            await _taskGenerationService.GenerateTasksAsync(generateTaskModel);

            return Ok();
        }
    }
}