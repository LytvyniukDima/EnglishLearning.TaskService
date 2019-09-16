using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.Filtering;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Linq.Extensions;

namespace EnglishLearning.TaskService.Application.Services
{
    public class RandomEnglishTaskService : IRandomEnglishTaskService
    {
        private readonly IEnglishTaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IUserFilterService _userFilterService;
        
        public RandomEnglishTaskService(
            IEnglishTaskRepository taskRepository,
            ApplicationMapper applicationMapper,
            IUserFilterService userFilterService)
        {
            _taskRepository = taskRepository;
            _mapper = applicationMapper.Mapper;
            _userFilterService = userFilterService;
        }

        public async Task<EnglishTaskModel> FindRandomEnglishTaskAsync(BaseFilterModel filterModel)
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
            var englishTaskModel = _mapper.Map<EnglishTaskModel>(englishTask);
            
            return englishTaskModel;
        }

        public async Task<EnglishTaskInfoModel> FindRandomInfoEnglishTaskAsync(BaseFilterModel filterModel)
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
            var englishTaskModel = _mapper.Map<EnglishTaskInfoModel>(englishTask);
            
            return englishTaskModel;
        }

        public async Task<IReadOnlyList<EnglishTaskModel>> GetRandomFromAllEnglishTask(int count)
        {   
            IEnumerable<EnglishTask> englishTasks = await _taskRepository.GetAllAsync();

            if (!englishTasks.Any())
            {
                return Array.Empty<EnglishTaskModel>();
            }

            IEnumerable<EnglishTask> randomedEnglishTasks = englishTasks.GetRandomCountOfElements(count);
            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskModel>>(randomedEnglishTasks);
            
            return englishTaskModels;
        }

        public async Task<IReadOnlyList<EnglishTaskModel>> GetRandomWithUserPreferencesEnglishTask(int count)
        {
            var filterModel = await _userFilterService.GetFilterModelForCurrentUser(count);
            
            return await FindRandomCountEnglishTask(count, filterModel);
        }

        public async Task<IReadOnlyList<EnglishTaskModel>> FindRandomCountEnglishTask(
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
                return Array.Empty<EnglishTaskModel>();
            }

            IEnumerable<EnglishTask> randomedEnglishTasks = englishTasks.GetRandomCountOfElements(count);
            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskModel>>(randomedEnglishTasks);
            
            return englishTaskModels;
        }

        public async Task<IReadOnlyList<EnglishTaskInfoModel>> GetRandomInfoFromAllEnglishTask(int count)
        {
            IEnumerable<EnglishTask> englishTasks = await _taskRepository.GetAllAsync();
            
            if (!englishTasks.Any())
            {
                return Array.Empty<EnglishTaskInfoModel>();
            }

            IEnumerable<EnglishTask> randomedEnglishTasks = englishTasks.GetRandomCountOfElements(count);
            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskInfoModel>>(randomedEnglishTasks);
            
            return englishTaskModels;
        }

        public async Task<IReadOnlyList<EnglishTaskInfoModel>> GetRandomInfoWithUserPreferencesEnglishTask(int count)
        {
            var filterModel = await _userFilterService.GetFilterModelForCurrentUser(count);
            
            return await FindRandomInfoCountEnglishTask(count, filterModel);
        }

        public async Task<IReadOnlyList<EnglishTaskInfoModel>> FindRandomInfoCountEnglishTask(
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
                return Array.Empty<EnglishTaskInfoModel>();
            }

            IEnumerable<EnglishTaskInfo> randomedEnglishTasks = englishTasks.GetRandomCountOfElements(count);
            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskInfoModel>>(randomedEnglishTasks);
            
            return englishTaskModels;
        }
    }
}
