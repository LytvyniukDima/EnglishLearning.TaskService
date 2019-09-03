using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.Utilities.Persistence.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EnglishLearning.TaskService.Persistence.Entities
{
    public class EnglishTask : IEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string GrammarPart { get; set; }
        [BsonRepresentation(BsonType.String)] 
        public TaskType TaskType { get; set; }
        [BsonRepresentation(BsonType.String)] 
        public EnglishLevel EnglishLevel { get; set; }
        
        public int Count { get; set; }
        public string Example { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
    }
}
