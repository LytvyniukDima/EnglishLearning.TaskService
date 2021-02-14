using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Abstract.TaskGeneration
{
    public interface ITaskItemsGeneratorFactory
    {
        ITaskItemsGeneratorService GetTaskItemsGenerator(
            string grammarPart,
            TaskType taskType);
    }
}