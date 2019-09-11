using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using MongoDB.Driver;

namespace EnglishLearning.TaskService.Persistence.Repositories
{
    public class EnglishTaskFilterOptionsRepository : IEnglishTaskFilterOptionsRepository
    {
        private readonly MongoContext _mongoContext;
        private readonly IMongoCollection<EnglishTask> _collection;

        public EnglishTaskFilterOptionsRepository(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
            _collection = mongoContext.GetCollection<EnglishTask>();
        }
        
        public async Task<Dictionary<string, int>> GetGrammarPartFilterOptions()
        {
            var aggregateResult = await _collection
                .Aggregate()
                .Group(x => x.GrammarPart, group => new
                {
                    Key = group.Key,
                    Value = group.Count(),
                })
                .ToListAsync();

            var filterOptions = aggregateResult.ToDictionary(x => x.Key, x => x.Value);
            return filterOptions;
        }

        public async Task<Dictionary<EnglishLevel, int>> GetEnglishLevelFilterOptions()
        {
            var aggregateResult = await _collection
                .Aggregate()
                .Group(x => x.EnglishLevel, group => new
                {
                    Key = group.Key,
                    Value = group.Count(),
                })
                .ToListAsync();

            var filterOptions = aggregateResult.ToDictionary(x => x.Key, x => x.Value);
            return filterOptions;
        }

        public async Task<Dictionary<TaskType, int>> GetTaskTypeFilterOptions()
        {
            var aggregateResult = await _collection
                .Aggregate()
                .Group(x => x.TaskType, group => new
                {
                    Key = group.Key,
                    Value = group.Count(),
                })
                .ToListAsync();
                
            var filterOptions = aggregateResult.ToDictionary(x => x.Key, x => x.Value);

            return filterOptions;
        }

        public async Task<EnglishTaskFullFilter> GetFullFilter()
        {
            return new EnglishTaskFullFilter()
            {
                GrammarPartFilterOptions = await GetGrammarPartFilterOptions(),
                EnglishLevelFilterOptions = await GetEnglishLevelFilterOptions(),
                TaskTypeFilterOptions = await GetTaskTypeFilterOptions(),
            };
        }
    }
}
