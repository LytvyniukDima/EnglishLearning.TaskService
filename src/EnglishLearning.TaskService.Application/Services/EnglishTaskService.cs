using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.Filtering;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Application.Services
{
    public class EnglishTaskService : IEnglishTaskService
    {
        private readonly IEnglishTaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IUserFilterService _userFilterService;
        public EnglishTaskService(
            IEnglishTaskRepository taskRepository, 
            ApplicationMapper applicationMapper,
            IUserFilterService userFilterService)
        {
            _taskRepository = taskRepository;
            _mapper = applicationMapper.Mapper;
            _userFilterService = userFilterService;
        }

        public async Task CreateEnglishTaskAsync(EnglishTaskCreateModel englishTaskCreateModel)
        {
            var englishTask = _mapper.Map<EnglishTaskCreateModel, EnglishTask>(englishTaskCreateModel);

            await _taskRepository.AddAsync(englishTask);
        }

        public async Task<bool> UpdateEnglishTaskAsync(string id, EnglishTaskCreateModel englishTaskModel)
        {
            var englishTask = _mapper.Map<EnglishTaskCreateModel, EnglishTask>(englishTaskModel);
            englishTask.Id = id;
            
            return await _taskRepository.UpdateAsync(englishTask);
        }

        public async Task<EnglishTaskModel> GetByIdEnglishTaskAsync(string id)
        {
            var englishTask = await GetEnglishTask(id);
            if (englishTask == null)
            {
                return null;
            }

            var englishTaskModel = _mapper.Map<EnglishTask, EnglishTaskModel>(englishTask);

            return englishTaskModel;
        }

        public async Task<IReadOnlyList<EnglishTaskModel>> GetAllEnglishTaskAsync()
        {
            var englishTasks = await _taskRepository.GetAllAsync();
            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskModel>>(englishTasks);
            
            return englishTaskModels;
        }

        public async Task<bool> DeleteByIdEnglishTaskAsync(string id)
        {
            return await _taskRepository.DeleteAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteAllEnglishTaskAsync()
        {
            return await _taskRepository.DeleteAllAsync();
        }

        public async Task<EnglishTaskInfoModel> GetByIdEnglishTaskInfoAsync(string id)
        {
            var englishTask = await GetEnglishTask(id);
            if (englishTask == null)
            {
                return null;
            }

            var englishTaskModel = _mapper.Map<EnglishTaskInfoModel>(englishTask);

            return englishTaskModel;
        }

        public async Task<IReadOnlyList<EnglishTaskInfoModel>> GetAllEnglishTaskInfoAsync()
        {
            var englishTasks = await _taskRepository.GetAllInfoAsync();
            var englishTasksModel = _mapper.Map<IReadOnlyList<EnglishTaskInfoModel>>(englishTasks);

            return englishTasksModel;
        }

        public async Task<IReadOnlyList<EnglishTaskInfoModel>> GetAllEnglishTaskInfoWithUserPreferencesAsync()
        {
            var filterModel = await _userFilterService.GetFilterModelForCurrentUser(CommonConstants.DefaultTaskOnPageCount);
            
            return await FindAllInfoEnglishTaskAsync(filterModel);
        }

        public async Task<IReadOnlyList<EnglishTaskModel>> FindAllEnglishTaskAsync(BaseFilterModel baseFilterModel)
        {
            if (baseFilterModel == null || baseFilterModel.IsEmpty())
            {
                return Array.Empty<EnglishTaskModel>();
            }

            var persistenceFilter = _mapper.Map<BaseFilter>(baseFilterModel);
            var englishTasks = await _taskRepository.FindAllByFilters(persistenceFilter);
            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskModel>>(englishTasks);
            
            return englishTaskModels;
        }

        public async Task<IReadOnlyList<EnglishTaskInfoModel>> FindAllInfoEnglishTaskAsync(BaseFilterModel baseFilterModel)
        {
            if (baseFilterModel == null || baseFilterModel.IsEmpty())
            {
                return Array.Empty<EnglishTaskInfoModel>();
            }

            var persistenceFilter = _mapper.Map<BaseFilter>(baseFilterModel);
            var englishTasks = await _taskRepository.FindAllInfoByFilters(persistenceFilter);
            
            var englishTaskModels = _mapper.Map<IReadOnlyList<EnglishTaskInfoModel>>(englishTasks);
            
            return englishTaskModels;
        }

        private async Task<EnglishTask> GetEnglishTask(string id)
        {
            return await _taskRepository.FindAsync(x => x.Id == id);
        }
    }
}
