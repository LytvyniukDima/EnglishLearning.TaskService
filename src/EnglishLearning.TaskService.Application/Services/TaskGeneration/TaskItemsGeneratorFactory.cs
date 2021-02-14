using System;
using EnglishLearning.TaskService.Application.Abstract.TaskGeneration;
using EnglishLearning.TaskService.Common.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.TaskService.Application.Services.TaskGeneration
{
    public class TaskItemsGeneratorFactory : ITaskItemsGeneratorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public TaskItemsGeneratorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public ITaskItemsGeneratorService GetTaskItemsGenerator(string grammarPart, TaskType taskType)
        {
            return (grammarPart, taskType) switch
            {
                (GrammarParts.PresentSimple, TaskType.SimpleBrackets) => _serviceProvider.GetRequiredService<PresentSimpleBracketsTaskItemsGenerator>(),
                _ => throw new ArgumentException($"Generator for grammarPart {grammarPart} and taskType {taskType} not supported")
            };
        }
    }
}