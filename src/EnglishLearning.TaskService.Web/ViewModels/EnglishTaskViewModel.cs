﻿using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Web.ViewModels
{
    public class EnglishTaskViewModel
    {
        public string Id { get; set; }
        
        public string GrammarPart { get; set; }
        
        public TaskType TaskType { get; set; }
        
        public EnglishLevel EnglishLevel { get; set; }
        
        public int Count { get; set; }
        
        public string Example { get; set; }
        
        public string Content { get; set; }
    }
}