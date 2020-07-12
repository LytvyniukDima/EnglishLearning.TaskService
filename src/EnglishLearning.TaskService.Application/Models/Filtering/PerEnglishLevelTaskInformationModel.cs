using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Models.Filtering
{
    public class PerEnglishLevelTaskInformationModel
    {
        public EnglishLevel EnglishLevel { get; set; }
        public Dictionary<string, int> GrammarPartCount { get; set; }
    }
}
