using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.Models;
using EnglishLearning.TaskService.Web.Models.Parameters;
using EnglishLearning.Utilities.Identity.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("/api/tasks/full")]
    public class EnglishTaskController : Controller
    {
        private readonly IEnglishTaskService _englishTaskService;
        private readonly IMapper _mapper;

        public EnglishTaskController(IEnglishTaskService englishTaskService, EnglishTaskWebMapper englishTaskWebMapper)
        {
            _englishTaskService = englishTaskService;
            _mapper = englishTaskWebMapper.Mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool withUserPreferences)
        {
            IEnumerable<EnglishTaskDto> englishTaskDtos = await _englishTaskService.GetAllEnglishTaskAsync();
            var englishTaskModels = _mapper.Map<IEnumerable<EnglishTaskDto>, IEnumerable<EnglishTaskModel>>(englishTaskDtos);

            return Ok(englishTaskModels);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            EnglishTaskDto englishTask = await _englishTaskService.GetByIdEnglishTaskAsync(id);
            if (englishTask == null)
            {
                return NotFound();
            }

            var englishTaskModel = _mapper.Map<EnglishTaskDto, EnglishTaskModel>(englishTask);
            
            return Ok(englishTaskModel);
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EnglishTaskCreateModel englishTaskCreateModel)
        {
            var englishTaskCreateDto = _mapper.Map<EnglishTaskCreateModel, EnglishTaskCreateDto>(englishTaskCreateModel);
            
            await _englishTaskService.CreateEnglishTaskAsync(englishTaskCreateDto);

            return Ok();
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] EnglishTaskCreateModel englishTaskCreateModel)
        {
            var englishTaskCreateDto = _mapper.Map<EnglishTaskCreateModel, EnglishTaskCreateDto>(englishTaskCreateModel);

            bool result = await _englishTaskService.UpdateEnglishTaskAsync(id, englishTaskCreateDto);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok();
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool result = await _englishTaskService.DeleteByIdEnglishTaskAsync(id);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok();
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            bool result = await _englishTaskService.DeleteAllEnglishTaskAsync();

            if (result == false)
            {
                return BadRequest();
            }

            return Ok();
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult> GetAllByFilter([FromQuery] BaseFilterParameters parameters)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(parameters);
            IReadOnlyList<EnglishTaskDto> englishTakDtos = await _englishTaskService.FindAllEnglishTaskAsync(filterModel);
            if (!englishTakDtos.Any())
            {
                return NotFound();
            }

            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskModel>>(englishTakDtos);
            
            return Ok(englishTaskModels);
        }
    }
}
