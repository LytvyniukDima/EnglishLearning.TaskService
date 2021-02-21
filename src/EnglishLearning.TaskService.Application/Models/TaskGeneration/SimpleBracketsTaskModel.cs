using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace EnglishLearning.TaskService.Application.Models.TaskGeneration
{
    public class SimpleBracketsTaskModel
    {
        [BsonElement("lines")]
        public IReadOnlyList<SimpleBracketsLineModel> Lines { get; set; }
    }
}