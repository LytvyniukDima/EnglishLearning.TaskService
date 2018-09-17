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
        
        [AllowAnonymous]
        [HttpGet("/filter")]
        public async Task<ActionResult> FindRandomInfoTaskByFilter(
            [FromQuery] string[] tasktypes, 
            [FromQuery] string[] grammarParts, 
            [FromQuery] string[] englishLevels)
        {
            EnglishTaskInfoDto englishTakDto = await _randomEnglishTaskService.FindRandomInfoEnglishTaskAsync(tasktypes, grammarParts, englishLevels);
            if (englishTakDto == null)
                return NotFound();

            var englishTaskModel = _mapper.Map<EnglishTaskInfoModel>(englishTakDto);
            
            return Ok(englishTaskModel);
        }
        
        [AllowAnonymous]
        [HttpGet("{count}")]
        public async Task<ActionResult> GetRandomInfoFromAll(int count)
        {
            IEnumerable<EnglishTaskInfoDto> englishTakDtos = await _randomEnglishTaskService.GetRandomInfoFromAllEnglishTask(count);
            if (!englishTakDtos.Any())
                return NotFound();

            var englishTaskModel = _mapper.Map<IEnumerable<EnglishTaskInfoModel>>(englishTakDtos);
            
            return Ok(englishTaskModel);
        }
        
        [AllowAnonymous]
        [HttpGet("/{count}/filter")]
        public async Task<ActionResult> FindRandomInfoTaskByFilter(
            int count,
            [FromQuery] string[] tasktypes, 
            [FromQuery] string[] grammarParts, 
            [FromQuery] string[] englishLevels)
        {
            IEnumerable<EnglishTaskInfoDto> englishTakDtos = await _randomEnglishTaskService.FindRandomInfoCountEnglishTask(count, tasktypes, grammarParts, englishLevels);
            if (!englishTakDtos.Any())
                return NotFound();

            var englishTaskModels = _mapper.Map<IEnumerable<EnglishTaskInfoModel>>(englishTakDtos);
            
            return Ok(englishTaskModels);
        }
    }
}