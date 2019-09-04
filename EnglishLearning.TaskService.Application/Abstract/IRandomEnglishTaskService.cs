using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IRandomEnglishTaskService
    {
        Task<EnglishTaskDto> FindRandomEnglishTaskAsync(string[] grammarParts = null, TaskType[] taskTypes = null, EnglishLevel[] englishLevels = null);
        Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(string[] grammarParts = null, TaskType[] taskTypes = null, EnglishLevel[] englishLevels = null);
        
        Task<IReadOnlyList<EnglishTaskDto>> GetRandomFromAllEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskDto>> FindRandomCountEnglishTask(int count, string[] grammarParts = null, TaskType[] taskTypes = null, EnglishLevel[] englishLevels = null);
        
        Task<IReadOnlyList<EnglishTaskInfoDto>> GetRandomInfoFromAllEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskInfoDto>> FindRandomInfoCountEnglishTask(int count, string[] grammarParts = null, TaskType[] taskTypes = null, EnglishLevel[] englishLevels = null);
    }
}
