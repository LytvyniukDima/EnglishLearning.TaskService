using System;
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

        public async Task<IEnumerable<EnglishTaskDto>> FindAllEnglishTaskAsync(
            string[] taskTypes = null, 
            string[] grammarParts = null, 
            string[] englishLevels =  null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return Enumerable.Empty<EnglishTaskDto>();

            Expression<Func<EnglishTask, bool>> expression =
                CreateExpression(taskTypes, grammarParts, englishLevels);
            
            IEnumerable<EnglishTask> englishTasks = await GetAllEnglishTasks(expression);
            
            if (!englishTasks.Any())
                return Enumerable.Empty<EnglishTaskDto>();
            
            var englishTaskDtos = _mapper.Map<IEnumerable<EnglishTask>, IEnumerable<EnglishTaskDto>>(englishTasks);
            
            return englishTaskDtos;
        }

        public async Task<EnglishTaskDto> FindRandomEnglishTaskAsync(
            string[] taskTypes = null, 
            string[] grammarParts = null, 
            string[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return null;

            Expression<Func<EnglishTask, bool>> expression =
                CreateExpression(taskTypes, grammarParts, englishLevels);
            
            IEnumerable<EnglishTask> englishTasks = await GetAllEnglishTasks(expression);

            if (!englishTasks.Any())
                return null;
            
            var englishTask = englishTasks.GetRandomValue();
            
            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskDto>(englishTask);
            
            return englishTaskDto;
        }

        public async Task<IEnumerable<EnglishTaskInfoDto>> FindAllInfoEnglishTaskAsync(
            string[] taskTypes = null, 
            string[] grammarParts = null, 
            string[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return Enumerable.Empty<EnglishTaskInfoDto>();

            Expression<Func<EnglishTask, bool>> expression =
                CreateExpression(taskTypes, grammarParts, englishLevels);
            
            IEnumerable<EnglishTask> englishTasks = await GetAllEnglishTasks(expression);
            
            if (!englishTasks.Any())
                return Enumerable.Empty<EnglishTaskInfoDto>();
            
            var englishTaskDtos = _mapper.Map<IEnumerable<EnglishTask>, IEnumerable<EnglishTaskInfoDto>>(englishTasks);
            
            return englishTaskDtos;
        }

        public async Task<EnglishTaskInfoDto> FindRandomInfoEnglishTaskAsync(
            string[] taskTypes = null, 
            string[] grammarParts= null, 
            string[] englishLevels = null)
        {
            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return null;

            Expression<Func<EnglishTask, bool>> expression =
                CreateExpression(taskTypes, grammarParts, englishLevels);
            
            IEnumerable<EnglishTask> englishTasks = await GetAllEnglishTasks(expression);
            
            if (!englishTasks.Any())
                return null;
            
            var englishTask = englishTasks.GetRandomValue();
            
            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskInfoDto>(englishTask);
            
            return englishTaskDto;
        }

        private async Task<IEnumerable<EnglishTask>> GetAllEnglishTasks(Expression<Func<EnglishTask, bool>> filter)
        {
            var englishTasks = await _dbRepository.FindAllAsync(filter);

            return englishTasks;
        }

        private Expression<Func<EnglishTask, bool>> CreateExpression(
            string[] taskTypes = null,
            string[] grammarParts = null,
            string[] englishLevels = null)
        {
            Expression<Func<EnglishTask, bool>> finalExpression = default(Expression<Func<EnglishTask, bool>>);;

            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return finalExpression;

            if (!taskTypes.IsNullOrEmpty())
            {
                var enumTaskTypes = ConverToEnumArray<TaskType>(taskTypes);
                
                var taskTypeExpression = new List<Expression<Func<EnglishTask, bool>>>();

                foreach (var taskType in enumTaskTypes)
                    taskTypeExpression.Add(x => x.TaskType == taskType);                

                finalExpression = taskTypeExpression[0];

                for (var i = 1; i < taskTypeExpression.Count; i++)
                    finalExpression = finalExpression.Or(taskTypeExpression[i]);
            }

            if (!grammarParts.IsNullOrEmpty())
            {
                var enumGrammarParts = ConverToEnumArray<GrammarPart>(grammarParts);
                
                var grammarTypeExpressions = new List<Expression<Func<EnglishTask, bool>>>();

                foreach (var grammarPart in enumGrammarParts)           
                    grammarTypeExpressions.Add(x => x.GrammarPart == grammarPart);

                Expression<Func<EnglishTask, bool>> grammarTypeExpression = grammarTypeExpressions[0];

                for (var i = 1; i < grammarTypeExpressions.Count; i++)
                    grammarTypeExpression = grammarTypeExpression.Or(grammarTypeExpressions[i]);

                finalExpression = finalExpression.And(grammarTypeExpression);
            }

            if (!englishLevels.IsNullOrEmpty())
            {
                var enumEnglishLevels = ConverToEnumArray<EnglishLevel>(englishLevels);
                
                var englishLevelExpressions = new List<Expression<Func<EnglishTask, bool>>>();

                foreach (var englishLevel in enumEnglishLevels)
                    englishLevelExpressions.Add(x => x.EnglishLevel == englishLevel);

                Expression<Func<EnglishTask, bool>> englishLevelExpression = englishLevelExpressions[0];

                for (var i = 1; i < englishLevelExpressions.Count; i++)
                    englishLevelExpression = englishLevelExpression.Or(englishLevelExpressions[i]);

                finalExpression = finalExpression.And(englishLevelExpression);
            }

            return finalExpression;
        }

        private IEnumerable<T> ConverToEnumArray<T>(string[] stringValues)
        {
            foreach (var stringValue in stringValues)
            {
                yield return ConvertToEnum<T>(stringValue);
            }
        }
        
        private T ConvertToEnum<T>(string stringValue)
        {
            return (T) Enum.Parse(typeof(T), stringValue);
        }
    }
}