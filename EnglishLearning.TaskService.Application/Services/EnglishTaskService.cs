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
        private readonly IUserInformationService _userInformationService;
        
        public EnglishTaskService(
            IEnglishTaskRepository taskRepository, 
            EnglishTaskServiceMapper englishTaskServiceMappermapper,
            IUserInformationService userInformationService)
        {
            _taskRepository = taskRepository;
            _mapper = englishTaskServiceMappermapper.Mapper;
            _userInformationService = userInformationService;
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
            var englishTasks = await _taskRepository.GetAllAsync();
            var englishTaskDtos = _mapper.Map<IReadOnlyList<EnglishTaskDto>>(englishTasks);
            
            return englishTaskDtos;
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

        public async Task<IReadOnlyList<EnglishTaskInfoDto>> GetAllEnglishTaskInfoWithUserPreferencesAsync()
        {
            var userInformation = await _userInformationService.GetUserInformationForCurrentUser();
            if (userInformation == null)
            {
                return await GetAllEnglishTaskInfoAsync();
            }

            var englishLevels = new[] { userInformation.EnglishLevel };
            return await FindAllInfoEnglishTaskAsync(grammarParts: userInformation.FavouriteGrammarParts, englishLevels: englishLevels);
        }

        public async Task<IReadOnlyList<EnglishTaskDto>> FindAllEnglishTaskAsync(IReadOnlyList<string> grammarParts = null, IReadOnlyList<TaskType> taskTypes = null, IReadOnlyList<EnglishLevel> englishLevels = null)
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

        public async Task<IReadOnlyList<EnglishTaskInfoDto>> FindAllInfoEnglishTaskAsync(
            IReadOnlyList<string> grammarParts = null, 
            IReadOnlyList<TaskType> taskTypes = null, 
            IReadOnlyList<EnglishLevel> englishLevels = null)
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
    }
}
