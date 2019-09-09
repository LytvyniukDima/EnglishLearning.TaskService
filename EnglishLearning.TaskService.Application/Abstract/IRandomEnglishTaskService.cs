using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.DTO;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IRandomEnglishTaskService
    {
        Task<EnglishTaskDto> FindRandomEnglishTaskAsync(BaseFilterModel baseFilterModel);
        Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(BaseFilterModel baseFilterModel);
        
        Task<IReadOnlyList<EnglishTaskDto>> GetRandomFromAllEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskDto>> GetRandomWithUserPreferencesEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskDto>> FindRandomCountEnglishTask(int count, BaseFilterModel baseFilterModel);
        
        Task<IReadOnlyList<EnglishTaskInfoDto>> GetRandomInfoFromAllEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskInfoDto>> GetRandomInfoWithUserPreferencesEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskInfoDto>> FindRandomInfoCountEnglishTask(int count, BaseFilterModel baseFilterModel);
    }
}
