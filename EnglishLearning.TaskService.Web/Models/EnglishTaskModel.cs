using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Web.Models
{
    public class EnglishTaskModel
    {
        public string Id { get; set; }
        
        public string GrammarPart { get; set; }
        public TaskType TaskType { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
        
        public int Count { get; set; }
        public string Example { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
    }
}