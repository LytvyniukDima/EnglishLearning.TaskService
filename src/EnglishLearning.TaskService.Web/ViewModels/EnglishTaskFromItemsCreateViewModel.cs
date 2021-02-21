using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Web.ViewModels
{
    public class EnglishTaskFromItemsCreateViewModel
    {
        public string GrammarPart { get; set; }
        public TaskType TaskType { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
        
        public IReadOnlyList<string> Items { get; set; }
    }
}