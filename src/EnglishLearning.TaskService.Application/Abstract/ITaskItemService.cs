using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.Filtering;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface ITaskItemService
    {
        Task AddManyAsync(IReadOnlyList<CreateTaskItemModel> taskItems);

        Task<IReadOnlyList<TaskItemModel>> GetAsync(TaskItemsFilterModel filter);

        Task<IReadOnlyList<TaskItemModel>> GetByIds(IReadOnlyList<string> ids);
        
        Task<TaskItemsFilterModel> GetFilterOptionsAsync();

        Task<IReadOnlyDictionary<TaskType, int>> GetTaskTypeFilterOptionsAsync(EnglishLevel? level);
    }
}