using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RandomEnglishTaskService : IRandomEnglishTaskService
    {
        private readonly IEnglishTaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public RandomEnglishTaskService(
            IEnglishTaskRepository taskRepository,
            EnglishTaskServiceMapper englishTaskServiceMapper)
        {
            _taskRepository = taskRepository;
            _mapper = englishTaskServiceMapper.Mapper;
        }

        public async Task<EnglishTaskDto> FindRandomEnglishTaskAsync(string[] grammarParts = null, TaskType[] taskTypes = null, EnglishLevel[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
            {
                return null;
            }
            
            IReadOnlyList<EnglishTask> englishTasks = await _taskRepository.FindAllByFilters(grammarParts, taskTypes, englishLevels);

            if (!englishTasks.Any())
            {
                return null;
            }

            var englishTask = englishTasks.GetRandomElement();
            var englishTaskDto = _mapper.Map<EnglishTaskDto>(englishTask);
            
            return englishTaskDto;
        }

        public async Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(string[] grammarParts = null, TaskType[] taskTypes = null, EnglishLevel[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
            {
                return null;
            }

            IReadOnlyList<EnglishTaskInfo> englishTasks = await _taskRepository.FindAllInfoByFilters(grammarParts, taskTypes, englishLevels);
            
            if (!englishTasks.Any())
            {
                return null;
            }

            var englishTask = englishTasks.GetRandomElement();
            var englishTaskDto = _mapper.Map<EnglishTaskInfoDto>(englishTask);
            
            return englishTaskDto;
        }

        public async Task<IReadOnlyList<EnglishTaskDto>> GetRandomFromAllEnglishTask(int count)
        {   
            IEnumerable<EnglishTask> englishTasks = await _taskRepository.GetAllAsync();

            if (!englishTasks.Any())
            {
                return Array.Empty<EnglishTaskDto>();
            }

            IEnumerable<EnglishTask> randomedEnglishTasks = englishTasks.GetRandomCountOfElements(count);
            var englishTaskDtos = _mapper.Map<IReadOnlyList<EnglishTaskDto>>(randomedEnglishTasks);
            
            return englishTaskDtos;
        }

        public async Task<IReadOnlyList<EnglishTaskDto>> FindRandomCountEnglishTask(int count, string[] grammarParts = null, TaskType[] taskTypes = null, EnglishLevel[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
            {
                return null;
            }

            IReadOnlyList<EnglishTask> englishTasks = await _taskRepository.FindAllByFilters(grammarParts, taskTypes, englishLevels);
            
            if (!englishTasks.Any())
            {
                return Array.Empty<EnglishTaskDto>();
            }

            IEnumerable<EnglishTask> randomedEnglishTasks = englishTasks.GetRandomCountOfElements(count);
            var englishTaskDtos = _mapper.Map<IReadOnlyList<EnglishTaskDto>>(randomedEnglishTasks);
            
            return englishTaskDtos;
        }

        public async Task<IReadOnlyList<EnglishTaskInfoDto>> GetRandomInfoFromAllEnglishTask(int count)
        {
            IEnumerable<EnglishTask> englishTasks = await _taskRepository.GetAllAsync();
            
            if (!englishTasks.Any())
            {
                return Array.Empty<EnglishTaskInfoDto>();
            }

            IEnumerable<EnglishTask> randomedEnglishTasks = englishTasks.GetRandomCountOfElements(count);
            var englishTaskDtos = _mapper.Map<IReadOnlyList<EnglishTaskInfoDto>>(randomedEnglishTasks);
            
            return englishTaskDtos;
        }

        public async Task<IReadOnlyList<EnglishTaskInfoDto>> FindRandomInfoCountEnglishTask(int count, string[] grammarParts = null, TaskType[] taskTypes = null, EnglishLevel[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
            {
                return null;
            }

            IReadOnlyList<EnglishTaskInfo> englishTasks = await _taskRepository.FindAllInfoByFilters(grammarParts, taskTypes, englishLevels);
            
            if (!englishTasks.Any())
            {
                return Array.Empty<EnglishTaskInfoDto>();
            }

            IEnumerable<EnglishTaskInfo> randomedEnglishTasks = englishTasks.GetRandomCountOfElements(count);
            var englishTaskDtos = _mapper.Map<IReadOnlyList<EnglishTaskInfoDto>>(randomedEnglishTasks);
            
            return englishTaskDtos;
        }
    }
}
