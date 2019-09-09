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
    [Route("/api/tasks/info")]
    public class InfoEnglishTaskController : Controller
    {
        private readonly IEnglishTaskService _englishTaskService;
        private readonly IMapper _mapper;

        public InfoEnglishTaskController(IEnglishTaskService englishTaskService, EnglishTaskWebMapper englishTaskWebMapper)
        {
            _englishTaskService = englishTaskService;
            _mapper = englishTaskWebMapper.Mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllInfo([FromQuery] bool withUserPreferences)
        {
            IEnumerable<EnglishTaskInfoDto> englishTaskDtos = await _englishTaskService.GetAllEnglishTaskInfoAsync();
            var englishTaskModels = _mapper.Map<IEnumerable<EnglishTaskInfoDto>, IEnumerable<EnglishTaskInfoModel>>(englishTaskDtos);

            return Ok(englishTaskModels);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoById(string id)
        {
            EnglishTaskDto englishTask = await _englishTaskService.GetByIdEnglishTaskAsync(id);
            if (englishTask == null)
            {
                return NotFound();
            }

            var englishTaskModel = _mapper.Map<EnglishTaskDto, EnglishTaskInfoModel>(englishTask);
            
            return Ok(englishTaskModel);
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult> GetAllInfoByFilter([FromQuery] BaseFilterParameters parameters)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(parameters);
            IEnumerable<EnglishTaskInfoDto> englishTakDtos = await _englishTaskService.FindAllInfoEnglishTaskAsync(filterModel);
            if (!englishTakDtos.Any())
            {
                return NotFound();
            }

            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskInfoModel>>(englishTakDtos);
            
            return Ok(englishTaskModels);
        }
    }
}
