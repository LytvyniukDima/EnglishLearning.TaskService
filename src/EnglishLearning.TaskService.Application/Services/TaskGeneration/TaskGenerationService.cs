using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Abstract.TaskGeneration;
using EnglishLearning.TaskService.Application.Abstract.TextAnalyze;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models.TaskGeneration;
using EnglishLearning.TaskService.Persistence.Abstract;

namespace EnglishLearning.TaskService.Application.Services.TaskGeneration
{
    internal class TaskGenerationService : ITaskGenerationService
    {
        private readonly ITaskGenerationRepository _taskGenerationRepository;

        private readonly IParsedSentService _parsedSentService;

        private readonly ITaskItemService _taskItemService;

        private readonly ITaskItemsGeneratorFactory _taskItemsGeneratorFactory;
        
        private readonly IMapper _mapper;
        
        public TaskGenerationService(
            ITaskGenerationRepository taskGenerationRepository,
            ApplicationMapper applicationMapper,
            IParsedSentService parsedSentService,
            ITaskItemService taskItemService,
            ITaskItemsGeneratorFactory taskItemsGeneratorFactory)
        {
            _taskGenerationRepository = taskGenerationRepository;
            _mapper = applicationMapper.Mapper;
            _parsedSentService = parsedSentService;
            _taskItemService = taskItemService;
            _taskItemsGeneratorFactory = taskItemsGeneratorFactory;
        }
        
        public async Task GenerateTasksAsync(GenerateTaskModel generateTaskModel)
        {
            var taskGenerationEntity = CreateTaskGenerationEntity(generateTaskModel);
            taskGenerationEntity = await _taskGenerationRepository.AddAsync(taskGenerationEntity);

            var parsedSents = await _parsedSentService.GetAllByAnalyzeAndGrammarPartAsync(
                generateTaskModel.AnalyzeId,
                generateTaskModel.GrammarPart);

            var taskGenerator = _taskItemsGeneratorFactory.GetTaskItemsGenerator(
                generateTaskModel.GrammarPart,
                generateTaskModel.TaskType);

            var taskItems = taskGenerator.GenerateTaskItems(
                taskGenerationEntity.Id,
                generateTaskModel,
                parsedSents);

            await _taskItemService.AddManyAsync(taskItems);
        }

        public async Task<IReadOnlyList<TaskGenerationModel>> GetAllAsync()
        {
            var entities = await _taskGenerationRepository.GetAllAsync();

            return _mapper.Map<IReadOnlyList<TaskGenerationModel>>(entities);
        }

        public async Task<TaskGenerationModel> GetByIdAsync(string id)
        {
            var entity = await _taskGenerationRepository.FindAsync(x => x.Id == id);

            return _mapper.Map<TaskGenerationModel>(entity);
        }

        private Persistence.Entities.TaskGeneration CreateTaskGenerationEntity(GenerateTaskModel generateTaskModel)
        {
            return new Persistence.Entities.TaskGeneration()
            {
                AnalyzeId = generateTaskModel.AnalyzeId,
                GrammarPart = generateTaskModel.GrammarPart,
                Name = generateTaskModel.Name,
                TaskType = generateTaskModel.TaskType,
                EnglishLevel = generateTaskModel.EnglishLevel,
                CreatedDateTime = DateTime.UtcNow,
            };
        }
    }
}