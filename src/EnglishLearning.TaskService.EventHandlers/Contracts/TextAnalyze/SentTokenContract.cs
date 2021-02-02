using System.Text.Json.Serialization;

namespace EnglishLearning.TaskService.EventHandlers.Contracts.TextAnalyze
{
    public class SentTokenContract
    {
        [JsonPropertyName("word")]
        public string Word { get; set; }
        
        [JsonPropertyName("pos")]
        public string Pos { get; set; }
        
        [JsonPropertyName("tag")]
        public string Tag { get; set; }
        
        [JsonPropertyName("dep")]
        public string Dep { get; set; }
    }
}