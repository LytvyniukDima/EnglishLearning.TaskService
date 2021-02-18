using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface ITaskItemService
    {
        Task AddManyAsync(IReadOnlyList<CreateTaskItemModel> taskItems);

        Task<IReadOnlyList<TaskItemModel>> GetAllAsync();
    }
}