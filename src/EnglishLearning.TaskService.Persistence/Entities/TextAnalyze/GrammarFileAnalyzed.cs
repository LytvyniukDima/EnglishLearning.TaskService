using System;
using System.Collections.Generic;
using EnglishLearning.Utilities.Persistence.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EnglishLearning.TaskService.Persistence.Entities.TextAnalyze
{
    public class GrammarFileAnalyzed : IEntity<Guid>
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid FileId { get; set; }
        
        public string Name { get; set; }
        
        public DateTime CreatedTime { get; set; }
        
        public IReadOnlyList<string> Path { get; set; }
        
        public int SentCount { get; set; }
    }
}