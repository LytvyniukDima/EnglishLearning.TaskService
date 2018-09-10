using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Common.Extensions;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.InternalServices;
using EnglishLearning.TaskService.Infrastructure.Extensions;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Application.Services
{
    public class RandomEnglishTaskService : IRandomEnglishTaskService
    {
        private readonly IMongoDbRepository<EnglishTask> _dbRepository;
        private readonly IMapper _mapper;

        public RandomEnglishTaskService(
            IMongoDbRepository<EnglishTask> dbRepository,
            EnglishTaskServiceMapper englishTaskServiceMapper)
        {
            _dbRepository = dbRepository;
            _mapper = englishTaskServiceMapper.Mapper;
        }

        public async Task<EnglishTaskDto> FindRandomEnglishTaskAsync(string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return null;
            
            IEnumerable<EnglishTask> englishTasks = await FilterEnglishTaskService.GetAllFilteredEnglishTasks(_dbRepository, taskTypes, grammarParts, englishLevels);

            if (!englishTasks.Any())
                return null;
            
            var englishTask = englishTasks.GetRandomElement();
            
            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskDto>(englishTask);
            
            return englishTaskDto;
        }

        public async Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(
            string[] taskTypes = null, 
            string[] grammarParts = null,
            string[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return null;
            
            IEnumerable<EnglishTask> englishTasks = await FilterEnglishTaskService.GetAllFilteredEnglishTasks(_dbRepository, taskTypes, grammarParts, englishLevels);;
            
            if (!englishTasks.Any())
                return null;
            
            var englishTask = englishTasks.GetRandomElement();
            
            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskInfoDto>(englishTask);
            
            return englishTaskDto;
        }

        public async Task<IEnumerable<EnglishTaskDto>> GetRandomFromAllEnglishTask(int count)
        {   
            IEnumerable<EnglishTask> englishTasks = await _dbRepository.GetAllAsync();
            
            if (!englishTasks.Any())
                return Enumerable.Empty<EnglishTaskDto>();

            IEnumerable<EnglishTask> randomedEnglishTasks = englishTasks.GetRandomCountOfElements(count);
            
            var englishTaskDtos = _mapper.Map<IEnumerable<EnglishTask>, IEnumerable<EnglishTaskDto>>(randomedEnglishTasks);
            
            return englishTaskDtos;
        }

        public async Task<IEnumerable<EnglishTaskDto>> FindRandomCountEnglishTask(int count, string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return null;
            
            IEnumerable<EnglishTask> englishTasks = await FilterEnglishTaskService.GetAllFilteredEnglishTasks(_dbRepository, taskTypes, grammarParts, englishLevels);
            
            if (!englishTasks.Any())
                return Enumerable.Empty<EnglishTaskDto>();

            IEnumerable<EnglishTask> randomedEnglishTasks = englishTasks.GetRandomCountOfElements(count);

            var englishTaskDtos = _mapper.Map<IEnumerable<EnglishTaskDto>>(randomedEnglishTasks);
            
            return englishTaskDtos;
        }

        public async Task<IEnumerable<EnglishTaskInfoDto>> GetRandomInfoFromAllEnglishTask(int count)
        {
            IEnumerable<EnglishTask> englishTasks = await _dbRepository.GetAllAsync();
            
            if (!englishTasks.Any())
                return Enumerable.Empty<EnglishTaskInfoDto>();

            IEnumerable<EnglishTask> randomedEnglishTasks = englishTasks.GetRandomCountOfElements(count);
            
            var englishTaskDtos = _mapper.Map<IEnumerable<EnglishTask>, IEnumerable<EnglishTaskInfoDto>>(randomedEnglishTasks);
            
            return englishTaskDtos;
        }

        public async Task<IEnumerable<EnglishTaskInfoDto>> FindRandomInfoCountEnglishTask(int count, string[] taskTypes = null, string[] grammarParts = null, string[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return null;
            
            IEnumerable<EnglishTask> englishTasks = await FilterEnglishTaskService.GetAllFilteredEnglishTasks(_dbRepository, taskTypes, grammarParts, englishLevels);
            
            if (!englishTasks.Any())
                return Enumerable.Empty<EnglishTaskInfoDto>();

            IEnumerable<EnglishTask> randomedEnglishTasks = englishTasks.GetRandomCountOfElements(count);

            var englishTaskDtos = _mapper.Map<IEnumerable<EnglishTaskInfoDto>>(randomedEnglishTasks);
            
            return englishTaskDtos;
        }
    }
}