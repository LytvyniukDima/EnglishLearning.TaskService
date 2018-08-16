using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EnglishLearning.TaskService.Persistence.Contexts
{
    public class TaskDbContext : ITaskDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoDbConfiguration _mongoDbConfiguration;
        
        public TaskDbContext(IOptions<MongoDbConfiguration> mongoDbConfiguration)
        {
            _mongoDbConfiguration = mongoDbConfiguration.Value;
            
            var client = new MongoClient(_mongoDbConfiguration.ServerAddress);
            // TODO: Throw exception
            if (client != null)
                _database = client.GetDatabase(_mongoDbConfiguration.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}