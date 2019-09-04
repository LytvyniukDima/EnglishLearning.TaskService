using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Linq.Extensions;

namespace EnglishLearning.TaskService.Application.Services
{
    public class EnglishTaskService : IEnglishTaskService
    {
        private readonly IEnglishTaskRepository _taskRepository;
        private readonly IMapper _mapper;
        
        public EnglishTaskService(IEnglishTaskRepository taskRepository, EnglishTaskServiceMapper englishTaskServiceMappermapper)
        {
            _taskRepository = taskRepository;
            _mapper = englishTaskServiceMappermapper.Mapper;
        }

        public async Task CreateEnglishTaskAsync(EnglishTaskCreateDto englishTaskCreateDto)
        {
            var englishTask = _mapper.Map<EnglishTaskCreateDto, EnglishTask>(englishTaskCreateDto);

            await _taskRepository.AddAsync(englishTask);
        }

        public async Task<bool> UpdateEnglishTaskAsync(string id, EnglishTaskCreateDto englishTaskDto)
        {
            var englishTask = _mapper.Map<EnglishTaskCreateDto, EnglishTask>(englishTaskDto);
            englishTask.Id = id;
            
            return await _taskRepository.UpdateAsync(englishTask);
        }

        public async Task<EnglishTaskDto> GetByIdEnglishTaskAsync(string id)
        {
            var englishTask = await GetEnglishTask(id);
            
            // TODO: Throw NotFoundException
            if (englishTask == null)
            {
                return null;
            }

            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskDto>(englishTask);

            return englishTaskDto;
        }

        public async Task<IReadOnlyList<EnglishTaskDto>> GetAllEnglishTaskAsync()
        {
            var englishTasks = await GetAllEnglishTasks();

            var englishTasksDto = _mapper.Map<IReadOnlyList<EnglishTaskDto>>(englishTasks);

            return englishTasksDto;
        }

        public async Task<bool> DeleteByIdEnglishTaskAsync(string id)
        {
            return await _taskRepository.DeleteAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteAllEnglishTaskAsync()
        {
            return await _taskRepository.DeleteAllAsync();
        }

        public async Task<EnglishTaskInfoDto> GetByIdEnglishTaskInfoAsync(string id)
        {
            var englishTask = await GetEnglishTask(id);
            
            // TODO: Throw NotFoundException
            if (englishTask == null)
            {
                return null;
            }

            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskInfoDto>(englishTask);

            return englishTaskDto;
        }

        public async Task<IReadOnlyList<EnglishTaskInfoDto>> GetAllEnglishTaskInfoAsync()
        {
            var englishTasks = await _taskRepository.GetAllInfoAsync();
            var englishTasksDto = _mapper.Map<IReadOnlyList<EnglishTaskInfoDto>>(englishTasks);

            return englishTasksDto;
        }

        public async Task<IReadOnlyList<EnglishTaskDto>> FindAllEnglishTaskAsync(string[] grammarParts = null, TaskType[] taskTypes = null, EnglishLevel[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
            {
                return Array.Empty<EnglishTaskDto>();
            }

            var taskTypesEntities = _mapper.Map<TaskType[]>(taskTypes);
            var englishLevelEntities = _mapper.Map<EnglishLevel[]>(englishLevels);
            
            var englishTasks = await _taskRepository.FindAllByFilters(grammarParts, taskTypesEntities, englishLevelEntities);
            var englishTaskDtos = _mapper.Map<IReadOnlyList<EnglishTaskDto>>(englishTasks);
            
            return englishTaskDtos;
        }

        public async Task<IReadOnlyList<EnglishTaskInfoDto>> FindAllInfoEnglishTaskAsync(string[] grammarParts = null, TaskType[] taskTypes = null, EnglishLevel[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
            {
                return Array.Empty<EnglishTaskInfoDto>();
            }

            var taskTypesEntities = _mapper.Map<TaskType[]>(taskTypes);
            var englishLevelEntities = _mapper.Map<EnglishLevel[]>(englishLevels);
            
            var englishTasks = await _taskRepository.FindAllInfoByFilters(grammarParts, taskTypesEntities, englishLevelEntities);
            
            var englishTaskDtos = _mapper.Map<IReadOnlyList<EnglishTaskInfoDto>>(englishTasks);
            
            return englishTaskDtos;
        }

        private async Task<EnglishTask> GetEnglishTask(string id)
        {
            return await _taskRepository.FindAsync(x => x.Id == id);
        }

        private async Task<IEnumerable<EnglishTask>> GetAllEnglishTasks()
        {
            return await _taskRepository.GetAllAsync();
        }
    }
}
