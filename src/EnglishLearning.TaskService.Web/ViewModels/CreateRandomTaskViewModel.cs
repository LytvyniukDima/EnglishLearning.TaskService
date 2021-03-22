using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Web.ViewModels
{
    public class CreateRandomTaskViewModel
    {
        public string GrammarPart { get; set; }
        
        public TaskType TaskType { get; set; }
        
        public EnglishLevel EnglishLevel { get; set; }
        
        public int Count { get; set; }
    }
}