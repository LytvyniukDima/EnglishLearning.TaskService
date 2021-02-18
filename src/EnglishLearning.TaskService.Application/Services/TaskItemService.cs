using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Abstract.TaskGeneration;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.TaskGeneration;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace EnglishLearning.TaskService.Application.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;

        private readonly ITaskGenerationRepository _taskGenerationRepository;
        
        private readonly IMapper _mapper;

        public TaskItemService(
            ITaskItemRepository taskItemRepository,
            ITaskGenerationRepository taskGenerationRepository,
            ApplicationMapper applicationMapper)
        {
            _taskItemRepository = taskItemRepository;
            _taskGenerationRepository = taskGenerationRepository;
            _mapper = applicationMapper.Mapper;
        }
        
        public Task AddManyAsync(IReadOnlyList<CreateTaskItemModel> taskItems)
        {
            var entities = _mapper.Map<IReadOnlyList<TaskItem>>(taskItems);

            return _taskItemRepository.AddManyAsync(entities);
        }

        public async Task<IReadOnlyList<TaskItemModel>> GetAllAsync()
        {
            var entities = await _taskItemRepository.GetAllAsync();
            var taskGenerationMap = (await _taskGenerationRepository.GetAllAsync())
                .ToDictionary(x => x.Id);

            return entities
                .Select(x => MapTaskItemModel(x, taskGenerationMap))
                .ToList();
        }

        private TaskItemModel MapTaskItemModel(
            TaskItem taskItem,
            IReadOnlyDictionary<string, Persistence.Entities.TaskGeneration> taskGenerationMap)
        {
            var generateTaskModel = taskGenerationMap[taskItem.TaskGenerationId];
            
            return new TaskItemModel
            {
                Id = taskItem.Id,
                GenerationName = generateTaskModel.Name,
                GrammarPart = taskItem.GrammarPart,
                SentType = taskItem.SentType,
                SourceSentId = taskItem.SourceSentId,
                TaskGenerationId = taskItem.TaskGenerationId,
                TaskType = taskItem.TaskType,
                Content = taskItem.Content.ToJson(null, null, null, default(BsonSerializationArgs)),
            };
        }
    }
}