using System;
using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Infrastructure;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.TaskService.Persistence.Entities;
using EnglishLearning.Utilities.Identity.Abstractions;

namespace EnglishLearning.TaskService.Application.Services
{
    public class UserInformationService : IUserInformationService
    {
        private readonly IUserInformationRepository _repository;
        private readonly IMapper _mapper;
        private readonly IJwtInfoProvider _jwtInfoProvider;
        
        public UserInformationService(
            IUserInformationRepository repository, 
            EnglishTaskServiceMapper serviceMapper,
            IJwtInfoProvider jwtInfoProvider)
        {
            _repository = repository;
            _mapper = serviceMapper.Mapper;
            _jwtInfoProvider = jwtInfoProvider;
        }
        
        public async Task AddUserInfo(UserInformationModel userInformation)
        {
            var entity = _mapper.Map<UserInformation>(userInformation);
            await _repository.AddAsync(entity);
        }

        public async Task<UserInformationModel> GetUserInformation(Guid id)
        {
            UserInformation entity = await _repository.FindAsync(x => x.Id == id);
            return _mapper.Map<UserInformationModel>(entity);
        }
        
        public async Task<UserInformationModel> GetUserInformationForCurrentUser()
        {
            if (!_jwtInfoProvider.IsAuthorized)
            {
                return null;
            }

            var userId = _jwtInfoProvider.UserId;
            return await GetUserInformation(userId);
        }
    }
}
