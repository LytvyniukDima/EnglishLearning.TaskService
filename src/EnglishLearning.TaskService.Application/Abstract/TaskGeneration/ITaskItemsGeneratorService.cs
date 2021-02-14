using System.Collections.Generic;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.TaskGeneration;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;

namespace EnglishLearning.TaskService.Application.Abstract.TaskGeneration
{
    public interface ITaskItemsGeneratorService
    {
        IReadOnlyList<CreateTaskItemModel> GenerateTaskItems(
            string generationId,
            GenerateTaskModel generateTaskModel,
            IReadOnlyList<ParsedSentModel> parsedSentModels);
    }
}