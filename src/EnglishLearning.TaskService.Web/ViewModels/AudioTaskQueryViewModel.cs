using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Web.ViewModels
{
    public class AudioTaskQueryViewModel
    {
        public EnglishLevel EnglishLevel { get; set; }
        
        public string GrammarPart { get; set; }
        
        public int Count { get; set; }
    }
}