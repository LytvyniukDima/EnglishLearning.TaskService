using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.Filtering;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IRandomEnglishTaskService
    {
        Task<EnglishTaskModel> FindRandomEnglishTaskAsync(BaseFilterModel baseFilterModel);
        Task<EnglishTaskInfoModel> FindRandomInfoEnglishTaskAsync(BaseFilterModel baseFilterModel);
        
        Task<IReadOnlyList<EnglishTaskModel>> GetRandomFromAllEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskModel>> GetRandomWithUserPreferencesEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskModel>> FindRandomCountEnglishTask(int count, BaseFilterModel baseFilterModel);
        
        Task<IReadOnlyList<EnglishTaskInfoModel>> GetRandomInfoFromAllEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskInfoModel>> GetRandomInfoWithUserPreferencesEnglishTask(int count);
        Task<IReadOnlyList<EnglishTaskInfoModel>> FindRandomInfoCountEnglishTask(int count, BaseFilterModel baseFilterModel);
    }
}
