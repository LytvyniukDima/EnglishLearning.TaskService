using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Persistence.Entities.TextAnalyze;
using EnglishLearning.Utilities.Persistence.Interfaces;

namespace EnglishLearning.TaskService.Persistence.Abstract
{
    public interface IParsedSentRepository : IBaseRepository<ParsedSent, string>
    {
        Task AddAsync(IReadOnlyCollection<ParsedSent> sents);
    }
}