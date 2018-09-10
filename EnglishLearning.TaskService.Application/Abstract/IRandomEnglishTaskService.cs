using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.DTO;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IRandomEnglishTaskService
    {
        Task<EnglishTaskDto> FindRandomEnglishTaskAsync(string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null);
        Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null);
        
        Task<IEnumerable<EnglishTaskDto>> GetRandomFromAllEnglishTask(int count);
        Task<IEnumerable<EnglishTaskDto>> FindRandomCountEnglishTask(int count, string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null);
        
        Task<IEnumerable<EnglishTaskInfoDto>> GetRandomInfoFromAllEnglishTask(int count);
        Task<IEnumerable<EnglishTaskInfoDto>> FindRandomInfoCountEnglishTask(int count, string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null);
    }
}