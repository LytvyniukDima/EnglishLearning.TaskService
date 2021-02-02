using System;
using System.Collections.Generic;

namespace EnglishLearning.TaskService.Web.ViewModels.TextAnalyze
{
    public class GrammarFileAnalyzedViewModel
    {
        public Guid Id { get; set; }
        
        public Guid FileId { get; set; }
        
        public string Name { get; set; }
        
        public DateTime CreatedTime { get; set; }
        
        public IReadOnlyList<string> Path { get; set; }
        
        public int SentCount { get; set; }
    }
}