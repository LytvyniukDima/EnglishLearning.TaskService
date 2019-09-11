using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Persistence.Abstract
{
    public interface IEnglishTaskFilterOptionsRepository
    {
        Dictionary<string, int> GetGrammarPartFilterOptions();
        Dictionary<EnglishLevel, int> GetEnglishLevelFilterOptions();
        Dictionary<TaskType, int> GetTaskTypeFilterOptions();
        EnglishTaskFullFilter GetFullFilter();
    }
}