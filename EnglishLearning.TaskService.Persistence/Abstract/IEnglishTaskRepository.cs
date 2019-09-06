using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Persistence.Interfaces;

namespace EnglishLearning.TaskService.Persistence.Abstract
{
    public interface IEnglishTaskRepository : IBaseWithInfoModelRepository<EnglishTask, EnglishTaskInfo, string>
    {
        Task<IReadOnlyList<EnglishTask>> FindAllByFilters(IReadOnlyList<string> grammarParts, IReadOnlyList<TaskType> taskTypes, IReadOnlyList<EnglishLevel> englishLevels);
        Task<IReadOnlyList<EnglishTaskInfo>> FindAllInfoByFilters(IReadOnlyList<string> grammarParts, IReadOnlyList<TaskType> taskTypes, IReadOnlyList<EnglishLevel> englishLevels);
    }
}
