using EnglishLearning.TaskService.Persistence.Entities;
using MongoDB.Driver;

namespace EnglishLearning.TaskService.Persistence.Abstract
{
    public interface ITaskDbContext
    {
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}