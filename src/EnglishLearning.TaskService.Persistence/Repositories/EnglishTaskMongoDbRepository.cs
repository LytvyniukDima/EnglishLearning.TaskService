using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Linq.Extensions;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using EnglishLearning.Utilities.Persistence.Mongo.Repositories;
using MongoDB.Driver;

namespace EnglishLearning.TaskService.Persistence.Repositories
{
    public class EnglishTaskMongoDbRepository : BaseMongoWithInfoModelRepository<EnglishTask, EnglishTaskInfo, string>, IEnglishTaskRepository
    {
        public EnglishTaskMongoDbRepository(MongoContext mongoContext) 
            : base(mongoContext)
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
                    TaskType = x.TaskType,
                });
        }
        
        public async Task<IReadOnlyList<EnglishTask>> FindAllByFilters(BaseFilter baseFilter)
        {
            var builder = Builders<EnglishTask>.Filter;
            var filter = builder.Empty;
            
            if (!baseFilter.GrammarPart.IsNullOrEmpty())
            {
                filter &= builder.In(x => x.GrammarPart, baseFilter.GrammarPart);
            }

            if (!baseFilter.TaskType.IsNullOrEmpty())
            {
                filter &= builder.In(x => x.TaskType, baseFilter.TaskType);
            }

            if (!baseFilter.EnglishLevel.IsNullOrEmpty())
            {
                filter &= builder.In(x => x.EnglishLevel, baseFilter.EnglishLevel);
            }

            return await _collection.Find(filter).ToListAsync(); 
        }
        
        public async Task<IReadOnlyList<EnglishTaskInfo>> FindAllInfoByFilters(BaseFilter baseFilter)
        {
            var builder = Builders<EnglishTask>.Filter;
            var filter = builder.Empty;
            
            if (!baseFilter.GrammarPart.IsNullOrEmpty())
            {
                filter &= builder.In(x => x.GrammarPart, baseFilter.GrammarPart);
            }

            if (!baseFilter.TaskType.IsNullOrEmpty())
            {
                filter &= builder.In(x => x.TaskType, baseFilter.TaskType);
            }

            if (!baseFilter.EnglishLevel.IsNullOrEmpty())
            {
                filter &= builder.In(x => x.EnglishLevel, baseFilter.EnglishLevel);
            }

            return await _collection
                .Find(filter)
                .Project(InfoModelProjectionDefinition)
                .ToListAsync(); 
        }
    }
}
