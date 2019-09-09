using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Persistence.Interfaces;

namespace EnglishLearning.TaskService.Persistence.Abstract
{
    public interface IEnglishTaskRepository : IBaseWithInfoModelRepository<EnglishTask, EnglishTaskInfo, string>
    {
        Task<IReadOnlyList<EnglishTask>> FindAllByFilters(BaseFilter baseFilter);
        Task<IReadOnlyList<EnglishTaskInfo>> FindAllInfoByFilters(BaseFilter baseFilter);
    }
}
