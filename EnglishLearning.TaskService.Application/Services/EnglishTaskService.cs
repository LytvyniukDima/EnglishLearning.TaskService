using System;
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
        private readonly IEnglishTaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly IUserInformationService _userInformationService;
        
        public EnglishTaskService(
            IEnglishTaskRepository taskRepository, 
            EnglishTaskServiceMapper englishTaskServiceMappermapper,
            IUserInformationService userInformationService)
        {
            _taskRepository = taskRepository;
            _mapper = englishTaskServiceMappermapper.Mapper;
            _userInformationService = userInformationService;
        }

        public async Task CreateEnglishTaskAsync(EnglishTaskCreateDto englishTaskCreateDto)
        {
            var englishTask = _mapper.Map<EnglishTaskCreateDto, EnglishTask>(englishTaskCreateDto);

            await _taskRepository.AddAsync(englishTask);
        }

        public async Task<bool> UpdateEnglishTaskAsync(string id, EnglishTaskCreateDto englishTaskDto)
        {
            var englishTask = _mapper.Map<EnglishTaskCreateDto, EnglishTask>(englishTaskDto);
            englishTask.Id = id;
            
            return await _taskRepository.UpdateAsync(englishTask);
        }

        public async Task<EnglishTaskDto> GetByIdEnglishTaskAsync(string id)
        {
            var englishTask = await GetEnglishTask(id);
            
            // TODO: Throw NotFoundException
            if (englishTask == null)
            {
                return null;
            }

            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskDto>(englishTask);

            return englishTaskDto;
        }

        public async Task<IReadOnlyList<EnglishTaskDto>> GetAllEnglishTaskAsync()
        {
            var englishTasks = await _taskRepository.GetAllAsync();
            var englishTaskDtos = _mapper.Map<IReadOnlyList<EnglishTaskDto>>(englishTasks);
            
            return englishTaskDtos;
        }

        public async Task<bool> DeleteByIdEnglishTaskAsync(string id)
        {
            return await _taskRepository.DeleteAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteAllEnglishTaskAsync()
        {
            return await _taskRepository.DeleteAllAsync();
        }

        public async Task<EnglishTaskInfoDto> GetByIdEnglishTaskInfoAsync(string id)
        {
            var englishTask = await GetEnglishTask(id);
            
            // TODO: Throw NotFoundException
            if (englishTask == null)
            {
                return null;
            }

            var englishTaskDto = _mapper.Map<EnglishTask, EnglishTaskInfoDto>(englishTask);

            return englishTaskDto;
        }

        public async Task<IReadOnlyList<EnglishTaskInfoDto>> GetAllEnglishTaskInfoAsync()
        {
            var englishTasks = await _taskRepository.GetAllInfoAsync();
            var englishTasksDto = _mapper.Map<IReadOnlyList<EnglishTaskInfoDto>>(englishTasks);

            return englishTasksDto;
        }

        public async Task<IReadOnlyList<EnglishTaskInfoDto>> GetAllEnglishTaskInfoWithUserPreferencesAsync()
        {
            var userInformation = await _userInformationService.GetUserInformationForCurrentUser();
            if (userInformation == null)
            {
                return await GetAllEnglishTaskInfoAsync();
            }

            var filterModel = BaseFilterModel.CreateFromUserInformation(userInformation);
            return await FindAllInfoEnglishTaskAsync(filterModel);
        }

        public async Task<IReadOnlyList<EnglishTaskDto>> FindAllEnglishTaskAsync(BaseFilterModel filterModel)
        {
            if (filterModel == null || filterModel.IsEmpty())
            {
                return Array.Empty<EnglishTaskDto>();
            }

            var persistenceFilter = _mapper.Map<BaseFilter>(filterModel);
            var englishTasks = await _taskRepository.FindAllByFilters(persistenceFilter);
            var englishTaskDtos = _mapper.Map<IReadOnlyList<EnglishTaskDto>>(englishTasks);
            
            return englishTaskDtos;
        }

        public async Task<IReadOnlyList<EnglishTaskInfoDto>> FindAllInfoEnglishTaskAsync(BaseFilterModel filterModel)
        {
            if (filterModel == null || filterModel.IsEmpty())
            {
                return Array.Empty<EnglishTaskInfoDto>();
            }

            var persistenceFilter = _mapper.Map<BaseFilter>(filterModel);
            var englishTasks = await _taskRepository.FindAllInfoByFilters(persistenceFilter);
            
            var englishTaskDtos = _mapper.Map<IReadOnlyList<EnglishTaskInfoDto>>(englishTasks);
            
            return englishTaskDtos;
        }

        private async Task<EnglishTask> GetEnglishTask(string id)
        {
            return await _taskRepository.FindAsync(x => x.Id == id);
        }
    }
}
