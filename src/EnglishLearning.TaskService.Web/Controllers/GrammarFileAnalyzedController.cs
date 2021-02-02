using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract.TextAnalyze;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.ViewModels.TextAnalyze;
using EnglishLearning.Utilities.Identity.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("/api/tasks/grammarFileAnalyzed")]
    public class GrammarFileAnalyzedController : Controller
    {
        private readonly IGrammarFileAnalyzedService _grammarFileAnalyzedService;

        private readonly IParsedSentService _parsedSentService;

        private readonly IMapper _mapper;
        
        public GrammarFileAnalyzedController(
            IGrammarFileAnalyzedService grammarFileAnalyzedService,
            IParsedSentService parsedSentService,
            WebMapper webMapper)
        {
            _grammarFileAnalyzedService = grammarFileAnalyzedService;
            _parsedSentService = parsedSentService;
            _mapper = webMapper.Mapper;
        }

        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var model = await _grammarFileAnalyzedService.GetByIdAsync(id);

            var viewModel = _mapper.Map<GrammarFileAnalyzedViewModel>(model);

            return Ok(viewModel);
        }

        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(Guid id)
        {
            var models = await _grammarFileAnalyzedService.GetAllAsync();

            var viewModels = _mapper.Map<IReadOnlyList<GrammarFileAnalyzedViewModel>>(models);

            return Ok(viewModels);
        }

        [EnglishLearningAuthorize(AuthorizeRole.Admin)]
        [HttpGet("{id}/parsedSents")]
        public async Task<IActionResult> GetParsedSentsByAnalyzeId(Guid id)
        {
            var models = await _parsedSentService.GetAllByAnalyzeId(id);

            var viewModels = _mapper.Map<IReadOnlyList<ParsedSentModel>>(models);

            return Ok(viewModels);
        }
    }
}