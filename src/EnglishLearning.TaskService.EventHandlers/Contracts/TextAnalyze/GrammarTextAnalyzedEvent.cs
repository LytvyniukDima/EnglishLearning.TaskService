using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnglishLearning.TaskService.EventHandlers.Contracts.TextAnalyze
{
    public class GrammarTextAnalyzedEvent
    {
        [JsonPropertyName("analyze_id")]
        public Guid AnalyzeId { get; set; }
        
        [JsonPropertyName("sents")]
        public IReadOnlyCollection<ParsedSentContract> Sents { get; set; }
    }
}