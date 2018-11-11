using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Enums;
using EnglishLearning.Utilities.Linq.Extensions;
using EnglishLearning.Utilities.Expressions.Helpers;

namespace EnglishLearning.TaskService.Application.InternalServices
{
    internal static class FilterEnglishTaskService
    {
        internal static async Task<IEnumerable<EnglishTask>> GetAllFilteredEnglishTasks(
            IMongoDbRepository<EnglishTask> dbRepository, 
            string[] taskTypes = null, 
            string[] grammarParts = null,
            string[] englishLevels = null)
        {
            Expression<Func<EnglishTask, bool>> expression =
                CreateExpression(taskTypes, grammarParts, englishLevels);
            
            var englishTasks = await dbRepository.FindAllAsync(expression);

            return englishTasks;
        }

        private static Expression<Func<EnglishTask, bool>> CreateExpression(
            string[] taskTypes = null,
            string[] grammarParts = null,
            string[] englishLevels = null)
        {
            Expression<Func<EnglishTask, bool>> finalExpression = null;

            if (taskTypes.IsNullOrEmpty() && grammarParts.IsNullOrEmpty() && englishLevels.IsNullOrEmpty())
                return finalExpression;

            if (!taskTypes.IsNullOrEmpty())
            {
                var enumTaskTypes = EnumConverter.ConverToEnumArray<TaskType>(taskTypes);
                
                var taskTypeExpression = new List<Expression<Func<EnglishTask, bool>>>();

                foreach (var taskType in enumTaskTypes)
                    taskTypeExpression.Add(x => x.TaskType == taskType);                

                finalExpression = taskTypeExpression[0];

                for (var i = 1; i < taskTypeExpression.Count; i++)
                    finalExpression = finalExpression.Or(taskTypeExpression[i]);
            }

            if (!grammarParts.IsNullOrEmpty())
            {
                var enumGrammarParts = EnumConverter.ConverToEnumArray<GrammarPart>(grammarParts);
                
                var grammarTypeExpressions = new List<Expression<Func<EnglishTask, bool>>>();

                foreach (var grammarPart in enumGrammarParts)           
                    grammarTypeExpressions.Add(x => x.GrammarPart == grammarPart);

                Expression<Func<EnglishTask, bool>> grammarTypeExpression = grammarTypeExpressions[0];

                for (var i = 1; i < grammarTypeExpressions.Count; i++)
                    grammarTypeExpression = grammarTypeExpression.Or(grammarTypeExpressions[i]);

                if (finalExpression != null)
                    finalExpression = finalExpression.And(grammarTypeExpression);
                else
                    finalExpression = grammarTypeExpression;
            }

            if (!englishLevels.IsNullOrEmpty())
            {
                var enumEnglishLevels = EnumConverter.ConverToEnumArray<EnglishLevel>(englishLevels);
                
                var englishLevelExpressions = new List<Expression<Func<EnglishTask, bool>>>();

                foreach (var englishLevel in enumEnglishLevels)
                    englishLevelExpressions.Add(x => x.EnglishLevel == englishLevel);

                Expression<Func<EnglishTask, bool>> englishLevelExpression = englishLevelExpressions[0];

                for (var i = 1; i < englishLevelExpressions.Count; i++)
                    englishLevelExpression = englishLevelExpression.Or(englishLevelExpressions[i]);

                if (finalExpression != null)
                    finalExpression = finalExpression.And(englishLevelExpression);
                else
                    finalExpression = englishLevelExpression;
            }

            return finalExpression;
        }
    }
}