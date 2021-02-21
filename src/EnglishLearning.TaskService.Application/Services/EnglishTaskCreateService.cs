using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
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
                .ToArray();

            var contentArray = ToBsonArray(taskContent);
            
            var englishTask = new EnglishTask
            {
                GrammarPart = createModel.GrammarPart,
                TaskType = createModel.TaskType,
                EnglishLevel = createModel.EnglishLevel,
                Count = taskItems.Count,
                Content = contentArray,
            };

            await _englishTaskRepository.AddAsync(englishTask);
        }

        private BsonArray ToBsonArray(IEnumerable<BsonValue> values)
        {
            var array = new BsonArray();
            array.AddRange(values);

            return array;
        }
    }
}