namespace EnglishLearning.TaskService.Persistence.Entities.TextAnalyze
{
    public class SentToken
    {
        public string Word { get; set; }
        
        public string Pos { get; set; }
        
        public string Tag { get; set; }
        
        public string Dep { get; set; }
    }
}