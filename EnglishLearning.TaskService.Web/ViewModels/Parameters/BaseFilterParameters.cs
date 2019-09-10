using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Web.ViewModels.Parameters
{
    public class BaseFilterParameters
    {
        public string[] GrammarPart { get; set; }
        public TaskType[] TaskType { get; set; }
        public EnglishLevel[] EnglishLevel { get; set; }
    }
}
