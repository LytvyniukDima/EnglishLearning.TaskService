using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnglishLearning.TaskService.EventHandlers.Contracts.TextAnalyze
{
    public class GrammarFileAnalyzedEvent
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        
        [JsonPropertyName("fileId")]
        public Guid FileId { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("createdTime")]
        public DateTime CreatedTime { get; set; }
        
        [JsonPropertyName("path")]
        public IReadOnlyCollection<string> Path { get; set; }
    }
}