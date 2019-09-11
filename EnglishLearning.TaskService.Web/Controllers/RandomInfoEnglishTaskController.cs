using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.ViewModels;
using EnglishLearning.TaskService.Web.ViewModels.Parameters;
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
            WebMapper englishTaskWebMapper)
        {
            _randomEnglishTaskService = randomEnglishTaskService;
            _mapper = englishTaskWebMapper.Mapper;
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult> FindRandomInfoTaskByFilter([FromQuery] BaseFilterParameters parameters)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(parameters);
            EnglishTaskInfoModel englishTakModel = await _randomEnglishTaskService.FindRandomInfoEnglishTaskAsync(filterModel);
            if (englishTakModel == null)
            {
                return NotFound();
            }

            var englishTaskModel = _mapper.Map<EnglishTaskInfoViewModel>(englishTakModel);
            
            return Ok(englishTaskModel);
        }
        
        [HttpGet("{count}")]
        public async Task<ActionResult> GetRandomInfoFromAll(int count, [FromQuery] bool withUserPreferences)
        {
            IReadOnlyList<EnglishTaskInfoModel> englishTaskModels;
            if (withUserPreferences)
            {
                englishTaskModels = await _randomEnglishTaskService.GetRandomInfoWithUserPreferencesEnglishTask(count);
            }
            else
            {
                englishTaskModels = await _randomEnglishTaskService.GetRandomInfoFromAllEnglishTask(count);
            }
            
            if (!englishTaskModels.Any())
            {
                return NotFound();
            }

            var englishTaskModel = _mapper.Map<IReadOnlyList<EnglishTaskInfoViewModel>>(englishTaskModels);
            
            return Ok(englishTaskModel);
        }
        
        [HttpGet("{count}/filter")]
        public async Task<ActionResult> FindRandomInfoTaskByFilter(
            int count,
            [FromQuery] BaseFilterParameters parameters)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(parameters);
            IReadOnlyList<EnglishTaskInfoModel> englishTakModels = await _randomEnglishTaskService.FindRandomInfoCountEnglishTask(count, filterModel);
            if (!englishTakModels.Any())
            {
                return NotFound();
            }

            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskInfoViewModel>>(englishTakModels);
            
            return Ok(englishTaskModels);
        }
    }
}
