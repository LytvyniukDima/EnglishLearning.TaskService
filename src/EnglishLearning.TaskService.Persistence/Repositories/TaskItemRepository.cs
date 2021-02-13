using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using EnglishLearning.Utilities.Persistence.Mongo.Repositories;

namespace EnglishLearning.TaskService.Persistence.Repositories
{
    internal class TaskItemRepository : BaseMongoRepository<TaskItem, string>, ITaskItemRepository
    {
        public TaskItemRepository(MongoContext mongoContext)
            : base(mongoContext)
        {
        }
    }
}