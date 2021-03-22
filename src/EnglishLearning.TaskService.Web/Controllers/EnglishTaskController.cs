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
using EnglishLearning.Utilities.Identity.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("/api/tasks/full")]
    public class EnglishTaskController : Controller
    {
        private readonly IEnglishTaskService _englishTaskService;
        private readonly IEnglishTaskCreateService _englishTaskCreateService;
        private readonly IMapper _mapper;

        public EnglishTaskController(
            IEnglishTaskService englishTaskService,
            IEnglishTaskCreateService englishTaskCreateService,
            WebMapper englishTaskWebMapper)
        {
            _englishTaskService = englishTaskService;
            _englishTaskCreateService = englishTaskCreateService;
            _mapper = englishTaskWebMapper.Mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnglishTaskViewModel>>> Get()
        {
            IEnumerable<EnglishTaskModel> englishTaskModels = await _englishTaskService.GetAllEnglishTaskAsync();
            var englishTaskViewModels = _mapper.Map<IEnumerable<EnglishTaskModel>, IEnumerable<EnglishTaskViewModel>>(englishTaskModels);

            return Ok(englishTaskViewModels);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            EnglishTaskModel englishTask = await _englishTaskService.GetByIdEnglishTaskAsync(id);
            if (englishTask == null)
            {
                return NotFound();
            }

            var englishTaskModel = _mapper.Map<EnglishTaskModel, EnglishTaskViewModel>(englishTask);
            
            return Ok(englishTaskModel);
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EnglishTaskCreateViewModel englishTaskCreateViewModel)
        {
            var englishTaskCreateModel = _mapper.Map<EnglishTaskCreateModel>(englishTaskCreateViewModel);
            
            await _englishTaskService.CreateEnglishTaskAsync(englishTaskCreateModel);

            return Ok();
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpPost("from-items")]
        public async Task<IActionResult> CreateFromItems([FromBody] EnglishTaskFromItemsCreateViewModel createModel)
        {
            var applicationModel = _mapper.Map<EnglishTaskFromItemsCreateModel>(createModel);

            await _englishTaskCreateService.CreateFromItemsAsync(applicationModel);

            return Ok();
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpPost("from-items/random")]
        public async Task<IActionResult> CreateFromRandomItems([FromBody] EnglishTaskFromRandomItemsCreateViewModel createModel)
        {
            var applicationModel = _mapper.Map<EnglishTaskFromRandomItemsCreateModel>(createModel);

            await _englishTaskCreateService.CreateFromRandomItemsAsync(applicationModel);

            return Ok();
        }
        
        [HttpPost("random")]
        public async Task<IActionResult> CreateRandomTask([FromBody] CreateRandomTaskViewModel createModel)
        {
            var applicationModel = _mapper.Map<CreateRandomTaskModel>(createModel);

            var task = await _englishTaskCreateService.CreateRandomTaskAsync(applicationModel);

            return Ok(task);
        }
        
        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] EnglishTaskCreateViewModel englishTaskCreateViewModel)
        {
            var englishTaskCreateModel = _mapper.Map<EnglishTaskCreateModel>(englishTaskCreateViewModel);

            bool result = await _englishTaskService.UpdateEnglishTaskAsync(id, englishTaskCreateModel);
            if (!result)
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
            if (!result)
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
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
        
        [HttpGet("filter")]
        public async Task<ActionResult> GetAllByFilter([FromQuery] BaseFilterParameters parameters)
        {
            var filterModel = _mapper.Map<BaseFilterModel>(parameters);
            IReadOnlyList<EnglishTaskModel> englishTakModels = await _englishTaskService.FindAllEnglishTaskAsync(filterModel);
            if (!englishTakModels.Any())
            {
                return NotFound();
            }

            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskViewModel>>(englishTakModels);
            
            return Ok(englishTaskModels);
        }
    }
}
