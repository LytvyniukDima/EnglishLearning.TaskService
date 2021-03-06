﻿using System.Collections.Generic;
using System.Linq;
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

        public async Task<TaskItemsFilter> GetAvailableFilters()
        {
            var taskItemInfoProjection = Builders<TaskItem>
                .Projection
                .Expression(x => new TaskItemInfo
                {
                    Id = x.Id,
                    GrammarPart = x.GrammarPart,
                    SentType = x.SentType,
                    TaskType = x.TaskType,
                    EnglishLevel = x.EnglishLevel,
                });

            var items = await _collection.Find(_ => true)
                .Project(taskItemInfoProjection)
                .ToListAsync();

            var grammarParts = items
                .Select(x => x.GrammarPart)
                .Distinct()
                .ToArray();
            var sentTypes = items
                .Select(x => x.SentType)
                .Distinct()
                .ToArray();
            var taskTypes = items
                .Select(x => x.TaskType)
                .Distinct()
                .ToArray();
            var englishLevels = items
                .Select(x => x.EnglishLevel)
                .Distinct()
                .ToArray();
            
            return new TaskItemsFilter
            {
                GrammarPart = grammarParts,
                SentType = sentTypes,
                TaskType = taskTypes,
                EnglishLevel = englishLevels,
            };
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

            if (!filter.EnglishLevel.IsNullOrEmpty())
            {
                itemsFilter &= filterBuilder.In(x => x.EnglishLevel, filter.EnglishLevel);
            }
            
            return itemsFilter;
        }
    }
}