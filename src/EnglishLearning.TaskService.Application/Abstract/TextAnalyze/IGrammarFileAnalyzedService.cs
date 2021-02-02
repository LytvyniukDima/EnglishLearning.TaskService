using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;

namespace EnglishLearning.TaskService.Application.Abstract.TextAnalyze
{
    public interface IGrammarFileAnalyzedService
    {
        Task AddAsync(GrammarFileAnalyzedModel grammarFileAnalyzed);
    }
}