using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.Utilities.Linq.Extensions;

namespace EnglishLearning.TaskService.Application.Services
{
    internal class AudioTaskService : IAudioTaskService
    {
        private readonly IParsedSentRepository _parsedSentRepository;
        
        private readonly IMapper _mapper;

        public AudioTaskService(
            IParsedSentRepository parsedSentRepository,
            ApplicationMapper mapper)
        {
            _parsedSentRepository = parsedSentRepository;
            _mapper = mapper.Mapper;
        }
        
        public async Task<AudioTaskModel> CreateAudioTaskAsync(AudioTaskQueryModel queryModel)
        {
            var sentTypes = GrammarParts.GrammarPartSentTypesMap[queryModel.GrammarPart];

            var sents = await _parsedSentRepository.FindAllAsync(x =>
                x.EnglishLevel == queryModel.EnglishLevel
                && sentTypes.Contains(x.SentType));

            if (sents.Count < queryModel.Count)
            {
                var diffCount = queryModel.Count - sents.Count;
                var extendedSents = await _parsedSentRepository
                    .FindAllAsync(x => sentTypes.Contains(x.SentType));
                extendedSents = extendedSents
                    .Where(x => x.EnglishLevel <= queryModel.EnglishLevel)
                    .ToList();
                
                var randomSents = extendedSents.GetRandomCountOfElements(diffCount);
                sents = sents.Concat(randomSents).ToList();
            }

            var querySents = sents
                .GetRandomCountOfElements(queryModel.Count)
                .ToList();

            var applicationModels = _mapper.Map<IReadOnlyList<ParsedSentModel>>(querySents);

            return new AudioTaskModel
            {
                EnglishLevel = queryModel.EnglishLevel,
                GrammarPart = queryModel.GrammarPart,
                Sents = applicationModels,
            };
        }
    }
}