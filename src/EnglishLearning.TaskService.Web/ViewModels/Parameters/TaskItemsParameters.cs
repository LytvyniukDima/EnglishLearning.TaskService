using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Web.ViewModels.Parameters
{
    public class TaskItemsParameters
    {
        public string[] GrammarPart { get; set; }
        public string[] SentType { get; set; }
        public TaskType[] TaskType { get; set; }
    }
}