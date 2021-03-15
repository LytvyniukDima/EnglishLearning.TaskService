using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Abstract.TextAnalyze;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.ExternalServices.Abstract;

namespace EnglishLearning.TaskService.Application.Services.TextAnalyze
{
    public class EnglishLevelAnalyseService : IEnglishLevelAnalyseService
    {
        private readonly IWordMetadataRepository _wordMetadataRepository;

        public EnglishLevelAnalyseService(IWordMetadataRepository wordMetadataRepository)
        {
            _wordMetadataRepository = wordMetadataRepository;
        }
        
        public async Task<EnglishLevel> GetSentLevelAsync(IReadOnlyList<string> sent)
        {
            var words = sent
                .SkipLast(1)
                .Select(word => word.ToLower())
                .ToList();

            var wordsMetadata = await _wordMetadataRepository.GetAsync(words);

            if (wordsMetadata.Count == 0)
            {
                return EnglishLevel.None;
            }

            return wordsMetadata.Max(x => x.Level);
        }
    }
}