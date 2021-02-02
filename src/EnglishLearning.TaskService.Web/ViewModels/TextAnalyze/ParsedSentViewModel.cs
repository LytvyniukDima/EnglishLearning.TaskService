using System;
using System.Collections.Generic;

namespace EnglishLearning.TaskService.Web.ViewModels.TextAnalyze
{
    public class ParsedSentViewModel
    {
        public string Id { get; set; }
        
        public Guid AnalyzeId { get; set; }
        
        public string Sent { get; set; }
        
        public string SentType { get; set; }
        
        public IReadOnlyList<SentTokenViewModel> Tokens { get; set; }
    }
}