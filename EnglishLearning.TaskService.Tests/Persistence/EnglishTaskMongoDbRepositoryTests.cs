using System;
using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.TaskService.Persistence.Repositories;
using EnglishLearning.Utilities.Configurations.MongoConfiguration;
using EnglishLearning.Utilities.Persistence.Mongo.Configuration;
using EnglishLearning.Utilities.Persistence.Mongo.Contexts;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Xunit;

namespace EnglishLearning.TaskService.Tests.Persistence
{
    public class EnglishTaskMongoDbRepositoryTests : IDisposable
    {
        private readonly MongoConfiguration _configuration = new MongoConfiguration
        {
            ServerAddress = "mongodb://localhost:27017",
            DatabaseName = "EnglishTasks_UnitTests"
        };

        private readonly MongoCollectionNamesProvider _mongoCollectionNamesProvider = new MongoCollectionNamesProvider();
        
        private readonly MongoContext _dbContext;
        private readonly EnglishTaskMongoDbRepository _repository;

        public EnglishTaskMongoDbRepositoryTests()
        {
            _mongoCollectionNamesProvider.Add<EnglishTask>("EnglishTasks");
            _dbContext = new MongoContext(Options.Create<MongoConfiguration>(_configuration), _mongoCollectionNamesProvider);
            _repository = new EnglishTaskMongoDbRepository(_dbContext);
        }

        [Fact]
        public async void AddAsync_CorrectParameters_CreateNewTask()
        {
            // Arrange
            var task = new EnglishTask
            {
                TaskType = TaskType.CorrectAlternative,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Advanced,
                Count = 10,
                Example = "example",
                Text = "text",
                Answer = "answer",
            };
            
            // Act
            await _repository.AddAsync(task);
            
            // Assert
            var taskFromDb = await _repository.FindAsync(x => x.Id.Equals(task.Id));
           
            taskFromDb.Should().BeEquivalentTo(task);
        }

        [Fact]
        public async void GetAllAsync_ReturnEmptyCollection()
        {
            // Act
            var tasks = await _repository.GetAllAsync();
            
            // Assert
            tasks.Should().HaveCount(0);
        }

        [Fact]
        public async void GetAllAsync_ReturnAllAddedDocuments()
        {
            // Arrange
            var tasks = new List<EnglishTask>
            {
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
            };

            foreach (var task in tasks)
            {
                await _repository.AddAsync(task);
            }

            // Act
            var tasksFromDb = await _repository.GetAllAsync();
            
            // Assert
            tasksFromDb.Should().BeEquivalentTo(tasks);
        }

        [Fact]
        public async void FindAsync_TaskId_ReturnTaskWithTheSameId()
        {
            // Arrange
            var tasks = new List<EnglishTask>
            {
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
            };

            foreach (var task in tasks)
            {
                await _repository.AddAsync(task);
            }

            // Act
            EnglishTask taskFromDb = await _repository.FindAsync(x => x.Id.Equals(tasks[0].Id));
            
            // Assert
            taskFromDb.Should().BeEquivalentTo(tasks[0]);
        }
        
        [Fact]
        public async void FindAsync_IncorrectId_ReturnNull()
        {
            // Arrange
            var tasks = new List<EnglishTask>
            {
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
            };

            foreach (var task in tasks)
            {
                await _repository.AddAsync(task);
            }

            // Act
            EnglishTask taskFromDb = await _repository.FindAsync(x => x.Id.Equals(new ObjectId().ToString()));
            
            // Assert
            taskFromDb.Should().BeNull();
        }

        [Fact]
        public async void FindAllAsync_IncorrectParameters_EmptySequence()
        {
            // Arrange
            var tasks = new List<EnglishTask>
            {
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
            };

            foreach (var task in tasks)
            {
                await _repository.AddAsync(task);
            }

            // Act
            IEnumerable<EnglishTask> taskFromDb = await _repository.FindAllAsync(x => x.EnglishLevel == EnglishLevel.Advanced && x.TaskType == TaskType.SimpleBrackets);
            
            // Assert
            taskFromDb.Should().BeEmpty();
        }
        
        [Fact]
        public async void FindAsync_TaskTypeGrammarPartEnglishLevel_ReturnTaskThatMatchExpression()
        {
            // Arrange
            var tasks = new List<EnglishTask>
            {
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectOrder, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Intermediate, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.DivisionBySlash, GrammarPart = "PRSimple", EnglishLevel = EnglishLevel.UpperIntermediate, Count = 10, Example = "example", Text = "text", Answer = "answer" },
            };

            foreach (var task in tasks)
            {
                await _repository.AddAsync(task);
            }

            // Act
            EnglishTask taskFromDb = await _repository.FindAsync(x => x.TaskType == TaskType.CorrectOrder && x.GrammarPart == "PRContinuous" && x.EnglishLevel == EnglishLevel.Intermediate);
            
            // Assert
            taskFromDb.Should().BeEquivalentTo(tasks[1]);
        }
        
        [Fact]
        public async void FindAllAsync_TaskTypeGrammarPartEnglishLevel_ReturnTasksThatMatchExpressionFromCollection()
        {
            // Arrange
            var tasks = new List<EnglishTask>
            {
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectOrder, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Intermediate, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectOrder, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Intermediate, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.DivisionBySlash, GrammarPart = "PRSimple", EnglishLevel = EnglishLevel.UpperIntermediate, Count = 10, Example = "example", Text = "text", Answer = "answer" },
            };

            foreach (var task in tasks)
            {
                await _repository.AddAsync(task);
            }

            // Act
            var tasksFromDb = new List<EnglishTask>(
                await _repository.FindAllAsync(x => x.TaskType == TaskType.CorrectOrder && x.GrammarPart == "PRContinuous" && x.EnglishLevel == EnglishLevel.Intermediate));
            
            // Assert
            tasksFromDb.Should().HaveCount(2);
            tasksFromDb.Should().BeEquivalentTo(new List<EnglishTask>
            {
                tasks[1],
                tasks[2],
            });
        }
        
        [Fact]
        public async void DeleteAsync_TaskId_DeleteTaskByIdFromCollection()
        {
            // Arrange
            var tasks = new List<EnglishTask>
            {
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectOrder, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Intermediate, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectOrder, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Intermediate, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.DivisionBySlash, GrammarPart = "PRSimple", EnglishLevel = EnglishLevel.UpperIntermediate, Count = 10, Example = "example", Text = "text", Answer = "answer" },
            };

            foreach (var task in tasks)
            {
                await _repository.AddAsync(task);
            }

            // Act
            bool actionResult = await _repository.DeleteAsync(x => x.Id.Equals(tasks[0].Id));
            
            // Assert
            var tasksFromDb = new List<EnglishTask>(await _repository.GetAllAsync());
            
            actionResult.Should().BeTrue();
            tasksFromDb.Should().HaveCount(3);
            tasksFromDb.Should().BeEquivalentTo(new List<EnglishTask>
            {
                tasks[1],
                tasks[2],
                tasks[3],
            });
        }
        
        [Fact]
        public async void DeleteAsync_TaskTypeGrammarPartEnglishLevel_DeleteTasksThatMatchExpressionFromCollection()
        {
            // Arrange
            var tasks = new List<EnglishTask>
            {
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectOrder, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Intermediate, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectOrder, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Intermediate, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.DivisionBySlash, GrammarPart = "PRSimple", EnglishLevel = EnglishLevel.UpperIntermediate, Count = 10, Example = "example", Text = "text", Answer = "answer" },
            };

            foreach (var task in tasks)
            {
                await _repository.AddAsync(task);
            }

            // Act
            bool actionResult = await _repository.DeleteAsync(x => x.TaskType == TaskType.CorrectOrder && x.GrammarPart == "PRContinuous" && x.EnglishLevel == EnglishLevel.Intermediate);
            
            // Assert
            var tasksFromDb = new List<EnglishTask>(await _repository.GetAllAsync());
            
            actionResult.Should().BeTrue();
            tasksFromDb.Should().HaveCount(2);
            tasksFromDb.Should().BeEquivalentTo(new List<EnglishTask>
            {
                tasks[0],
                tasks[3],
            });
        }
        
        [Fact]
        public async void DeleteAsync_IncorrectParameters_NothingDeletedFromCollection()
        {
            // Arrange
            var tasks = new List<EnglishTask>
            {
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
                new EnglishTask { TaskType = TaskType.CorrectAlternative, GrammarPart = "PRContinuous", EnglishLevel = EnglishLevel.Advanced, Count = 10, Example = "example", Text = "text", Answer = "answer" },
            };

            foreach (var task in tasks)
            {
                await _repository.AddAsync(task);
            }

            // Act
            var actionResult = await _repository.DeleteAsync(x => x.EnglishLevel == EnglishLevel.Advanced && x.TaskType == TaskType.SimpleBrackets);
            
            // Assert
            var tasksFromDb = new List<EnglishTask>(await _repository.GetAllAsync());
            
            actionResult.Should().BeFalse();
            tasksFromDb.Should().HaveCount(3);
            tasksFromDb.Should().BeEquivalentTo(tasks);
        }
        
        [Fact]
        public async void UpdateAsync_UpdatedEntity_UpdatedEntityInCollection()
        {
            // Arrange
            var task = new EnglishTask
            {
                TaskType = TaskType.CorrectAlternative,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Advanced,
                Count = 10,
                Example = "example",
                Text = "text",
                Answer = "answer",
            };          
            await _repository.AddAsync(task);

            // Act
            task.GrammarPart = "PRSimple";
            task.EnglishLevel = EnglishLevel.Intermediate;
            bool actionResult = await _repository.UpdateAsync(task);
            
            // Assert
            var tasksFromDb = new List<EnglishTask>(await _repository.GetAllAsync());

            actionResult.Should().BeTrue();
            tasksFromDb.Should().HaveCount(1);
            tasksFromDb[0].Should().BeEquivalentTo(task);
        }
        
        public void Dispose()
        {   
            var client = new MongoClient(_configuration.ServerAddress);
            client.DropDatabase(_configuration.DatabaseName);
        }
    }
}
