using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Models.Filtering
{
    public class TaskItemsFilterModel
    {
        public string[] GrammarPart { get; set; }
        public string[] SentType { get; set; }
        public TaskType[] TaskType { get; set; }
    }
}