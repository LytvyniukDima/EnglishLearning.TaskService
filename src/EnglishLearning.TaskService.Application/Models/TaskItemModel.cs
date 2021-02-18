﻿using EnglishLearning.TaskService.Common.Models;
using MongoDB.Bson;

namespace EnglishLearning.TaskService.Application.Models
{
    public class TaskItemModel
    {
        public string Id { get; set; }
        
        public string TaskGenerationId { get; set; }
        
        public string GenerationName { get; set; }
        
        public string SourceSentId { get; set; }
        
        public string GrammarPart { get; set; }
        
        public TaskType TaskType { get; set; }

        public string SentType { get; set; }
        
        public string Content { get; set; }
    }
}