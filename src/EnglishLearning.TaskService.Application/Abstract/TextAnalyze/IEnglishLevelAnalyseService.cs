using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Abstract.TextAnalyze
{
    public interface IEnglishLevelAnalyseService
    {
        Task<EnglishLevel> GetSentLevelAsync(ParsedSentModel sent);
    }
}