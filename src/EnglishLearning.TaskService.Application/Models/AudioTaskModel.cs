using System.Collections.Generic;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Models
{
    public class AudioTaskModel
    {
        public EnglishLevel EnglishLevel { get; set; }
        
        public string GrammarPart { get; set; }

        public IReadOnlyList<ParsedSentModel> Sents { get; set; }
    }
}