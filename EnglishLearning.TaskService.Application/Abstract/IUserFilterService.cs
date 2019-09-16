using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models.Filtering;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IUserFilterService
    {
        Task<BaseFilterModel> GetFilterModelForCurrentUser(int requiredTaskCount);
    }
}
