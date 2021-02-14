using System;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.Utilities.Persistence.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EnglishLearning.TaskService.Persistence.Entities
{
    public class TaskGeneration : IEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid AnalyzeId { get; set; }
        
        public string Name { get; set; }
        
        [BsonRepresentation(BsonType.String)] 
        public TaskType TaskType { get; set; }
        
        public string GrammarPart { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
    }
}