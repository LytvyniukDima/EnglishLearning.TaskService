using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.Filtering;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface ITaskItemService
    {
        Task AddManyAsync(IReadOnlyList<CreateTaskItemModel> taskItems);

        Task<IReadOnlyList<TaskItemModel>> GetAsync(TaskItemsFilterModel filter);

        Task<TaskItemsFilterModel> GetFilterOptionsAsync();
    }
}