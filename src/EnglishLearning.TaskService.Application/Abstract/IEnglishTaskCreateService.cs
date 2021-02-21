using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IEnglishTaskCreateService
    {
        Task CreateFromItemsAsync(EnglishTaskFromItemsCreateModel createModel);

        Task CreateFromRandomItemsAsync(EnglishTaskFromRandomItemsCreateModel createModel);
    }
}