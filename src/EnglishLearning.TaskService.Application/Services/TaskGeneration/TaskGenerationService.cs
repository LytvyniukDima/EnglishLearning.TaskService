using System;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract.TaskGeneration;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models.TaskGeneration;
using EnglishLearning.TaskService.Persistence.Abstract;

namespace EnglishLearning.TaskService.Application.Services.TaskGeneration
{
    internal class TaskGenerationService : ITaskGenerationService
    {
        private readonly ITaskGenerationRepository _taskGenerationRepository;

        private IMapper _mapper;

        public TaskGenerationService(
            ITaskGenerationRepository taskGenerationRepository,
            ApplicationMapper applicationMapper)
        {
            _taskGenerationRepository = taskGenerationRepository;
            _mapper = applicationMapper.Mapper;
        }
        
        public async Task GenerateTasksAsync(GenerateTaskModel generateTaskModel)
        {
            var taskGenerationEntity = CreateTaskGenerationEntity(generateTaskModel);
            taskGenerationEntity = await _taskGenerationRepository.AddAsync(taskGenerationEntity);
        }

        private Persistence.Entities.TaskGeneration CreateTaskGenerationEntity(GenerateTaskModel generateTaskModel)
        {
            return new Persistence.Entities.TaskGeneration()
            {
                SourceId = generateTaskModel.AnalyzeId,
                GrammarPart = generateTaskModel.GrammarPart,
                Name = generateTaskModel.Name,
                TaskType = generateTaskModel.TaskType,
                CreatedDateTime = DateTime.UtcNow,
            };
        }
    }
}