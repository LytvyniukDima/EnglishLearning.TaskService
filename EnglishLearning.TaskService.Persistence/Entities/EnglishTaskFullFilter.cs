using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Persistence.Entities
{
    public class EnglishTaskFullFilter
    {
        public Dictionary<string, int> GrammarPartFilterOptions { get; set; }
        public Dictionary<EnglishLevel, int> EnglishLevelFilterOptions { get; set; }
        public Dictionary<TaskType, int> TaskTypeFilterOptions { get; set; }
    }
}