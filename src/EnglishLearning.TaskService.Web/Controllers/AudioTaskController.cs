using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("/api/tasks/audio")]
    public class AudioTaskController : Controller
    {
        private readonly IParsedSentToAudioService _parsedSentToAudioService;

        public AudioTaskController(IParsedSentToAudioService parsedSentToAudioService)
        {
            _parsedSentToAudioService = parsedSentToAudioService;
        }
        
        [HttpGet("sent/{id}")]
        public async Task<IActionResult> GetForTaskItem(string id)
        {
            var audioStream = await _parsedSentToAudioService.GetParsedSentAudioAsync(id);
            
            return new FileStreamResult(audioStream, "audio/wav")
            {
                FileDownloadName = "audio.wav",
            };
        }
    }
}