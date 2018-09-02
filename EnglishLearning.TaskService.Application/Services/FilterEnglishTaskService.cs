using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Common;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Application.Services
{
    public class FilterEnglishTaskService : IFilterEnglishTaskService
    {
        private readonly IMongoDbRepository<EnglishTask> _dbRepository;
        private readonly IMapper _mapper;
        
        public FilterEnglishTaskService(
            IMongoDbRepository<EnglishTask> dbRepository,
            EnglishTaskServiceMapper englishTaskServiceMapper)
        {
            _dbRepository = dbRepository;
            _mapper = englishTaskServiceMapper.Mapper;
        }
        
        public async Task<IEnumerable<EnglishTaskDto>> FindAllEnglishTaskAsync(Expression<Func<EnglishTaskFilterDto, bool>> filter)
        {
            IEnumerable<EnglishTask> englishTasks = await GetAllEnglishTasks(filter);

            if (!englishTasks.Any())
                return Enumerable.Empty<EnglishTaskDto>();
            
            var englishTaskDtos = _mapper.Map<IEnumerable<EnglishTask>, IEnumerable<EnglishTaskDto>>(englishTasks);
            
            return englishTaskDtos;
        }

        public async Task<IEnumerable<EnglishTaskDto>> FindAllEnglishTaskAsync(
            string[] taskTypes = null, 
            string[] grammarParts = null, 
            string[] englishLevels =  null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return Enumerable.Empty<EnglishTaskDto>();

            Expression<Func<EnglishTaskFilterDto, bool>> expression =
                CreateExpression(taskTypes, grammarParts, englishLevels);
            
            IEnumerable<EnglishTask> englishTasks = await GetAllEnglishTasks(expression);
            
            if (!englishTasks.Any())
                return Enumerable.Empty<EnglishTaskDto>();
            
            var englishTaskDtos = _mapper.Map<IEnumerable<EnglishTask>, IEnumerable<EnglishTaskDto>>(englishTasks);
            
            return englishTaskDtos;
        }

        public async Task<EnglishTaskDto> FindRandomEnglishTaskAsync(Expression<Func<EnglishTaskFilterDto, bool>> filter)
        {
            IEnumerable<EnglishTask> englishTasks = await GetAllEnglishTasks(filter);

            if (!englishTasks.Any())
                return null;

            var englishTask = englishTasks.GetRandomValue();
            
            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskDto>(englishTask);
            
            return englishTaskDto;
        }

        public async Task<EnglishTaskDto> FindRandomEnglishTaskAsync(
            string[] taskTypes = null, 
            string[] grammarParts = null, 
            string[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return null;

            Expression<Func<EnglishTaskFilterDto, bool>> expression =
                CreateExpression(taskTypes, grammarParts, englishLevels);
            
            IEnumerable<EnglishTask> englishTasks = await GetAllEnglishTasks(expression);

            if (!englishTasks.Any())
                return null;
            
            var englishTask = englishTasks.GetRandomValue();
            
            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskDto>(englishTask);
            
            return englishTaskDto;
        }

        public async Task<IEnumerable<EnglishTaskInfoDto>> FindAllInfoEnglishTaskAsync(Expression<Func<EnglishTaskFilterDto, bool>> filter)
        {
            IEnumerable<EnglishTask> englishTasks = await GetAllEnglishTasks(filter);

            if (!englishTasks.Any())
                return Enumerable.Empty<EnglishTaskInfoDto>();
            
            var englishTaskDtos = _mapper.Map<IEnumerable<EnglishTask>, IEnumerable<EnglishTaskInfoDto>>(englishTasks);
            
            return englishTaskDtos;
        }

        public async Task<IEnumerable<EnglishTaskInfoDto>> FindAllInfoEnglishTaskAsync(
            string[] taskTypes = null, 
            string[] grammarParts = null, 
            string[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return Enumerable.Empty<EnglishTaskInfoDto>();

            Expression<Func<EnglishTaskFilterDto, bool>> expression =
                CreateExpression(taskTypes, grammarParts, englishLevels);
            
            IEnumerable<EnglishTask> englishTasks = await GetAllEnglishTasks(expression);
            
            if (!englishTasks.Any())
                return Enumerable.Empty<EnglishTaskInfoDto>();
            
            var englishTaskDtos = _mapper.Map<IEnumerable<EnglishTask>, IEnumerable<EnglishTaskInfoDto>>(englishTasks);
            
            return englishTaskDtos;
        }

        public async Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(Expression<Func<EnglishTaskFilterDto, bool>> filter)
        {
            IEnumerable<EnglishTask> englishTasks = await GetAllEnglishTasks(filter);

            if (!englishTasks.Any())
                return null;
            
            var englishTask = englishTasks.GetRandomValue();
            
            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskInfoDto>(englishTask);
            
            return englishTaskDto;
        }

        public async Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(
            string[] taskTypes = null, 
            string[] grammarParts= null, 
            string[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return null;

            Expression<Func<EnglishTaskFilterDto, bool>> expression =
                CreateExpression(taskTypes, grammarParts, englishLevels);
            
            IEnumerable<EnglishTask> englishTasks = await GetAllEnglishTasks(expression);
            
            if (!englishTasks.Any())
                return null;
            
            var englishTask = englishTasks.GetRandomValue();
            
            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskInfoDto>(englishTask);
            
            return englishTaskDto;
        }

        private async Task<IEnumerable<EnglishTask>> GetAllEnglishTasks(Expression<Func<EnglishTaskFilterDto, bool>> filter)
        {
            var englishTaskFilter = _mapper.Map<Expression<Func<EnglishTask, bool>>>(filter);

            var englishTasks = await _dbRepository.FindAllAsync(englishTaskFilter);

            return englishTasks;
        }

        private Expression<Func<EnglishTaskFilterDto, bool>> CreateExpression(
            string[] taskTypes = null,
            string[] grammarParts = null,
            string[] englishLevels = null)
        {
            Expression<Func<EnglishTaskFilterDto, bool>> finalExpression = default(Expression<Func<EnglishTaskFilterDto, bool>>);;

            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return finalExpression;

            if (taskTypes.Any())
            {
                var taskTypeExpression = new List<Expression<Func<EnglishTaskFilterDto, bool>>>();

                foreach (var taskType in taskTypes)
                    taskTypeExpression.Add(x => x.TaskType == taskType);                

                finalExpression = taskTypeExpression[0];

                for (var i = 1; i < taskTypeExpression.Count; i++)
                    finalExpression = finalExpression.Or(taskTypeExpression[i]);
            }

            if (grammarParts.Any())
            {
                var grammarTypeExpressions = new List<Expression<Func<EnglishTaskFilterDto, bool>>>();

                foreach (var grammarPart in grammarParts)           
                    grammarTypeExpressions.Add(x => x.GrammarPart == grammarPart);

                Expression<Func<EnglishTaskFilterDto, bool>> grammarTypeExpression = grammarTypeExpressions[0];

                for (var i = 1; i < grammarTypeExpressions.Count; i++)
                    grammarTypeExpression.Or(grammarTypeExpressions[i]);

                finalExpression.And(grammarTypeExpression);
            }

            if (englishLevels.Any())
            {
                var englishLevelExpresions = new List<Expression<Func<EnglishTaskFilterDto, bool>>>();

                foreach (var englishLevel in englishLevels)
                    englishLevelExpresions.Add(x => x.EnglishLevel == englishLevel);

                Expression<Func<EnglishTaskFilterDto, bool>> englishLevelExpression = englishLevelExpresions[0];

                for (var i = 1; i < englishLevelExpresions.Count; i++)
                    englishLevelExpression.Or(englishLevelExpresions[i]);

                finalExpression.And(englishLevelExpression);
            }

            return finalExpression;
        }
    }
}