using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using EnglishLearning.Utilities.Persistence.Mongo.Repositories;

namespace EnglishLearning.TaskService.Persistence.Repositories
{
    public class TaskGenerationRepository : BaseMongoRepository<TaskGeneration, string>, ITaskGenerationRepository
    {
        public TaskGenerationRepository(MongoContext mongoContext)
            : base(mongoContext)
        {
        }
    }
}