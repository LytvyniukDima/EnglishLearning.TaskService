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
        private readonly IUserInformationService _userInformationService;
        
        public RandomEnglishTaskService(
            IEnglishTaskRepository taskRepository,
            EnglishTaskServiceMapper englishTaskServiceMapper,
            IUserInformationService userInformationService)
        {
            _taskRepository = taskRepository;
            _mapper = englishTaskServiceMapper.Mapper;
            _userInformationService = userInformationService;
        }

        public async Task<EnglishTaskDto> FindRandomEnglishTaskAsync(
            IReadOnlyList<string> grammarParts = null, 
            IReadOnlyList<TaskType> taskTypes = null, 
            IReadOnlyList<EnglishLevel> englishLevels = null)
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

        public async Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(
            IReadOnlyList<string> grammarParts = null, 
            IReadOnlyList<TaskType> taskTypes = null, 
            IReadOnlyList<EnglishLevel> englishLevels = null)
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

        public async Task<IReadOnlyList<EnglishTaskDto>> GetRandomWithUserPreferencesEnglishTask(int count)
        {
            var userInformation = await _userInformationService.GetUserInformationForCurrentUser();
            if (userInformation == null)
            {
                return await GetRandomFromAllEnglishTask(count);
            }

            var englishLevels = new[] { userInformation.EnglishLevel };
            return await FindRandomCountEnglishTask(count, grammarParts: userInformation.FavouriteGrammarParts, englishLevels: englishLevels);
        }

        public async Task<IReadOnlyList<EnglishTaskDto>> FindRandomCountEnglishTask(
            int count, 
            IReadOnlyList<string> grammarParts = null, 
            IReadOnlyList<TaskType> taskTypes = null, 
            IReadOnlyList<EnglishLevel> englishLevels = null)
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

        public async Task<IReadOnlyList<EnglishTaskInfoDto>> GetRandomInfoWithUserPreferencesEnglishTask(int count)
        {
            var userInformation = await _userInformationService.GetUserInformationForCurrentUser();
            if (userInformation == null)
            {
                return await GetRandomInfoFromAllEnglishTask(count);
            }

            var englishLevels = new[] { userInformation.EnglishLevel };
            return await FindRandomInfoCountEnglishTask(count, grammarParts: userInformation.FavouriteGrammarParts, englishLevels: englishLevels);
        }

        public async Task<IReadOnlyList<EnglishTaskInfoDto>> FindRandomInfoCountEnglishTask(
            int count, 
            IReadOnlyList<string> grammarParts = null, 
            IReadOnlyList<TaskType> taskTypes = null, 
            IReadOnlyList<EnglishLevel> englishLevels = null)
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
