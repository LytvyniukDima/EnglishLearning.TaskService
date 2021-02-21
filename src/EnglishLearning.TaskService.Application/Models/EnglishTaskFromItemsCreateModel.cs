using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Models
{
    public class EnglishTaskFromItemsCreateModel
    {
        public string GrammarPart { get; set; }
        public TaskType TaskType { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
        
        public IReadOnlyList<string> Items { get; set; }
    }
}