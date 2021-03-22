using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Models
{
    public class CreateRandomTaskModel
    {
        public string GrammarPart { get; set; }
        
        public TaskType TaskType { get; set; }
        
        public EnglishLevel EnglishLevel { get; set; }
        
        public int Count { get; set; }
    }
}