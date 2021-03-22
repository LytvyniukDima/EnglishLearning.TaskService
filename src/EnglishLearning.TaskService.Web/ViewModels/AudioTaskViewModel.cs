using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Web.ViewModels.TextAnalyze;

namespace EnglishLearning.TaskService.Web.ViewModels
{
    public class AudioTaskViewModel
    {
        public EnglishLevel EnglishLevel { get; set; }
        
        public string GrammarPart { get; set; }

        public IReadOnlyList<ParsedSentViewModel> Sents { get; set; }
    }
}