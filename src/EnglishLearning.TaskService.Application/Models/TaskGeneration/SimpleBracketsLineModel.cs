using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace EnglishLearning.TaskService.Application.Models.TaskGeneration
{
    public class SimpleBracketsLineModel
    {
        [BsonElement("content")]
        public string Content { get; set; }
        
        [BsonElement("option")]
        public string Option { get; set; }
        
        [BsonElement("answer")]
        public IReadOnlyList<string> Answer { get; set; }
    }
}