using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Persistence.Repositories
{
    public class EnglishTaskMongoDbRepository : BaseMongoDbRepository<EnglishTask>
    {
        private const string dbName = "EnglishTasks";
        
        public EnglishTaskMongoDbRepository(ITaskDbContext dbContext) : base(dbContext, dbName)
        {
            
        }
    }
}