using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EnglishLearning.Utilities.MessageBrokers.Contracts.TextAnalyze;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Abstraction;

namespace EnglishLearning.TaskService.EventHandlers.Handlers
{
    public class TextAnalyzeMessageHandler : IKafkaMessageHandler<GrammarTextAnalyzedEvent>
    {
        public async Task OnMessageAsync(GrammarTextAnalyzedEvent message)
        {
        }
    }
}
