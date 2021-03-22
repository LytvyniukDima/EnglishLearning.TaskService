using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Linq.Extensions;
using EnglishLearning.Utilities.Persistence.Mongo.Extensions;
using MongoDB.Bson;

namespace EnglishLearning.TaskService.Application.Services
{
    internal class EnglishTaskCreateService : IEnglishTaskCreateService
    {
        private readonly IEnglishTaskRepository _englishTaskRepository;

        private readonly ITaskItemRepository _taskItemRepository;

        private readonly IMapper _mapper;

        public EnglishTaskCreateService(
            IEnglishTaskRepository englishTaskRepository,
            ITaskItemRepository taskItemRepository,
            ApplicationMapper applicationMapper)
        {
            _englishTaskRepository = englishTaskRepository;
            _taskItemRepository = taskItemRepository;
            _mapper = applicationMapper.Mapper;
        }
        
        public async Task CreateFromItemsAsync(EnglishTaskFromItemsCreateModel createModel)
        {
            var taskItems = await _taskItemRepository
                .FindAllAsync(x => createModel.Items.Contains(x.Id));

            var taskContent = taskItems
                .Select(x => x.Content)
                .ToBsonArray();

            var englishTask = new EnglishTask
            {
                GrammarPart = createModel.GrammarPart,
                TaskType = createModel.TaskType,
                EnglishLevel = createModel.EnglishLevel,
                Count = taskItems.Count,
                Content = taskContent,
            };

            await _englishTaskRepository.AddAsync(englishTask);
        }

        public async Task CreateFromRandomItemsAsync(EnglishTaskFromRandomItemsCreateModel createModel)
        {
            var randomItems = await GetRandomTaskItemsAsync(
                createModel.EnglishLevel,
                createModel.GrammarPart,
                createModel.TaskType,
                createModel.ItemsCount);

            var taskContent = randomItems
                .Select(x => x.Content)
                .ToBsonArray();

            var englishTask = new EnglishTask
            {
                GrammarPart = createModel.GrammarPart,
                TaskType = createModel.TaskType,
                EnglishLevel = createModel.EnglishLevel,
                Count = randomItems.Count,
                Content = taskContent,
            };

            await _englishTaskRepository.AddAsync(englishTask);
        }

        public async Task<EnglishTaskModel> CreateRandomTaskAsync(CreateRandomTaskModel createModel)
        {
            var randomItems = await GetRandomTaskItemsAsync(
                createModel.EnglishLevel,
                createModel.GrammarPart,
                createModel.TaskType,
                createModel.Count);
            
            var taskContent = randomItems
                .Select(x => x.Content)
                .ToBsonArray();

            var englishTask = new EnglishTaskModel()
            {
                GrammarPart = createModel.GrammarPart,
                TaskType = createModel.TaskType,
                EnglishLevel = createModel.EnglishLevel,
                Count = randomItems.Count,
                Content = taskContent.ToJson(),
            };

            return englishTask;
        }

        private async Task<IReadOnlyList<TaskItem>> GetRandomTaskItemsAsync(
            EnglishLevel englishLevel,
            string grammarPart,
            TaskType taskType,
            int count)
        {
            var englishLevels = new List<EnglishLevel>() { englishLevel };
            if (englishLevel == EnglishLevel.Intermediate)
            {
                englishLevels.Add(EnglishLevel.PreIntermediate);
                englishLevels.Add(EnglishLevel.Elementary);
            }
            
            var taskItems = await _taskItemRepository
                .FindAllAsync(x => 
                    x.GrammarPart == grammarPart 
                    && x.TaskType == taskType);
            
            taskItems = taskItems
                .Where(x => englishLevels.Contains(x.EnglishLevel))
                .ToList();
            
            var randomItems = taskItems
                .GetRandomCountOfElements(count)
                .ToList();

            return randomItems;
        }
    }
}