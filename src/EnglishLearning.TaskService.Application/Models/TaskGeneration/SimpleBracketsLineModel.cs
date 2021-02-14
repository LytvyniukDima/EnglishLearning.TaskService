using System.Collections.Generic;

namespace EnglishLearning.TaskService.Application.Models.TaskGeneration
{
    public class SimpleBracketsLineModel
    {
        public string Content { get; set; }
        
        public string Option { get; set; }
        
        public IReadOnlyList<string> Answer { get; set; }
    }
}