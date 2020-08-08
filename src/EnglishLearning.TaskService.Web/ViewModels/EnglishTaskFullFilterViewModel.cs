using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Web.ViewModels
{
    public class EnglishTaskFullFilterViewModel
    {
        public Dictionary<string, int> GrammarPartFilterOptions { get; set; }
        public Dictionary<string, int> EnglishLevelFilterOptions { get; set; }
        public Dictionary<string, int> TaskTypeFilterOptions { get; set; }
    }
}
