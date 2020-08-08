using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Models.Filtering;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.ViewModels;
using EnglishLearning.Utilities.Linq.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("/api/tasks/filterOptions")]
    public class EnglishTaskFilterOptionsController : Controller
    {
        private readonly IEnglishTaskFilterOptionsService _filterOptionsService;
        private readonly IMapper _mapper;

        public EnglishTaskFilterOptionsController(IEnglishTaskFilterOptionsService filterOptionsService, WebMapper webMapper)
        {
            _filterOptionsService = filterOptionsService;
            _mapper = webMapper.Mapper;
        }

        [HttpGet("grammarPart")]
        public async Task<IActionResult> GetGrammarPartFilterOptions()
        {
            Dictionary<string, int> filterOptions = await _filterOptionsService.GetGrammarPartFilterOptions();

            return Ok(filterOptions);
        }
        
        [HttpGet("taskType")]
        public async Task<IActionResult> GetTaskTypeFilterOptions()
        {
            Dictionary<TaskType, int> filterOptions = await _filterOptionsService.GetTaskTypeFilterOptions();

            return Ok(filterOptions.ConvertToStringKeyDictionary());
        }
        
        [HttpGet("englishLevel")]
        public async Task<IActionResult> GetEnglishLevelFilterOptions()
        {
            Dictionary<EnglishLevel, int> filterOptions = await _filterOptionsService.GetEnglishLevelFilterOptions();
            
            return Ok(filterOptions.ConvertToStringKeyDictionary());
        }
        
        [HttpGet("fullFilter")]
        public async Task<IActionResult> GetFullFilterOptions()
        {
            EnglishTaskFullFilterModel filterOptions = await _filterOptionsService.GetFullFilter();
            var viewModel = _mapper.Map<EnglishTaskFullFilterViewModel>(filterOptions);

            return Ok(viewModel);
        }
    }
}
