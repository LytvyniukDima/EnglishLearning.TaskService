using System;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Models.TaskGeneration
{
    public class TaskGenerationModel
    {
        public string Id { get; set; }
        
        public Guid AnalyzeId { get; set; }
        
        public string Name { get; set; }
        
        public TaskType TaskType { get; set; }
        
        public string GrammarPart { get; set; }
        
        public EnglishLevel EnglishLevel { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
    }
}