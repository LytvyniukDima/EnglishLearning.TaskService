using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Web.ViewModels
{
    public class GenerateTaskViewModel
    {
        public string AnalyzeId { get; set; }
        
        public string SentType { get; set; }
        
        public string GrammarPart { get; set; }
        
        public TaskType TaskType { get; set; }
        
        public string Name { get; set; }
    }
}