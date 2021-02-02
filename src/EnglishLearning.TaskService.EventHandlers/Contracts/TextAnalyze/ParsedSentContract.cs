using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnglishLearning.TaskService.EventHandlers.Contracts.TextAnalyze
{
    public class ParsedSentContract
    {
        [JsonPropertyName("sent")]
        public string Sent { get; set; }
        
        [JsonPropertyName("sent_type")]
        public string SentType { get; set; }
        
        [JsonPropertyName("tokens")]
        public IReadOnlyCollection<SentTokenContract> Tokens { get; set; }
    }
}