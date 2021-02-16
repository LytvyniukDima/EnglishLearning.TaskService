using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract.TextAnalyze;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;
using EnglishLearning.TaskService.Common.Models;
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

        public async Task<IReadOnlyList<ParsedSentModel>> GetAllByAnalyzeId(Guid analyzeId)
        {
            var entities = await _parsedSentRepository.FindAllAsync(x => x.AnalyzeId == analyzeId);

            return _mapper.Map<IReadOnlyList<ParsedSentModel>>(entities);
        }

        public async Task<ParsedSentModel> GetById(string id)
        {
            var entity = await _parsedSentRepository.FindAsync(x => x.Id == id);

            return _mapper.Map<ParsedSentModel>(entity);
        }

        public async Task<IReadOnlyList<ParsedSentModel>> GetAllByAnalyzeAndGrammarPartAsync(Guid analyzeId, string grammarPart)
        {
            var sentTypes = GrammarParts.GrammarPartSentTypesMap[grammarPart];

            var entities = await _parsedSentRepository
                .FindAllAsync(x => x.AnalyzeId == analyzeId && sentTypes.Contains(x.SentType));

            return _mapper.Map<IReadOnlyList<ParsedSentModel>>(entities);
        }
    }
}