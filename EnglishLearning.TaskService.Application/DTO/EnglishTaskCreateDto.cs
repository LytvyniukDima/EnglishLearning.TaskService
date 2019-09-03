namespace EnglishLearning.TaskService.Application.DTO
{
    public class EnglishTaskCreateDto
    {   
        public string GrammarPart { get; set; }
        public TaskTypeDto TaskType { get; set; }
        public EnglishLevelDto EnglishLevel { get; set; }
        
        public int Count { get; set; }
        public string Example { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
    }
}
