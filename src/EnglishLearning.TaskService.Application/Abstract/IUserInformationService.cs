using System;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IUserInformationService
    {
        Task AddUserInfo(UserInformationModel userInformation);
        Task<UserInformationModel> GetUserInformation(Guid id);
        Task<UserInformationModel> GetUserInformationForCurrentUser();
    }
}
