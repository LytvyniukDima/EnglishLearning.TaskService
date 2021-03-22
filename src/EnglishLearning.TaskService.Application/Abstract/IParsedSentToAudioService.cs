using System.IO;
using System.Threading.Tasks;

namespace EnglishLearning.TaskService.Application.Abstract
{
    public interface IParsedSentToAudioService
    {
        Task<Stream> GetParsedSentAudioAsync(string sentId);
    }
}