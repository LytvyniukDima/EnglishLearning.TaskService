using System;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.DTO;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IUserInformationService
    {
        Task AddUserInfo(UserInformationDto userInformation);
        Task<UserInformationDto> GetUserInformation(Guid id);
    }
}
