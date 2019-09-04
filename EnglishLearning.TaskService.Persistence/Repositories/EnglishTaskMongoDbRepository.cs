using System.Collections.Generic;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Common.Models;
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
        
        public async Task<IReadOnlyList<EnglishTask>> FindAllByFilters(string[] grammarParts, TaskType[] taskTypes, EnglishLevel[] englishLevels)
        {
            var builder = Builders<EnglishTask>.Filter;
            var filter = builder.Empty;
            
            if (!grammarParts.IsNullOrEmpty())
            {
                filter &= builder.In(x => x.GrammarPart, grammarParts);
            }

            if (!taskTypes.IsNullOrEmpty())
            {
                filter &= builder.In(x => x.TaskType, taskTypes);
            }

            if (!englishLevels.IsNullOrEmpty())
            {
                filter &= builder.In(x => x.EnglishLevel, englishLevels);
            }

            return await _collection.Find(filter).ToListAsync(); 
        }
        
        public async Task<IReadOnlyList<EnglishTaskInfo>> FindAllInfoByFilters(string[] grammarParts, TaskType[] taskTypes, EnglishLevel[] englishLevels)
        {
            var builder = Builders<EnglishTask>.Filter;
            var filter = builder.Empty;
            
            if (!grammarParts.IsNullOrEmpty())
            {
                filter &= builder.In(x => x.GrammarPart, grammarParts);
            }

            if (!taskTypes.IsNullOrEmpty())
            {
                filter &= builder.In(x => x.TaskType, taskTypes);
            }

            if (!englishLevels.IsNullOrEmpty())
            {
                filter &= builder.In(x => x.EnglishLevel, englishLevels);
            }

            return await _collection
                .Find(filter)
                .Project(InfoModelProjectionDefinition)
                .ToListAsync(); 
        }
    }
}
