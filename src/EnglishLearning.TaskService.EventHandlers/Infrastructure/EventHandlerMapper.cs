using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;
using EnglishLearning.TaskService.EventHandlers.Contracts.TextAnalyze;

namespace EnglishLearning.TaskService.EventHandlers.Infrastructure
{
    public class EventHandlerMapper
    {
        public EventHandlerMapper()
        {
            Mapper = new MapperConfiguration(x => x
                    .AddProfile(new EventHandlerMapperProfile()))
                .CreateMapper();
        }

        public IMapper Mapper { get; }

        public IReadOnlyList<ParsedSentModel> MapEventToParsedModels(GrammarTextAnalyzedEvent grammarEvent)
        {
            return grammarEvent.Sents.Select(x =>
                {
                    return new ParsedSentModel
                    {
                        AnalyzeId = grammarEvent.AnalyzeId,
                        Sent = x.Sent,
                        SentType = x.SentType,
                        Tokens = Mapper.Map<IReadOnlyList<SentTokenModel>>(x.Tokens),
                    };
                })
                .ToList();
        }
    }
}