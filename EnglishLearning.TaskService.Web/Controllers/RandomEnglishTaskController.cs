using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
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
        
        [AllowAnonymous]
        [HttpGet("api/tasks/random/filter")]
        public async Task<ActionResult> FindRandomTaskByFilter(
            [FromQuery] string[] tasktypes, 
            [FromQuery] string[] grammarParts, 
            [FromQuery] string[] englishLevels)
        {
            EnglishTaskDto englishTakDto = await _randomEnglishTaskService.FindRandomEnglishTaskAsync(tasktypes, englishLevels, grammarParts);
            if (englishTakDto == null)
                return NotFound();

            var englishTaskModel = _mapper.Map<EnglishTaskModel>(englishTakDto);
            
            return Ok(englishTaskModel);
        }
        
        [AllowAnonymous]
        [HttpGet("api/tasks/info/random/filter")]
        public async Task<ActionResult> FindRandomInfoTaskByFilter(
            [FromQuery] string[] tasktypes, 
            [FromQuery] string[] grammarParts, 
            [FromQuery] string[] englishLevels)
        {
            EnglishTaskInfoDto englishTakDto = await _randomEnglishTaskService.FindRandomInfoEnglishTaskAsync(tasktypes, englishLevels, grammarParts);
            if (englishTakDto == null)
                return NotFound();

            var englishTaskModel = _mapper.Map<EnglishTaskInfoModel>(englishTakDto);
            
            return Ok(englishTaskModel);
        }
        
        [AllowAnonymous]
        [HttpGet("api/tasks/random/{count}")]
        public async Task<ActionResult> GetRandomFromAll(int count)
        {
            IEnumerable<EnglishTaskDto> englishTakDtos = await _randomEnglishTaskService.GetRandomFromAllEnglishTask(count);
            if (!englishTakDtos.Any())
                return NotFound();

            var englishTaskModel = _mapper.Map<IEnumerable<EnglishTaskModel>>(englishTakDtos);
            
            return Ok(englishTaskModel);
        }
        
        [AllowAnonymous]
        [HttpGet("api/tasks/info/random/{count}")]
        public async Task<ActionResult> GetRandomInfoFromAll(int count)
        {
            IEnumerable<EnglishTaskInfoDto> englishTakDtos = await _randomEnglishTaskService.GetRandomInfoFromAllEnglishTask(count);
            if (!englishTakDtos.Any())
                return NotFound();

            var englishTaskModel = _mapper.Map<IEnumerable<EnglishTaskInfoModel>>(englishTakDtos);
            
            return Ok(englishTaskModel);
        }
        
        [AllowAnonymous]
        [HttpGet("api/tasks/random/{count}/filter")]
        public async Task<ActionResult> FindRandomCountTasksByFilter(
            int count,
            [FromQuery] string[] tasktypes, 
            [FromQuery] string[] grammarParts, 
            [FromQuery] string[] englishLevels)
        {
            IEnumerable<EnglishTaskDto> englishTakDtos = await _randomEnglishTaskService.FindRandomCountEnglishTask(count, tasktypes, englishLevels, grammarParts);
            if (!englishTakDtos.Any())
                return NotFound();

            var englishTaskModels = _mapper.Map<EnglishTaskModel>(englishTakDtos);
            
            return Ok(englishTaskModels);
        }
        
        [AllowAnonymous]
        [HttpGet("api/tasks/info/random/{count}/filter")]
        public async Task<ActionResult> FindRandomInfoTaskByFilter(
            int count,
            [FromQuery] string[] tasktypes, 
            [FromQuery] string[] grammarParts, 
            [FromQuery] string[] englishLevels)
        {
            IEnumerable<EnglishTaskInfoDto> englishTakDtos = await _randomEnglishTaskService.FindRandomInfoCountEnglishTask(count, tasktypes, englishLevels, grammarParts);
            if (!englishTakDtos.Any())
                return NotFound();

            var englishTaskModels = _mapper.Map<EnglishTaskInfoModel>(englishTakDtos);
            
            return Ok(englishTaskModels);
        }
    }
}