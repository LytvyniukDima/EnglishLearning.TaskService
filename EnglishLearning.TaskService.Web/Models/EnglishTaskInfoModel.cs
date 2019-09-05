using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Web.Models
{
    public class EnglishTaskInfoModel
    {
        public string Id { get; set; }
        
        public string GrammarPart { get; set; }
        public TaskType TaskType { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
    }
}