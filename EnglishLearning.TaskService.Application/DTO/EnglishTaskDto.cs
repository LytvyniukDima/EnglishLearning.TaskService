﻿namespace EnglishLearning.TaskService.Application.DTO
{
    public class EnglishTaskDto
    {
        public string Id { get; set; }
        
        public string TaskType { get; set; }
        public string GrammarPart { get; set; }
        public string EnglishLevel { get; set; }
        
        public int Count { get; set; }
        public string Example { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
    }
}