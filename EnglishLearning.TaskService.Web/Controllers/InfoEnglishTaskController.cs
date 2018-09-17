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
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllInfo()
        {
            IEnumerable<EnglishTaskInfoDto> englishTaskDtos = await _englishTaskService.GetAllEnglishTaskInfoAsync();
            var englishTaskModels = _mapper.Map<IEnumerable<EnglishTaskInfoDto>, IEnumerable<EnglishTaskInfoModel>>(englishTaskDtos);

            return Ok(englishTaskModels);
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoById(string id)
        {
            EnglishTaskDto englishTask = await _englishTaskService.GetByIdEnglishTaskAsync(id);
            if (englishTask == null)
                return NotFound();

            var englishTaskModel = _mapper.Map<EnglishTaskDto, EnglishTaskModel>(englishTask);
            
            return Ok(englishTaskModel);
        }
        
        [AllowAnonymous]
        [HttpGet("/filter")]
        public async Task<ActionResult> GetAllInfoByFilter(
            [FromQuery] string[] tasktype, 
            [FromQuery] string[] grammarPart, 
            [FromQuery] string[] englishLevel)
        {
            IEnumerable<EnglishTaskInfoDto> englishTakDtos = await _englishTaskService.FindAllInfoEnglishTaskAsync(tasktype, grammarPart, englishLevel);
            if (!englishTakDtos.Any())
                return NotFound();

            var englishTaskModels = _mapper.Map<IEnumerable<EnglishTaskModel>>(englishTakDtos);
            
            return Ok(englishTaskModels);
        }
    }
}