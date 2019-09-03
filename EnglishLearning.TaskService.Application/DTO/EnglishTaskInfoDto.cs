namespace EnglishLearning.TaskService.Application.DTO
{
    public class EnglishTaskInfoDto
    {
        public string Id { get; set; }
        
        public string GrammarPart { get; set; }
        public TaskTypeDto TaskType { get; set; }
        public EnglishLevelDto EnglishLevel { get; set; }
    }
}