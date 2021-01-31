using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Abstract.TextAnalyze;
using EnglishLearning.TaskService.EventHandlers.Infrastructure;
using EnglishLearning.Utilities.MessageBrokers.Contracts.TextAnalyze;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Abstraction;

namespace EnglishLearning.TaskService.EventHandlers.Handlers
{
    internal class TextAnalyzeMessageHandler : IKafkaMessageHandler<GrammarTextAnalyzedEvent>
    {
        private IParsedSentService _parsedSentService;

        private EventHandlerMapper _eventHandlerMapper;

        public TextAnalyzeMessageHandler(IParsedSentService parsedSentService, EventHandlerMapper eventHandlerMapper)
        {
            _parsedSentService = parsedSentService;
            _eventHandlerMapper = eventHandlerMapper;
        }
        
        public Task OnMessageAsync(GrammarTextAnalyzedEvent message)
        {
            var parsedSents = _eventHandlerMapper.MapEventToParsedModels(message);

            return _parsedSentService.AddSentsAsync(parsedSents);
        }
    }
}
