using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Persistence.Entities
{
    public class PerEnglishLevelTaskInformation
    {
        public EnglishLevel EnglishLevel { get; set; }
        public Dictionary<string, int> GrammarPartCount { get; set; }
    }
}
