using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Persistence.Entities
{
    public class BaseFilter
    {
        public IReadOnlyList<string> GrammarPart { get; set; }
        public IReadOnlyList<TaskType> TaskType { get; set; }
        public IReadOnlyList<EnglishLevel> EnglishLevel { get; set; }
    }
}
