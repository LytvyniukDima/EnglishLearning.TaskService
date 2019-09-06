using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IRandomEnglishTaskService
    {
        Task<EnglishTaskDto> FindRandomEnglishTaskAsync(IReadOnlyList<string> grammarParts = null, IReadOnlyList<TaskType> taskTypes = null, IReadOnlyList<EnglishLevel> englishLevels = null);
        Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(IReadOnlyList<string> grammarParts = null, IReadOnlyList<TaskType> taskTypes = null, IReadOnlyList<EnglishLevel> englishLevels = null);
        
        Task<IReadOnlyList<EnglishTaskDto>> GetRandomFromAllEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskDto>> GetRandomWithUserPreferencesEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskDto>> FindRandomCountEnglishTask(int count, IReadOnlyList<string> grammarParts = null, IReadOnlyList<TaskType> taskTypes = null, IReadOnlyList<EnglishLevel> englishLevels = null);
        
        Task<IReadOnlyList<EnglishTaskInfoDto>> GetRandomInfoFromAllEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskInfoDto>> GetRandomInfoWithUserPreferencesEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskInfoDto>> FindRandomInfoCountEnglishTask(int count, IReadOnlyList<string> grammarParts = null, IReadOnlyList<TaskType> taskTypes = null, IReadOnlyList<EnglishLevel> englishLevels = null);
    }
}
