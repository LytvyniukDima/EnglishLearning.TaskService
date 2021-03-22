using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Models;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IAudioTaskService
    {
        Task<AudioTaskModel> CreateAudioTaskAsync(AudioTaskQueryModel queryModel);
    }
}