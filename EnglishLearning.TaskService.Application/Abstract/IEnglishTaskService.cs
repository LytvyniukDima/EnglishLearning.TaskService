using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.DTO;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IEnglishTaskService
    {
        Task CreateEnglishTaskAsync(EnglishTaskCreateDto englishTaskCreateDto);
        Task<bool> UpdateEnglishTaskAsync(string id, EnglishTaskCreateDto englishTaskDto);
        Task<EnglishTaskDto> GetByIdEnglishTaskAsync(string id);
        Task<IEnumerable<EnglishTaskDto>> GetAllEnglishTaskAsync();
        Task<bool> DeleteByIdEnglishTaskAsync(string id);
        Task<bool> DeleteAllEnglishTaskAsync();
        
        Task<EnglishTaskInfoDto> GetByIdEnglishTaskInfoAsync(string id);
        Task<IEnumerable<EnglishTaskInfoDto>> GetAllEnglishTaskInfoAsync();
        
        Task<IEnumerable<EnglishTaskDto>> FindAllEnglishTaskAsync(string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null);
        Task<IEnumerable<EnglishTaskInfoDto>> FindAllInfoEnglishTaskAsync(string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null);
    }
}