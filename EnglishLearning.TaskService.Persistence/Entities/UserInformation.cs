using System;
using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.Utilities.Persistence.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace EnglishLearning.TaskService.Persistence.Entities
{
    public class UserInformation : IEntity<Guid>
    {
        [BsonId]
        [BsonElement("_id")]
        public Guid Id { get; set; }
        
        public EnglishLevel EnglishLevel { get; set; }
        public List<string> FavouriteGrammarParts { get; set; }
    }
}
