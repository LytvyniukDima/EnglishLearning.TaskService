using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Persistence
{
    public class TaskItemsFilter
    {
        public string[] GrammarPart { get; set; }
        public string[] SentType { get; set; }
        public TaskType[] TaskType { get; set; }
    }
}