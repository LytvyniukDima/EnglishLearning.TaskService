using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;

namespace EnglishLearning.TaskService.Application.Abstract.TextAnalyze
{
    public interface IGrammarFileAnalyzedService
    {
        Task AddAsync(GrammarFileAnalyzedModel grammarFileAnalyzed);

        Task<IReadOnlyList<GrammarFileAnalyzedModel>> GetAllAsync();

        Task<GrammarFileAnalyzedModel> GetByIdAsync(Guid id);
    }
}