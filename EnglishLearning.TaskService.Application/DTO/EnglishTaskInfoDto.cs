using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.DTO
{
    public class EnglishTaskInfoDto
    {
        public string Id { get; set; }
        
        public string GrammarPart { get; set; }
        public TaskType TaskType { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
    }
}