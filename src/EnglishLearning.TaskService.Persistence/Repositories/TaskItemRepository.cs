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
    internal class TaskItemRepository : BaseMongoRepository<TaskItem, string>, ITaskItemRepository
    {
        public TaskItemRepository(MongoContext mongoContext)
            : base(mongoContext)
        {
        }

        public async Task<IReadOnlyList<TaskItem>> FindAllByFilters(TaskItemsFilter filter)
        {
            var itemsFilter = BuildFilter(filter);

            return await _collection.Find(itemsFilter).ToListAsync();
        }

        private FilterDefinition<TaskItem> BuildFilter(TaskItemsFilter filter)
        {
            var filterBuilder = Builders<TaskItem>.Filter;
            var itemsFilter = filterBuilder.Empty;

            if (filter == null)
            {
                return itemsFilter;
            }
            
            if (!filter.GrammarPart.IsNullOrEmpty())
            {
                itemsFilter &= filterBuilder.In(x => x.GrammarPart, filter.GrammarPart);
            }
            
            if (!filter.TaskType.IsNullOrEmpty())
            {
                itemsFilter &= filterBuilder.In(x => x.TaskType, filter.TaskType);
            }
            
            if (!filter.SentType.IsNullOrEmpty())
            {
                itemsFilter &= filterBuilder.In(x => x.SentType, filter.SentType);
            }

            return itemsFilter;
        }
    }
}