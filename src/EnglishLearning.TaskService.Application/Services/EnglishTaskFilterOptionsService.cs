using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models.Filtering;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Persistence.Interfaces;

namespace EnglishLearning.TaskService.Application.Services
{
    public class EnglishTaskFilterOptionsService : IEnglishTaskFilterOptionsService
    {
        private const string TaskInformationCacheKey = "EnglishTasks_TaskInformations";
        private const string FullFilterCacheKey = "EnglishTasks_FullFilter";
        
        private readonly IEnglishTaskFilterOptionsRepository _repository;
        private readonly IKeyValueRepository _cacheRepository;
        private readonly IMapper _mapper;
        
        public EnglishTaskFilterOptionsService(
            IEnglishTaskFilterOptionsRepository repository,
            IKeyValueRepository cacheRepository,
            ApplicationMapper serviceMapper)
        {
            _repository = repository;
            _cacheRepository = cacheRepository;
            _mapper = serviceMapper.Mapper;
        }

        public Task<Dictionary<string, int>> GetGrammarPartFilterOptions()
        {
            return _repository.GetGrammarPartFilterOptions();
        }

        public Task<Dictionary<EnglishLevel, int>> GetEnglishLevelFilterOptions()
        {
            return _repository.GetEnglishLevelFilterOptions();
        }

        public Task<Dictionary<TaskType, int>> GetTaskTypeFilterOptions()
        {
            return _repository.GetTaskTypeFilterOptions();
        }

        public async Task<EnglishTaskFullFilterModel> GetFullFilter()
        {
            var cachedFilter = _cacheRepository.GetObjectValue<EnglishTaskFullFilterModel>(FullFilterCacheKey);
            if (cachedFilter != null)
            {
                return cachedFilter;
            }
            
            var fromDbFilter = await GetFromDbFullFilter(); 
            _cacheRepository.SetObjectValue(FullFilterCacheKey, fromDbFilter);

            return fromDbFilter;
        }

        public async Task<IReadOnlyList<PerEnglishLevelTaskInformationModel>> GetTaskInformationModels()
        {
            var cachedModels = _cacheRepository.GetObjectValue<IReadOnlyList<PerEnglishLevelTaskInformationModel>>(TaskInformationCacheKey);
            if (cachedModels != null)
            {
                return cachedModels;
            }

            var fromDbModels = await GetFromDbTaskInformationModels();
            _cacheRepository.SetObjectValue(TaskInformationCacheKey, fromDbModels);

            return fromDbModels;
        }

        public async Task FillFiltersCache()
        {
            var fullFilter = await GetFromDbFullFilter();
            var taskInformationModels = await GetFromDbTaskInformationModels();
            
            _cacheRepository.SetObjectValue(FullFilterCacheKey, fullFilter);
            _cacheRepository.SetObjectValue(TaskInformationCacheKey, taskInformationModels);
        }
        
        private async Task<EnglishTaskFullFilterModel> GetFromDbFullFilter()
        {
            EnglishTaskFullFilter filter = await _repository.GetFullFilter();

            var applicationFilter = _mapper.Map<EnglishTaskFullFilterModel>(filter);
            return applicationFilter;
        }
        
        private async Task<IReadOnlyList<PerEnglishLevelTaskInformationModel>> GetFromDbTaskInformationModels()
        {
            IReadOnlyList<PerEnglishLevelTaskInformation> persistenceModels = await _repository.GetPerEnglishLevelTaskInformation();
            var applicationModels = _mapper.Map<IReadOnlyList<PerEnglishLevelTaskInformationModel>>(persistenceModels);

            return applicationModels;
        }
    }
}
