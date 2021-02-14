using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Application.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;

        private readonly IMapper _mapper;

        public TaskItemService(
            ITaskItemRepository taskItemRepository,
            ApplicationMapper applicationMapper)
        {
            _taskItemRepository = taskItemRepository;
            _mapper = applicationMapper.Mapper;
        }
        
        public Task AddManyAsync(IReadOnlyList<CreateTaskItemModel> taskItems)
        {
            var entities = _mapper.Map<IReadOnlyList<TaskItem>>(taskItems);

            return _taskItemRepository.AddManyAsync(entities);
        }
    }
}