using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Services;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace EnglishLearning.TaskService.Tests.Application
{
    public class EnglishTaskServiceTests
    {
        private readonly IEnglishTaskRepository _dbRepository = Substitute.For<IEnglishTaskRepository>();

        private readonly EnglishTaskServiceMapper _mapper = new EnglishTaskServiceMapper();
        
        [Fact]
        public void CreateEnglishTaskAsync_SuccessResult()
        {
            // Arrange
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper);

            // Act
            Func<Task> act = async () =>
            {
                await englishTaskService.CreateEnglishTaskAsync(defaultEnglishTaskCreateDto);
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
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper);
            EnglishTaskCreateDto englishTaskDto = defaultEnglishTaskCreateDto;
            
            // Act
            bool result = await englishTaskService.UpdateEnglishTaskAsync("myId", englishTaskDto);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task GetByIdEnglishTaskAsync_CorrectId_ReturnsCorrectEnglishTaskDto()
        {
            // Arrange
            _dbRepository
                .FindAsync(Arg.Any<Expression<Func<EnglishTask, bool>>>())
                .Returns(Task.FromResult(defaulEnglishTask));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper);       
           
            // Act
            EnglishTaskDto englishTaskDto = await englishTaskService.GetByIdEnglishTaskAsync("Id");
            
            // Arrange
            englishTaskDto.Should().BeEquivalentTo(defaultEnglishTaskDto);
        }
        
        [Fact]
        public async Task GetByIdEnglishTaskAsync_IncorrectId_ReturnsNull()
        {
            // Arrange
            _dbRepository
                .FindAsync(Arg.Any<Expression<Func<EnglishTask, bool>>>())
                .Returns(Task.FromResult(default(EnglishTask)));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper);       
           
            // Act
            EnglishTaskDto englishTaskDto = await englishTaskService.GetByIdEnglishTaskAsync("Id");
            
            // Arrange
            englishTaskDto.Should().BeNull();
        }
        
        [Fact]
        public async Task GetAllEnglishTaskAsync_ReturnsCorrectEnglishTaskDtoSequence()
        {
            // Arrange
            _dbRepository.GetAllAsync().Returns(Task.FromResult(EnglishTaskData));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper);
            
            // Act
            IEnumerable<EnglishTaskDto> englishTaskDtos = await englishTaskService.GetAllEnglishTaskAsync();
            
            // Assert
            englishTaskDtos.Should().BeEquivalentTo(EnglishTaskDtos);
        }

        [Fact]
        public async Task DeleteByIdEnglishTaskAsync_CorrectId_ReturnTrue()
        {
            // Arrange
            _dbRepository
                .DeleteAsync(Arg.Any<Expression<Func<EnglishTask, bool>>>())
                .Returns(Task.FromResult(true));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper);
            
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
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper);
            
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
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper);
            
            // Act
            bool result = await englishTaskService.DeleteAllEnglishTaskAsync();
            
            // Assert
            result.Should().BeTrue();
        }
        
        [Fact]
        public async Task GetByIdEnglishTaskInfoAsync_CorrectId_ReturnsCorrectEnglishTaskInfoDto()
        {
            // Arrange
            _dbRepository
                .FindAsync(Arg.Any<Expression<Func<EnglishTask, bool>>>())
                .Returns(Task.FromResult(defaulEnglishTask));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper);       
           
            // Act
            EnglishTaskInfoDto englishTaskDto = await englishTaskService.GetByIdEnglishTaskInfoAsync("Id");
            
            // Arrange
            englishTaskDto.Should().BeEquivalentTo(defaultEnglishTaskInfoDto);
        }
        
        [Fact]
        public async Task GetByIdEnglishTaskInfoAsync_IncorrectId_ReturnsNull()
        {
            // Arrange
            _dbRepository
                .FindAsync(Arg.Any<Expression<Func<EnglishTask, bool>>>())
                .Returns(Task.FromResult(default(EnglishTask)));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper);       
           
            // Act
            EnglishTaskInfoDto englishTaskDto = await englishTaskService.GetByIdEnglishTaskInfoAsync("Id");
            
            // Arrange
            englishTaskDto.Should().BeNull();
        }
        
        [Fact]
        public async Task GetAllEnglishTaskInfoAsync_ReturnsCorrectEnglishTaskDtoInfoSequence()
        {
            // Arrange
            _dbRepository.GetAllAsync().Returns(Task.FromResult(EnglishTaskData));
            var englishTaskService = new EnglishTaskService(_dbRepository, _mapper);
            
            // Act
            IEnumerable<EnglishTaskInfoDto> englishTaskInfoDtos = await englishTaskService.GetAllEnglishTaskInfoAsync();
            
            // Assert
            englishTaskInfoDtos.Should().BeEquivalentTo(EnglishTaskInfoDtos);
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
                Text = "text",
                Answer = "answer",
            },
            new EnglishTask
            {
                TaskType = TaskType.SimpleBrackets,
                GrammarPart = "PRSimple",
                EnglishLevel = EnglishLevel.PreIntermediate,
                Count = 8,
                Example = "example",
                Text = "text",
                Answer = "answer",
            },
            new EnglishTask
            {
                TaskType = TaskType.WordsFromBox,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Intermediate,
                Count = 9,
                Example = "example",
                Text = "text",
                Answer = "answer",
            },
        };

        private IEnumerable<EnglishTaskDto> EnglishTaskDtos = new List<EnglishTaskDto>()
        {
            new EnglishTaskDto
            {
                TaskType = TaskType.CorrectAlternative,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Advanced,
                Count = 10,
                Example = "example",
                Text = "text",
                Answer = "answer",
            },
            new EnglishTaskDto
            {
                TaskType = TaskType.SimpleBrackets,
                GrammarPart = "PRSimple",
                EnglishLevel = EnglishLevel.PreIntermediate,
                Count = 8,
                Example = "example",
                Text = "text",
                Answer = "answer",
            },
            new EnglishTaskDto
            {
                TaskType = TaskType.WordsFromBox,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Intermediate,
                Count = 9,
                Example = "example",
                Text = "text",
                Answer = "answer",
            },
        };
        
        private IEnumerable<EnglishTaskInfoDto> EnglishTaskInfoDtos = new List<EnglishTaskInfoDto>()
        {
            new EnglishTaskInfoDto
            {
                TaskType = TaskType.CorrectAlternative,
                GrammarPart = "PRContinuous",
                EnglishLevel = EnglishLevel.Advanced,
            },
            new EnglishTaskInfoDto
            {
                TaskType = TaskType.SimpleBrackets,
                GrammarPart = "PRSimple",
                EnglishLevel = EnglishLevel.Elementary,
            },
            new EnglishTaskInfoDto
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
            Text = "text",
            Answer = "answer",
        };

        private EnglishTaskDto defaultEnglishTaskDto = new EnglishTaskDto
        {
            TaskType = TaskType.CorrectAlternative,
            GrammarPart = "PRContinuous",
            EnglishLevel = EnglishLevel.Advanced,
            Count = 10,
            Example = "example",
            Text = "text",
            Answer = "answer",
        };
        
        private EnglishTaskCreateDto defaultEnglishTaskCreateDto = new EnglishTaskCreateDto
        {
            TaskType = TaskType.CorrectAlternative,
            GrammarPart = "PRContinuous",
            EnglishLevel = EnglishLevel.Advanced,
            Count = 9,
            Example = "example",
            Answer = "answer",
        };

        private EnglishTaskInfoDto defaultEnglishTaskInfoDto = new EnglishTaskInfoDto
        {
            TaskType = TaskType.CorrectAlternative,
            GrammarPart = "PRContinuous",
            EnglishLevel = EnglishLevel.Advanced,
        };
    }
}
