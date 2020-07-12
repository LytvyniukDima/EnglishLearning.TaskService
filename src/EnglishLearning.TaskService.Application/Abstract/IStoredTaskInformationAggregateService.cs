using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models.Filtering;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IStoredTaskInformationAggregateService
    {
        Task<StoredTasksInformationAggregate> GetStoredTaskInformationAggregate();
    }
}
