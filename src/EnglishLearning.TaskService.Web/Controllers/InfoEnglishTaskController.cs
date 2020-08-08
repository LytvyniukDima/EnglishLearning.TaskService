using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.Filtering;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.ViewModels;
using EnglishLearning.TaskService.Web.ViewModels.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("/api/tasks/info")]
    public class InfoEnglishTaskController : Controller
    {
        private readonly IEnglishTaskService _englishTaskService;
        private readonly IMapper _mapper;

        public InfoEnglishTaskController(IEnglishTaskService englishTaskService, WebMapper englishTaskWebMapper)
        {
            _englishTaskService = englishTaskService;
            _mapper = englishTaskWebMapper.Mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllInfo([FromQuery] bool withUserPreferences)
        {
            IReadOnlyList<EnglishTaskInfoModel> englishTaskModels;
            if (withUserPreferences)
            {
                englishTaskModels = await _englishTaskService.GetAllEnglishTaskInfoWithUserPreferencesAsync();
            }
            else
            {
                englishTaskModels = await _englishTaskService.GetAllEnglishTaskInfoAsync();
            }
            
            var englishTaskViewModels = _mapper.Map<IEnumerable<EnglishTaskInfoModel>, IEnumerable<EnglishTaskInfoViewModel>>(englishTaskModels);

            return Ok(englishTaskViewModels);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoById(string id)
        {
            EnglishTaskModel englishTask = await _englishTaskService.GetByIdEnglishTaskAsync(id);
            if (englishTask == null)
            {
                return NotFound();
            }

            var englishTaskModel = _mapper.Map<EnglishTaskInfoViewModel>(englishTask);
            
            return Ok(englishTaskModel);
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult> GetAllInfoByFilter([FromQuery] BaseFilterParameters parameters)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(parameters);
            IEnumerable<EnglishTaskInfoModel> englishTakModels = await _englishTaskService.FindAllInfoEnglishTaskAsync(filterModel);
            if (!englishTakModels.Any())
            {
                return NotFound();
            }

            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskInfoViewModel>>(englishTakModels);
            
            return Ok(englishTaskModels);
        }
    }
}
