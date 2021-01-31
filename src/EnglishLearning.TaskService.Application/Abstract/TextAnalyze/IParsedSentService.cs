using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;

namespace EnglishLearning.TaskService.Application.Abstract.TextAnalyze
{
    public interface IParsedSentService
    {
        Task AddSentsAsync(IReadOnlyCollection<ParsedSentModel> sents);
    }
}
