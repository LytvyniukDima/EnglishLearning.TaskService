using System;
using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Models
{
    public class UserInformationModel
    {
        public Guid UserId { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
        public List<string> FavouriteGrammarParts { get; set; }
    }
}
