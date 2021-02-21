using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Persistence.Interfaces;

namespace EnglishLearning.TaskService.Persistence.Abstract
{
    public interface ITaskItemRepository : IBaseRepository<TaskItem, string>
    {
        Task<IReadOnlyList<TaskItem>> FindAllByFilters(TaskItemsFilter filter);

        Task<TaskItemsFilter> GetAvailableFilters();
    }
}