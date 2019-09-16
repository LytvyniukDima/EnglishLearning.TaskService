using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.Filtering;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IEnglishTaskService
    {
        Task CreateEnglishTaskAsync(EnglishTaskCreateModel englishTaskCreateModel);
        Task<bool> UpdateEnglishTaskAsync(string id, EnglishTaskCreateModel englishTaskModel);
        Task<EnglishTaskModel> GetByIdEnglishTaskAsync(string id);
        Task<IReadOnlyList<EnglishTaskModel>> GetAllEnglishTaskAsync();
        Task<bool> DeleteByIdEnglishTaskAsync(string id);
        Task<bool> DeleteAllEnglishTaskAsync();
        
        Task<EnglishTaskInfoModel> GetByIdEnglishTaskInfoAsync(string id);
        Task<IReadOnlyList<EnglishTaskInfoModel>> GetAllEnglishTaskInfoAsync();
        Task<IReadOnlyList<EnglishTaskInfoModel>> GetAllEnglishTaskInfoWithUserPreferencesAsync();
        
        Task<IReadOnlyList<EnglishTaskModel>> FindAllEnglishTaskAsync(BaseFilterModel baseFilterModel);
        Task<IReadOnlyList<EnglishTaskInfoModel>> FindAllInfoEnglishTaskAsync(BaseFilterModel baseFilterModel);
    }
}
