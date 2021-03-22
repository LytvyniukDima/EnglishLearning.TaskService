using System.Threading.Tasks;
using AutoMapper;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Web.Infrastructure;
using EnglishLearning.TaskService.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearning.TaskService.Web.Controllers
{
    [Route("/api/tasks/audio")]
    public class AudioTaskController : Controller
    {
        private readonly IParsedSentToAudioService _parsedSentToAudioService;

        private readonly IAudioTaskService _audioTaskService;
        
        private readonly IMapper _mapper;

        public AudioTaskController(
            IParsedSentToAudioService parsedSentToAudioService,
            IAudioTaskService audioTaskService,
            WebMapper webMapper)
        {
            _parsedSentToAudioService = parsedSentToAudioService;
            _audioTaskService = audioTaskService;
            _mapper = webMapper.Mapper;
        }

        [HttpPost]
        public async Task<IActionResult> GetAudioTask([FromBody] AudioTaskQueryViewModel queryModel)
        {
            var applicationQuery = _mapper.Map<AudioTaskQueryModel>(queryModel);
            var task = await _audioTaskService.CreateAudioTaskAsync(applicationQuery);

            var webModel = _mapper.Map<AudioTaskViewModel>(task);

            return Ok(webModel);
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