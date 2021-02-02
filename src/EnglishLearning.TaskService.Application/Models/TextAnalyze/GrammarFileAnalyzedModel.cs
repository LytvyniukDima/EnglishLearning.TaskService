using System;
using System.Collections.Generic;

namespace EnglishLearning.TaskService.Application.Models.TextAnalyze
{
    public class GrammarFileAnalyzedModel
    {
        public Guid Id { get; set; }
        
        public Guid FileId { get; set; }
        
        public string Name { get; set; }
        
        public DateTime CreatedTime { get; set; }
        
        public IReadOnlyList<string> Path { get; set; }
    }
}