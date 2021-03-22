using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Models
{
    public class AudioTaskQueryModel
    {
        public EnglishLevel EnglishLevel { get; set; }
        
        public string GrammarPart { get; set; }
        
        public int Count { get; set; }
    }
}