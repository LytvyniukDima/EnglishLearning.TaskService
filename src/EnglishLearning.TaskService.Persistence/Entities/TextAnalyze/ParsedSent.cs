using System;
using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.Utilities.Persistence.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EnglishLearning.TaskService.Persistence.Entities.TextAnalyze
{
    public class ParsedSent : IEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid AnalyzeId { get; set; }
        
        public string Sent { get; set; }
        
        public string SentType { get; set; }

        [BsonRepresentation(BsonType.String)]        
        public EnglishLevel EnglishLevel { get; set; }
        
        public IReadOnlyCollection<SentToken> Tokens { get; set; }
    }
}