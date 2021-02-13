using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Persistence.Interfaces;

namespace EnglishLearning.TaskService.Persistence.Abstract
{
    public interface ITaskGenerationRepository : IBaseRepository<TaskGeneration, string>
    {
    }
}