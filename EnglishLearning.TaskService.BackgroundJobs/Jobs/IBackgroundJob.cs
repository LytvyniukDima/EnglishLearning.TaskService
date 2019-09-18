using System.Threading.Tasks;

namespace EnglishLearning.TaskService.BackgroundJobs.Jobs
{
    public interface IBackgroundJob
    {
        Task Execute();
    }
}
