using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Application.Services
{
    public class EnglishTaskService : IEnglishTaskService
    {
        private readonly IMongoDbRepository<EnglishTask> _dbRepository;
        private readonly IMapper _mapper;
        
        public EnglishTaskService(IMongoDbRepository<EnglishTask> dbRepository, EnglishTaskServiceMapper englishTaskServiceMappermapper)
        {
            _dbRepository = dbRepository;
            _mapper = englishTaskServiceMappermapper.Mapper;
        }


        public async Task CreateEnglishTaskAsync(EnglishTaskCreateDto englishTaskCreateDto)
        {
            var englishTask = _mapper.Map<EnglishTaskCreateDto, EnglishTask>(englishTaskCreateDto);

            await _dbRepository.AddAsync(englishTask);
        }

        public async Task<bool> UpdateEnglishTaskAsync(EnglishTaskDto englishTaskDto)
        {
            var englishTask = _mapper.Map<EnglishTaskDto, EnglishTask>(englishTaskDto);

            return await _dbRepository.UpdateAsync(englishTask.Id, englishTask);
        }

        public async Task<EnglishTaskDto> GetByIdEnglishTaskAsync(string id)
        {
            var englishTask = await GetEnglishTask(id);

            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskDto>(englishTask);

            return englishTaskDto;
        }

        public async Task<IEnumerable<EnglishTaskDto>> GetAllEnglishTaskAsync()
        {
            var englishTasks = await GetAllEnglishTasks();

            var englishTasksDto = 
                _mapper.Map<IEnumerable<EnglishTask>, IEnumerable<EnglishTaskDto>>(englishTasks);

            return englishTasksDto;
        }

        public async Task<bool> DeleteByIdEnglishTaskAsync(string id)
        {
            return await _dbRepository.DeleteAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteAllEnglishTaskAsync()
        {
            return await _dbRepository.DeleteAllAsync();
        }

        public async Task<EnglishTaskInfoDto> GetByIdEnglishTaskInfoAsync(string id)
        {
            var englishTask = await GetEnglishTask(id);

            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskInfoDto>(englishTask);

            return englishTaskDto;
        }

        public async Task<IEnumerable<EnglishTaskInfoDto>> GetAllEnglishTaskInfoAsync()
        {
            var englishTasks = await GetAllEnglishTasks();

            var englishTasksDto = 
                _mapper.Map<IEnumerable<EnglishTask>, IEnumerable<EnglishTaskInfoDto>>(englishTasks);

            return englishTasksDto;
        }

        private async Task<EnglishTask> GetEnglishTask(string id)
        {
            return await _dbRepository.FindAsync(x => x.Id == id);
        }

        private async Task<IEnumerable<EnglishTask>> GetAllEnglishTasks()
        {
            return await _dbRepository.GetAllAsync();
        }
    }
}