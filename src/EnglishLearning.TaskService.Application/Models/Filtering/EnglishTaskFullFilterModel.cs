using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Models.Filtering
{
    public class EnglishTaskFullFilterModel
    {
        public Dictionary<string, int> GrammarPartFilterOptions { get; set; }
        public Dictionary<EnglishLevel, int> EnglishLevelFilterOptions { get; set; }
        public Dictionary<TaskType, int> TaskTypeFilterOptions { get; set; }
    }
}
