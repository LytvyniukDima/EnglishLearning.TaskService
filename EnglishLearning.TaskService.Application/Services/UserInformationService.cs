using System;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.DTO;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;

namespace EnglishLearning.TaskService.Application.Services
{
    public class UserInformationService : IUserInformationService
    {
        private readonly IUserInformationRepository _repository;
        private readonly IMapper _mapper;
        
        public UserInformationService(IUserInformationRepository repository, EnglishTaskServiceMapper serviceMapper)
        {
            _repository = repository;
            _mapper = serviceMapper.Mapper;
        }
        
        public async Task AddUserInfo(UserInformationDto userInformation)
        {
            var entity = _mapper.Map<UserInformation>(userInformation);
            await _repository.AddAsync(entity);
        }

        public async Task<UserInformationDto> GetUserInformation(Guid id)
        {
            UserInformation entity = await _repository.FindAsync(x => x.Id == id);
            return _mapper.Map<UserInformationDto>(entity);
        }
    }
}
