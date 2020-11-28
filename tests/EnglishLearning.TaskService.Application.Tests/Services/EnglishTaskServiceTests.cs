using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Services;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using FluentAssertions;
using MongoDB.Bson;
using NSubstitute;
using Xunit;

namespace EnglishLearning.TaskService.Application.Tests.Services
{
    public class EnglishTaskServiceTests
    {
        private readonly IEnglishTaskRepository _dbRepository = Substitute.For<IEnglishTaskRepository>();
        private readonly IUserFilterService _userFilterService = Substitute.For<IUserFilterService>();
        
        private readonly ApplicationMapper _mapper = new ApplicationMapper();
        
        [Fact]
        public void CreateEnglishTaskAsync_SuccessResult()
        {
            // Arrange
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper, _userFilterService);

            // Act
            Func<Task> act = async () =>
            {
                await englishTaskService.CreateEnglishTaskAsync(defaultEnglishTaskCreateModel);
            };
            
            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public async Task UpdateEnglishTaskAsync_ReturnsTrue()
        {
            // Arrange
            _dbRepository
                .UpdateAsync(Arg.Any<EnglishTask>())
                .Returns(Task.FromResult(true));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper, _userFilterService);
            EnglishTaskCreateModel englishTaskModel = defaultEnglishTaskCreateModel;
            
            // Act
            bool result = await englishTaskService.UpdateEnglishTaskAsync("myId", englishTaskModel);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task GetByIdEnglishTaskAsync_CorrectId_ReturnsCorrectEnglishTaskModel()
        {
            // Arrange
            _dbRepository
                .FindAsync(Arg.Any<Expression<Func<EnglishTask, bool>>>())
                .Returns(Task.FromResult(defaulEnglishTask));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper, _userFilterService);       
           
            // Act
            EnglishTaskModel englishTaskModel = await englishTaskService.GetByIdEnglishTaskAsync("Id");
            
            // Arrange
            englishTaskModel.Should().BeEquivalentTo(defaultEnglishTaskModel);
        }
        
        [Fact]
        public async Task GetByIdEnglishTaskAsync_IncorrectId_ReturnsNull()
        {
            // Arrange
            _dbRepository
                .FindAsync(Arg.Any<Expression<Func<EnglishTask, bool>>>())
                .Returns(Task.FromResult(default(EnglishTask)));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper, _userFilterService);       
           
            // Act
            EnglishTaskModel englishTaskModel = await englishTaskService.GetByIdEnglishTaskAsync("Id");
            
            // Arrange
            englishTaskModel.Should().BeNull();
        }
        
        [Fact]
        public async Task GetAllEnglishTaskAsync_ReturnsCorrectEnglishTaskModelSequence()
        {
            // Arrange
            _dbRepository.GetAllAsync().Returns(Task.FromResult(EnglishTaskData));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper, _userFilterService);
            
            // Act
            IEnumerable<EnglishTaskModel> englishTaskModels = await englishTaskService.GetAllEnglishTaskAsync();
            
            // Assert
            englishTaskModels.Should().BeEquivalentTo(EnglishTaskModels);
        }

        [Fact]
        public async Task DeleteByIdEnglishTaskAsync_CorrectId_ReturnTrue()
        {
            // Arrange
            _dbRepository
                .DeleteAsync(Arg.Any<Expression<Func<EnglishTask, bool>>>())
                .Returns(Task.FromResult(true));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper, _userFilterService);
            
            // Act
            bool result = await englishTaskService.DeleteByIdEnglishTaskAsync("myId");
            
            // Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async Task DeleteByIdEnglishTaskAsync_IncorrectId_ReturnFalse()
        {
            // Arrange
            _dbRepository
                .DeleteAsync(Arg.Any<Expression<Func<EnglishTask, bool>>>())
                .Returns(Task.FromResult(false));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper, _userFilterService);
            
            // Act
            bool result = await englishTaskService.DeleteByIdEnglishTaskAsync("myId");
            
            // Assert
            result.Should().BeFalse();
        }
        
        [Fact]
        public async Task DeleteAllEnglishTaskAsync_ReturnTrue()
        {
            // Arrange
            _dbRepository
                .DeleteAllAsync()
                .Returns(Task.FromResult(true));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper, _userFilterService);
            
            // Act
            bool result = await englishTaskService.DeleteAllEnglishTaskAsync();
            
            // Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async Task GetByIdEnglishTaskInfoAsync_CorrectId_ReturnsCorrectEnglishTaskInfoModel()
        {
            // Arrange
            _dbRepository
                .FindAsync(Arg.Any<Expression<Func<EnglishTask, bool>>>())
                .Returns(Task.FromResult(defaulEnglishTask));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper, _userFilterService);       
           
            // Act
            EnglishTaskInfoModel englishTaskModel = await englishTaskService.GetByIdEnglishTaskInfoAsync("Id");
            
            // Arrange
            englishTaskModel.Should().BeEquivalentTo(defaultEnglishTaskInfoModel);
        }
        
        [Fact]
        public async Task GetByIdEnglishTaskInfoAsync_IncorrectId_ReturnsNull()
        {
            // Arrange
            _dbRepository
                .FindAsync(Arg.Any<Expression<Func<EnglishTask, bool>>>())
                .Returns(Task.FromResult(default(EnglishTask)));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper, _userFilterService);       
           
            // Act
            EnglishTaskInfoModel englishTaskModel = await englishTaskService.GetByIdEnglishTaskInfoAsync("Id");
            
            // Arrange
            englishTaskModel.Should().BeNull();
        }
        
        [Fact]
        public async Task GetAllEnglishTaskInfoAsync_ReturnsCorrectEnglishTaskModelInfoSequence()
        {
            // Arrange
            _dbRepository.GetAllInfoAsync().Returns(Task.FromResult(EnglishTaskInfoData));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper, _userFilterService);
            
            // Act
            IEnumerable<EnglishTaskInfoModel> englishTaskInfoModels = await englishTaskService.GetAllEnglishTaskInfoAsync();
            
            // Assert
            englishTaskInfoModels.Should().BeEquivalentTo(EnglishTaskInfoModels);
        }
        
        private IReadOnlyList<EnglishTask> EnglishTaskData = new List<EnglishTask>()
        {
            new EnglishTask
            {
                TaskType = TaskType.CorrectAlternative,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Advanced,
                Count = 10,
                Example = "example",
                Content = BsonString.Create("text"),
            },
            new EnglishTask
            {
                TaskType = TaskType.SimpleBrackets,
                GrammarPart = "PRSimple",
                EnglishLevel = EnglishLevel.PreIntermediate,
                Count = 8,
                Example = "example",
                Content = BsonString.Create("text"),
            },
            new EnglishTask
            {
                TaskType = TaskType.WordsFromBox,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Intermediate,
                Count = 9,
                Example = "example",
                Content = BsonString.Create("text"),
            },
        };

        private IReadOnlyList<EnglishTaskInfo> EnglishTaskInfoData = new List<EnglishTaskInfo>()
        {
            new EnglishTaskInfo
            {
                TaskType = TaskType.CorrectAlternative,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Advanced,
            },
            new EnglishTaskInfo
            {
                TaskType = TaskType.SimpleBrackets,
                GrammarPart = "PRSimple",
                EnglishLevel = EnglishLevel.Elementary,
            },
            new EnglishTaskInfo
            {
                TaskType = TaskType.WordsFromBox,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Intermediate,
            },
        };
        
        private IEnumerable<EnglishTaskModel> EnglishTaskModels = new List<EnglishTaskModel>()
        {
            new EnglishTaskModel
            {
                TaskType = TaskType.CorrectAlternative,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Advanced,
                Count = 10,
                Example = "example",
                Content = "\"text\"",
            },
            new EnglishTaskModel
            {
                TaskType = TaskType.SimpleBrackets,
                GrammarPart = "PRSimple",
                EnglishLevel = EnglishLevel.PreIntermediate,
                Count = 8,
                Example = "example",
                Content = "\"text\"",
            },
            new EnglishTaskModel
            {
                TaskType = TaskType.WordsFromBox,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Intermediate,
                Count = 9,
                Example = "example",
                Content = "\"text\"",
            },
        };
        
        private IEnumerable<EnglishTaskInfoModel> EnglishTaskInfoModels = new List<EnglishTaskInfoModel>()
        {
            new EnglishTaskInfoModel
            {
                TaskType = TaskType.CorrectAlternative,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Advanced,
            },
            new EnglishTaskInfoModel
            {
                TaskType = TaskType.SimpleBrackets,
                GrammarPart = "PRSimple",
                EnglishLevel = EnglishLevel.Elementary,
            },
            new EnglishTaskInfoModel
            {
                TaskType = TaskType.WordsFromBox,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Intermediate,
            },
        };

        private EnglishTask defaulEnglishTask = new EnglishTask
        {
            TaskType = TaskType.CorrectAlternative,
            GrammarPart = "PRContinuous",
            EnglishLevel = EnglishLevel.Advanced,
            Count = 10,
            Example = "example",
            Content = BsonString.Create("text"),
        };

        private EnglishTaskModel defaultEnglishTaskModel = new EnglishTaskModel
        {
            TaskType = TaskType.CorrectAlternative,
            GrammarPart = "PRContinuous",
            EnglishLevel = EnglishLevel.Advanced,
            Count = 10,
            Example = "example",
            Content = "\"text\"",
        };
        
        private EnglishTaskCreateModel defaultEnglishTaskCreateModel = new EnglishTaskCreateModel
        {
            TaskType = TaskType.CorrectAlternative,
            GrammarPart = "PRContinuous",
            EnglishLevel = EnglishLevel.Advanced,
            Count = 9,
            Example = "example",
            Answer = "answer",
        };

        private EnglishTaskInfoModel defaultEnglishTaskInfoModel = new EnglishTaskInfoModel
        {
            TaskType = TaskType.CorrectAlternative,
            GrammarPart = "PRContinuous",
            EnglishLevel = EnglishLevel.Advanced,
        };
    }
}
