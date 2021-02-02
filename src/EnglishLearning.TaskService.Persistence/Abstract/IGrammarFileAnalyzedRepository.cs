using System;
using EnglishLearning.TaskService.Persistence.Entities.TextAnalyze;
using EnglishLearning.Utilities.Persistence.Interfaces;

namespace EnglishLearning.TaskService.Persistence.Abstract
{
    public interface IGrammarFileAnalyzedRepository : IBaseRepository<GrammarFileAnalyzed, Guid>
    {
    }
}