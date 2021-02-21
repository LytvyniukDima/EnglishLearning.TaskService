using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Models.Filtering;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.ViewModels;
using EnglishLearning.TaskService.Web.ViewModels.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("/api/tasks/item")]
    public class TaskItemController : Controller
    {
        private readonly ITaskItemService _taskItemService;

        private readonly IMapper _mapper;
        
        public TaskItemController(ITaskItemService taskItemService, WebMapper webMapper)
        {
            _taskItemService = taskItemService;
            _mapper = webMapper.Mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetItems([FromQuery] TaskItemsParameters parameters)
        {
            var applicationFilter = _mapper.Map<TaskItemsFilterModel>(parameters);
            
            var items = await _taskItemService.GetAsync(applicationFilter);
            var viewModels = _mapper.Map<IReadOnlyList<TaskItemViewModel>>(items);

            return Ok(viewModels);
        }
    }
}