using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Persistence.Abstract
{
    public interface IEnglishTaskFilterOptionsRepository
    {
        Task<Dictionary<string, int>> GetGrammarPartFilterOptions();
        Task<Dictionary<EnglishLevel, int>> GetEnglishLevelFilterOptions();
        Task<Dictionary<TaskType, int>> GetTaskTypeFilterOptions();
        Task<EnglishTaskFullFilter> GetFullFilter();
        Task<IReadOnlyList<PerEnglishLevelTaskInformation>> GetPerEnglishLevelTaskInformation();
    }
}
