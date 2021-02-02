using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;

namespace EnglishLearning.TaskService.Application.Abstract.TextAnalyze
{
    public interface IParsedSentService
    {
        Task AddSentsAsync(IReadOnlyList<ParsedSentModel> sents);

        Task<IReadOnlyList<ParsedSentModel>> GetAllAsync();

        Task<IReadOnlyList<ParsedSentModel>> GetAllByAnalyzeId(Guid analyzeId);

        Task<ParsedSentModel> GetById(string id);
    }
}
