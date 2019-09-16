using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Models.Filtering;

namespace EnglishLearning.TaskService.Application.Services
{
    public class UserFilterService : IUserFilterService
    {
        private readonly IUserInformationService _userInformationService;
        private readonly IStoredTaskInformationAggregateService _taskInformationAggregateService;

        public UserFilterService(
            IUserInformationService userInformationService,
            IStoredTaskInformationAggregateService taskInformationAggregateService)
        {
            _userInformationService = userInformationService;
            _taskInformationAggregateService = taskInformationAggregateService;
        }
        
        public async Task<BaseFilterModel> GetFilterModelForCurrentUser(int requiredTaskCount)
        {
            var taskInformationAggregate = await _taskInformationAggregateService.GetStoredTaskInformationAggregate();
            var userInformation = await _userInformationService.GetUserInformationForCurrentUser();
            if (userInformation == null)
            {
                return taskInformationAggregate.GetDefaultFilterModel();
            }

            return taskInformationAggregate.GetFilterModelForUser(requiredTaskCount, userInformation);
        }
    }
}