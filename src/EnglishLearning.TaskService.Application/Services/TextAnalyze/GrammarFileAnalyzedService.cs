using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract.TextAnalyze;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities.TextAnalyze;

namespace EnglishLearning.TaskService.Application.Services.TextAnalyze
{
    public class GrammarFileAnalyzedService : IGrammarFileAnalyzedService
    {
        private readonly IGrammarFileAnalyzedRepository _grammarFileAnalyzedRepository;

        private readonly IMapper _mapper;
        
        public GrammarFileAnalyzedService(
            IGrammarFileAnalyzedRepository grammarFileAnalyzedService,
            ApplicationMapper applicationMapper)
        {
            _grammarFileAnalyzedRepository = grammarFileAnalyzedService;
            _mapper = applicationMapper.Mapper;
        }
        
        public Task AddAsync(GrammarFileAnalyzedModel grammarFileAnalyzed)
        {
            var entity = _mapper.Map<GrammarFileAnalyzed>(grammarFileAnalyzed);
            
            return _grammarFileAnalyzedRepository.AddAsync(entity);
        }

        public async Task<IReadOnlyList<GrammarFileAnalyzedModel>> GetAllAsync()
        {
            var entities = await _grammarFileAnalyzedRepository.GetAllAsync();

            return _mapper.Map<IReadOnlyList<GrammarFileAnalyzedModel>>(entities);
        }

        public async Task<GrammarFileAnalyzedModel> GetByIdAsync(Guid id)
        {
            var entity = await _grammarFileAnalyzedRepository.FindAsync(x => x.Id.Equals(id));

            return _mapper.Map<GrammarFileAnalyzedModel>(entity);
        }
    }
}
