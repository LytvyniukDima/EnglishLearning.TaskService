﻿using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Models
{
    public class PerEnglishLevelGrammarPartInfoModel
    {
        public EnglishLevel EnglishLevel { get; set; }
        public Dictionary<string, int> GrammarPartCount { get; set; }
    }
}
