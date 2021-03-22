using System.IO;
using System.Threading.Tasks;
using EnglishLearning.TaskService.Application.Abstract;
using EnglishLearning.TaskService.Persistence.Abstract;
using EnglishLearning.Utilities.Speech.Contracts;

namespace EnglishLearning.TaskService.Application.Services
{
    internal class ParsedSentToAudioService : IParsedSentToAudioService
    {
        private readonly ITextToSpeechService _textToSpeechService;

        private readonly IParsedSentRepository _parsedSentRepository;
        
        public ParsedSentToAudioService(
            ITextToSpeechService textToSpeechService,
            IParsedSentRepository parsedSentRepository)
        {
            _textToSpeechService = textToSpeechService;
            _parsedSentRepository = parsedSentRepository;
        }
        
        public async Task<Stream> GetParsedSentAudioAsync(string sentId)
        {
            var parsedSent = await _parsedSentRepository.FindAsync(x => x.Id == sentId);
            var audioStream = await _textToSpeechService.SpeakTextAsync(parsedSent.Sent);

            return audioStream;
        }
    }
}