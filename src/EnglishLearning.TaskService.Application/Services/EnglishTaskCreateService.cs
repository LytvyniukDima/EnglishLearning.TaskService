using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Linq.Extensions;
using EnglishLearning.Utilities.Persistence.Mongo.Extensions;

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
            var taskItems = await _taskItemRepository
                .FindAllAsync(x => x.GrammarPart == createModel.GrammarPart && x.TaskType == createModel.TaskType);

            var randomItems = taskItems
                .GetRandomCountOfElements(createModel.ItemsCount)
                .ToList();

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
    }
}