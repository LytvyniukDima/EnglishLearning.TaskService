using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Models.Filtering;

namespace EnglishLearning.TaskService.Application.Services
{
    public class StoredTaskInformationAggregateService : IStoredTaskInformationAggregateService
    {
        private readonly IEnglishTaskFilterOptionsService _filterOptionsService;

        public StoredTaskInformationAggregateService(IEnglishTaskFilterOptionsService filterOptionsService)
        {
            _filterOptionsService = filterOptionsService;
        }
        
        public async Task<StoredTasksInformationAggregate> GetStoredTaskInformationAggregate()
        {
            var taskInformationModels = await _filterOptionsService.GetTaskInformationModels();
            var fullFilter = await _filterOptionsService.GetFullFilter();

            var taskInformationModelsDictionary = taskInformationModels.ToDictionary(x => x.EnglishLevel);
            var grammarParts = fullFilter.GrammarPartFilterOptions.Keys.ToList();
            var aggregate = new StoredTasksInformationAggregate(taskInformationModelsDictionary, grammarParts);

            return aggregate;
        }
    }
}