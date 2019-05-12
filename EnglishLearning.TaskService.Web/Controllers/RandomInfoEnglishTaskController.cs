using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("/api/tasks/info/random")]
    public class RandomInfoEnglishTaskController : Controller
    {
        private readonly IRandomEnglishTaskService _randomEnglishTaskService;
        private readonly IMapper _mapper;

        public RandomInfoEnglishTaskController(IRandomEnglishTaskService randomEnglishTaskService,
            EnglishTaskWebMapper englishTaskWebMapper)
        {
            _randomEnglishTaskService = randomEnglishTaskService;
            _mapper = englishTaskWebMapper.Mapper;
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult> FindRandomInfoTaskByFilter(
            [FromQuery] string[] taskType, 
            [FromQuery] string[] grammarPart, 
            [FromQuery] string[] englishLevel)
        {
            EnglishTaskInfoDto englishTakDto = await _randomEnglishTaskService.FindRandomInfoEnglishTaskAsync(taskType, grammarPart, englishLevel);
            if (englishTakDto == null)
                return NotFound();

            var englishTaskModel = _mapper.Map<EnglishTaskInfoModel>(englishTakDto);
            
            return Ok(englishTaskModel);
        }
        
        [HttpGet("{count}")]
        public async Task<ActionResult> GetRandomInfoFromAll(int count)
        {
            IEnumerable<EnglishTaskInfoDto> englishTakDtos = await _randomEnglishTaskService.GetRandomInfoFromAllEnglishTask(count);
            if (!englishTakDtos.Any())
                return NotFound();

            var englishTaskModel = _mapper.Map<IEnumerable<EnglishTaskInfoModel>>(englishTakDtos);
            
            return Ok(englishTaskModel);
        }
        
        [HttpGet("{count}/filter")]
        public async Task<ActionResult> FindRandomInfoTaskByFilter(
            int count,
            [FromQuery] string[] taskType, 
            [FromQuery] string[] grammarPart, 
            [FromQuery] string[] englishLevel)
        {
            IEnumerable<EnglishTaskInfoDto> englishTakDtos = await _randomEnglishTaskService.FindRandomInfoCountEnglishTask(count, taskType, grammarPart, englishLevel);
            if (!englishTakDtos.Any())
                return NotFound();

            var englishTaskModels = _mapper.Map<IEnumerable<EnglishTaskInfoModel>>(englishTakDtos);
            
            return Ok(englishTaskModels);
        }
    }
}
