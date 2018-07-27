using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EnglishLearning.TaskService.Persistence.Entities
{
    public class EnglishTask
    {
        [BsonId]
        public Guid Id { get; set; }
//        public TaskType TaskType { get; set; }
//        public GrammarPart GrammarPart { get; set; }
//        public EnglishLevel EnglishLevel { get; set; }
        public int Count { get; set; }
        public string Example { get; set; }
        
        public string Text { get; set; }
        
        public string Answer { get; set; }
    }
}