using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Application.Infrastructure;
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

        public async Task<EnglishTaskDto> FindRandomEnglishTaskAsync(BaseFilterModel filterModel)
        {
            if (filterModel == null || filterModel.IsEmpty())
            {
                return null;
            }

            var persistenceFilter = _mapper.Map<BaseFilter>(filterModel);
            IReadOnlyList<EnglishTask> englishTasks = await _taskRepository.FindAllByFilters(persistenceFilter);

            if (!englishTasks.Any())
            {
                return null;
            }

            var englishTask = englishTasks.GetRandomElement();
            var englishTaskDto = _mapper.Map<EnglishTaskDto>(englishTask);
            
            return englishTaskDto;
        }

        public async Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(BaseFilterModel filterModel)
        {
            if (filterModel == null || filterModel.IsEmpty())
            {
                return null;
            }

            var persistenceFilter = _mapper.Map<BaseFilter>(filterModel);
            IReadOnlyList<EnglishTaskInfo> englishTasks = await _taskRepository.FindAllInfoByFilters(persistenceFilter);
            
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

            var filterModel = BaseFilterModel.CreateFromUserInformation(userInformation);
            return await FindRandomCountEnglishTask(count, filterModel);
        }

        public async Task<IReadOnlyList<EnglishTaskDto>> FindRandomCountEnglishTask(
            int count, 
            BaseFilterModel filterModel)
        {
            if (filterModel == null || filterModel.IsEmpty())
            {
                return null;
            }

            var persistenceFilter = _mapper.Map<BaseFilter>(filterModel);
            IReadOnlyList<EnglishTask> englishTasks = await _taskRepository.FindAllByFilters(persistenceFilter);
            
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

            var filterModel = BaseFilterModel.CreateFromUserInformation(userInformation);
            return await FindRandomInfoCountEnglishTask(count, filterModel);
        }

        public async Task<IReadOnlyList<EnglishTaskInfoDto>> FindRandomInfoCountEnglishTask(
            int count, 
            BaseFilterModel filterModel)
        {
            if (filterModel == null || filterModel.IsEmpty())
            {
                return null;
            }

            var persistenceFilter = _mapper.Map<BaseFilter>(filterModel);
            IReadOnlyList<EnglishTaskInfo> englishTasks = await _taskRepository.FindAllInfoByFilters(persistenceFilter);
            
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
