using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Abstract;
using Serilog;

namespace EnglishLearning.TaskService.BackgroundJobs.Jobs
{
    public class FillFilterCacheBackgroundJob : IBackgroundJob
    {
        private readonly IEnglishTaskFilterOptionsService _filterOptionsService;

        public FillFilterCacheBackgroundJob(IEnglishTaskFilterOptionsService filterOptionsService)
        {
            _filterOptionsService = filterOptionsService;
        }
        
        public async Task Execute()
        {
            Log.Information("Filling filter cache");
            await _filterOptionsService.FillFiltersCache();
        }
    }
}
