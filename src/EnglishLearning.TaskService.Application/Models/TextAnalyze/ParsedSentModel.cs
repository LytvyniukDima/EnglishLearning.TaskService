using System;
using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Models.TextAnalyze
{
    public class ParsedSentModel
    {
        public string Id { get; set; }
        
        public Guid AnalyzeId { get; set; }
        
        public string Sent { get; set; }
        
        public string SentType { get; set; }
        
        public EnglishLevel EnglishLevel { get; set; }
        
        public IReadOnlyList<SentTokenModel> Tokens { get; set; }
    }
}