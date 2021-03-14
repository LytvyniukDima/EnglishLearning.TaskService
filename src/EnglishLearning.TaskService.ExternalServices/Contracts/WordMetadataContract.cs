using EnglishLearning.Dictionary.Common.Models;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.ExternalServices.Contracts
{
    public class WordMetadataContract
    {
        public string Word { get; set; }

        public EnglishLevel Level { get; set; }
    }
}