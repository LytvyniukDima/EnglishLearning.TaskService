using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using EnglishLearning.Utilities.Persistence.Mongo.Repositories;
using MongoDB.Driver;

namespace EnglishLearning.TaskService.Persistence.Repositories
{
    public class EnglishTaskMongoDbRepository : BaseMongoWithInfoModelRepository<EnglishTask, EnglishTaskInfo, string>, IEnglishTaskRepository
    {
        public EnglishTaskMongoDbRepository(MongoContext dbContext) : base(dbContext)
        {
            
        }

        protected override ProjectionDefinition<EnglishTask, EnglishTaskInfo> InfoModelProjectionDefinition
        {
            get => Builders<EnglishTask>
                .Projection
                .Expression(x => new EnglishTaskInfo()
                {
                    Id = x.Id,
                    EnglishLevel = x.EnglishLevel,
                    GrammarPart = x.GrammarPart,
                    TaskType = x.TaskType
                });
        }
    }
}
