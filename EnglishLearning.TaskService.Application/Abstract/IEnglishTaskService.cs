using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IEnglishTaskService
    {
        Task CreateEnglishTaskAsync(EnglishTaskCreateDto englishTaskCreateDto);
        Task<bool> UpdateEnglishTaskAsync(string id, EnglishTaskCreateDto englishTaskDto);
        Task<EnglishTaskDto> GetByIdEnglishTaskAsync(string id);
        Task<IReadOnlyList<EnglishTaskDto>> GetAllEnglishTaskAsync();
        Task<bool> DeleteByIdEnglishTaskAsync(string id);
        Task<bool> DeleteAllEnglishTaskAsync();
        
        Task<EnglishTaskInfoDto> GetByIdEnglishTaskInfoAsync(string id);
        Task<IReadOnlyList<EnglishTaskInfoDto>> GetAllEnglishTaskInfoAsync();
        
        Task<IReadOnlyList<EnglishTaskDto>> FindAllEnglishTaskAsync(string[] grammarParts = null, TaskType[] taskTypes = null, EnglishLevel[] englishLevels = null);
        Task<IReadOnlyList<EnglishTaskInfoDto>> FindAllInfoEnglishTaskAsync(string[] grammarParts = null, TaskType[] taskTypes = null, EnglishLevel[] englishLevels = null);
    }
}
