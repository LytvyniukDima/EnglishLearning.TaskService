using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models.Filtering;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IEnglishTaskFilterOptionsService
    {
        Task<Dictionary<string, int>> GetGrammarPartFilterOptions();
        Task<Dictionary<EnglishLevel, int>> GetEnglishLevelFilterOptions();
        Task<Dictionary<TaskType, int>> GetTaskTypeFilterOptions();
        Task<EnglishTaskFullFilterModel> GetFullFilter();
    }
}
