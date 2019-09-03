using System;
using System.Collections.Generic;

namespace EnglishLearning.TaskService.Persistence.Entities
{
    public class UserInformation
    {
        public Guid UserId { get; set; }
        public string EnglishLevel { get; set; }
        public List<string> FavouriteGrammarParts { get; set; }
    }
}
