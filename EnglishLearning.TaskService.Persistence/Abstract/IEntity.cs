using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EnglishLearning.TaskService.Persistence.Abstract
{
    public interface IEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
    }
}