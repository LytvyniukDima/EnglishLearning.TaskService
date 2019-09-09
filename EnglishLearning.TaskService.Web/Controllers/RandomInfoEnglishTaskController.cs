using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.Models;
using EnglishLearning.TaskService.Web.Models.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("/api/tasks/info/random")]
    public class RandomInfoEnglishTaskController : Controller
    {
        private readonly IRandomEnglishTaskService _randomEnglishTaskService;
        private readonly IMapper _mapper;

        public RandomInfoEnglishTaskController(
            IRandomEnglishTaskService randomEnglishTaskService,
            EnglishTaskWebMapper englishTaskWebMapper)
        {
            _randomEnglishTaskService = randomEnglishTaskService;
            _mapper = englishTaskWebMapper.Mapper;
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult> FindRandomInfoTaskByFilter([FromQuery] BaseFilterParameters parameters)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(parameters);
            EnglishTaskInfoDto englishTakDto = await _randomEnglishTaskService.FindRandomInfoEnglishTaskAsync(filterModel);
            if (englishTakDto == null)
            {
                return NotFound();
            }

            var englishTaskModel = _mapper.Map<EnglishTaskInfoModel>(englishTakDto);
            
            return Ok(englishTaskModel);
        }
        
        [HttpGet("{count}")]
        public async Task<ActionResult> GetRandomInfoFromAll(int count, [FromQuery] bool withUserPreferences)
        {
            IReadOnlyList<EnglishTaskInfoDto> englishTaskDtos;
            if (withUserPreferences)
            {
                englishTaskDtos = await _randomEnglishTaskService.GetRandomInfoWithUserPreferencesEnglishTask(count);
            }
            else
            {
                englishTaskDtos = await _randomEnglishTaskService.GetRandomInfoFromAllEnglishTask(count);
            }
            
            if (!englishTaskDtos.Any())
            {
                return NotFound();
            }

            var englishTaskModel = _mapper.Map<IReadOnlyList<EnglishTaskInfoModel>>(englishTaskDtos);
            
            return Ok(englishTaskModel);
        }
        
        [HttpGet("{count}/filter")]
        public async Task<ActionResult> FindRandomInfoTaskByFilter(
            int count,
            [FromQuery] BaseFilterParameters parameters)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(parameters);
            IReadOnlyList<EnglishTaskInfoDto> englishTakDtos = await _randomEnglishTaskService.FindRandomInfoCountEnglishTask(count, filterModel);
            if (!englishTakDtos.Any())
            {
                return NotFound();
            }

            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskInfoModel>>(englishTakDtos);
            
            return Ok(englishTaskModels);
        }
    }
}
