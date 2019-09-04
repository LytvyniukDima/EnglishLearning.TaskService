using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.Models;
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
        public async Task<ActionResult> FindRandomTaskByFilter(
            [FromQuery] string[] grammarPart,
            [FromQuery] TaskType[] taskType,
            [FromQuery] EnglishLevel[] englishLevel)
        {
            EnglishTaskDto englishTakDto = await _randomEnglishTaskService.FindRandomEnglishTaskAsync(grammarPart, taskType, englishLevel);
            if (englishTakDto == null)
            {
                return NotFound();
            }

            var englishTaskModel = _mapper.Map<EnglishTaskModel>(englishTakDto);
            
            return Ok(englishTaskModel);
        }
        
        [HttpGet("{count}")]
        public async Task<ActionResult> GetRandomFromAll(int count)
        {
            IEnumerable<EnglishTaskDto> englishTakDtos = await _randomEnglishTaskService.GetRandomFromAllEnglishTask(count);
            if (!englishTakDtos.Any())
            {
                return NotFound();
            }

            var englishTaskModel = _mapper.Map<IEnumerable<EnglishTaskModel>>(englishTakDtos);
            
            return Ok(englishTaskModel);
        }
        
        [HttpGet("{count}/filter")]
        public async Task<ActionResult> FindRandomCountTasksByFilter(
            int count,
            [FromQuery] string[] grammarPart,
            [FromQuery] TaskType[] taskType,
            [FromQuery] EnglishLevel[] englishLevel)
        {
            IEnumerable<EnglishTaskDto> englishTakDtos = await _randomEnglishTaskService.FindRandomCountEnglishTask(count, grammarPart, taskType, englishLevel);
            if (!englishTakDtos.Any())
            {
                return NotFound();
            }

            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskModel>>(englishTakDtos);
            
            return Ok(englishTaskModels);
        }
    }
}
