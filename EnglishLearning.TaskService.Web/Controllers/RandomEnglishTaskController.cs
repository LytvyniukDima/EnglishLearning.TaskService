using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.ViewModels;
using EnglishLearning.TaskService.Web.ViewModels.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("/api/tasks/full/random")]
    public class RandomEnglishTaskController : Controller
    {
        private readonly IRandomEnglishTaskService _randomEnglishTaskService;
        private readonly IMapper _mapper;

        public RandomEnglishTaskController(
            IRandomEnglishTaskService randomEnglishTaskService,
            EnglishTaskWebMapper englishTaskWebMapper)
        {
            _randomEnglishTaskService = randomEnglishTaskService;
            _mapper = englishTaskWebMapper.Mapper;
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult> FindRandomTaskByFilter([FromQuery] BaseFilterParameters parameters)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(parameters);
            EnglishTaskDto englishTakDto = await _randomEnglishTaskService.FindRandomEnglishTaskAsync(filterModel);
            if (englishTakDto == null)
            {
                return NotFound();
            }

            var englishTaskModel = _mapper.Map<EnglishTaskViewModel>(englishTakDto);
            
            return Ok(englishTaskModel);
        }
        
        [HttpGet("{count}")]
        public async Task<ActionResult> GetRandomFromAll(int count, [FromQuery] bool withUserPreferences)
        {
            IReadOnlyList<EnglishTaskDto> englishTaskDtos;
            if (withUserPreferences)
            {
                englishTaskDtos = await _randomEnglishTaskService.GetRandomWithUserPreferencesEnglishTask(count);
            }
            else
            {
                englishTaskDtos = await _randomEnglishTaskService.GetRandomFromAllEnglishTask(count);
            }
            
            if (!englishTaskDtos.Any())
            {
                return NotFound();
            }

            var englishTaskModel = _mapper.Map<IEnumerable<EnglishTaskViewModel>>(englishTaskDtos);
            
            return Ok(englishTaskModel);
        }
        
        [HttpGet("{count}/filter")]
        public async Task<ActionResult> FindRandomCountTasksByFilter(
            int count, 
            [FromQuery] BaseFilterParameters parameters)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(parameters);
            IEnumerable<EnglishTaskDto> englishTakDtos = await _randomEnglishTaskService.FindRandomCountEnglishTask(count, filterModel);
            if (!englishTakDtos.Any())
            {
                return NotFound();
            }

            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskViewModel>>(englishTakDtos);
            
            return Ok(englishTaskModels);
        }
    }
}
