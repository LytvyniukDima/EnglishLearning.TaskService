using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract.TextAnalyze;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities.TextAnalyze;

namespace EnglishLearning.TaskService.Application.Services.TextAnalyze
{
    internal class ParsedSentService : IParsedSentService
    {
        private readonly IParsedSentRepository _parsedSentRepository;

        private readonly IMapper _mapper;

        public ParsedSentService(IParsedSentRepository parsedSentRepository, ApplicationMapper applicationMapper)
        {
            _parsedSentRepository = parsedSentRepository;
            _mapper = applicationMapper.Mapper;
        }

        public Task AddSentsAsync(IReadOnlyList<ParsedSentModel> sents)
        {
            var entities = _mapper.Map<IReadOnlyList<ParsedSent>>(sents);

            return _parsedSentRepository.AddManyAsync(entities);
        }
    }
}