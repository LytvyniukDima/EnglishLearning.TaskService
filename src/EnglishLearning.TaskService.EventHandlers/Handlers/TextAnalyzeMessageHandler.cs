using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Abstract.TextAnalyze;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;
using EnglishLearning.TaskService.EventHandlers.Contracts.TextAnalyze;
using EnglishLearning.TaskService.EventHandlers.Infrastructure;
using EnglishLearning.Utilities.MessageBrokers.Kafka.Abstraction;

namespace EnglishLearning.TaskService.EventHandlers.Handlers
{
    internal class TextAnalyzeMessageHandler : IKafkaMessageHandler<GrammarTextAnalyzedEvent>, IKafkaMessageHandler<GrammarFileAnalyzedEvent>
    {
        private readonly IParsedSentService _parsedSentService;

        private readonly IGrammarFileAnalyzedService _grammarFileAnalyzedService;
        
        private EventHandlerMapper _eventHandlerMapper;

        public TextAnalyzeMessageHandler(
            IParsedSentService parsedSentService,
            IGrammarFileAnalyzedService grammarFileAnalyzedService,
            EventHandlerMapper eventHandlerMapper)
        {
            _parsedSentService = parsedSentService;
            _grammarFileAnalyzedService = grammarFileAnalyzedService;
            _eventHandlerMapper = eventHandlerMapper;
        }
        
        public Task OnMessageAsync(GrammarTextAnalyzedEvent message)
        {
            var parsedSents = _eventHandlerMapper.MapEventToParsedModels(message);

            return _parsedSentService.AddSentsAsync(parsedSents);
        }

        public Task OnMessageAsync(GrammarFileAnalyzedEvent message)
        {
            var model = _eventHandlerMapper.Mapper.Map<GrammarFileAnalyzedModel>(message);
            
            return _grammarFileAnalyzedService.AddAsync(model);
        }
    }
}
