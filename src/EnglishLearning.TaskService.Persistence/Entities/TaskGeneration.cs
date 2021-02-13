using System;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.Utilities.Persistence.Interfaces;

namespace EnglishLearning.TaskService.Persistence.Entities
{
    public class TaskGeneration : IEntity<string>
    {
        public string Id { get; set; }
        
        public string SourceId { get; set; }
        
        public string Name { get; set; }
        
        public TaskType TaskType { get; set; }
        
        public string GrammarPart { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
    }
}