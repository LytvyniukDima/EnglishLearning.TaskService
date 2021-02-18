using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models.TaskGeneration;

namespace EnglishLearning.TaskService.Application.Abstract.TaskGeneration
{
    public interface ITaskGenerationService
    {
        Task GenerateTasksAsync(GenerateTaskModel generateTaskModel);

        Task<IReadOnlyList<TaskGenerationModel>> GetAllAsync();

        Task<TaskGenerationModel> GetByIdAsync(string id);
    }
}