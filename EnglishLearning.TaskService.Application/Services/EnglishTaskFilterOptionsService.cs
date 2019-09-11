using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models.Filtering;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Application.Services
{
    public class EnglishTaskFilterOptionsService : IEnglishTaskFilterOptionsService
    {
        private readonly IEnglishTaskFilterOptionsRepository _repository;
        private readonly IMapper _mapper;
        
        public EnglishTaskFilterOptionsService(IEnglishTaskFilterOptionsRepository repository, ApplicationMapper serviceMapper)
        {
            _repository = repository;
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
            EnglishTaskFullFilter filter = await _repository.GetFullFilter();

            var applicationFilter = _mapper.Map<EnglishTaskFullFilterModel>(filter);
            return applicationFilter;
        }
    }
}
