using System.Collections.Generic;
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
    [Route("api/englishtask/info")]
    public class InfoEnglishTaskController : Controller
    {
        private readonly IEnglishTaskService _englishTaskService;
        private readonly IFilterEnglishTaskService _filterEnglishTaskService;

        private readonly IMapper _mapper;

        public InfoEnglishTaskController(
            IEnglishTaskService englishTaskService,
            IFilterEnglishTaskService filterEnglishTaskService,
            EnglishTaskWebMapper englishTaskWebMapper)
        {
            _englishTaskService = englishTaskService;
            _filterEnglishTaskService = filterEnglishTaskService;
            _mapper = englishTaskWebMapper.Mapper;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<EnglishTaskInfoDto> englishTaskDtos = await _englishTaskService.GetAllEnglishTaskInfoAsync();
            var englishTaskModels = _mapper.Map<IEnumerable<EnglishTaskInfoDto>, IEnumerable<EnglishTaskInfoModel>>(englishTaskDtos);

            return Ok(englishTaskModels);
        }
        
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            EnglishTaskDto englishTask = await _englishTaskService.GetByIdEnglishTaskAsync(id);
            if (englishTask == null)
                return NotFound();

            var englishTaskModel = _mapper.Map<EnglishTaskDto, EnglishTaskModel>(englishTask);
            
            return Ok(englishTaskModel);
        }
    }
}