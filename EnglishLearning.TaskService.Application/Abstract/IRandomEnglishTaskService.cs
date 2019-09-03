using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.DTO;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IRandomEnglishTaskService
    {
        Task<EnglishTaskDto> FindRandomEnglishTaskAsync(string[] grammarParts = null, TaskTypeDto[] taskTypes = null, EnglishLevelDto[] englishLevels = null);
        Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(string[] grammarParts = null, TaskTypeDto[] taskTypes = null, EnglishLevelDto[] englishLevels = null);
        
        Task<IReadOnlyList<EnglishTaskDto>> GetRandomFromAllEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskDto>> FindRandomCountEnglishTask(int count, string[] grammarParts = null, TaskTypeDto[] taskTypes = null, EnglishLevelDto[] englishLevels = null);
        
        Task<IReadOnlyList<EnglishTaskInfoDto>> GetRandomInfoFromAllEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskInfoDto>> FindRandomInfoCountEnglishTask(int count, string[] grammarParts = null, TaskTypeDto[] taskTypes = null, EnglishLevelDto[] englishLevels = null);
    }
}
