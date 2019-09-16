using System.Collections.Generic;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.Utilities.Linq.Extensions;

namespace EnglishLearning.TaskService.Application.Models.Filtering
{
    public class BaseFilterModel
    {
        public IReadOnlyList<string> GrammarPart { get; set; }
        public IReadOnlyList<TaskType> TaskType { get; set; }
        public IReadOnlyList<EnglishLevel> EnglishLevel { get; set; }

        public static BaseFilterModel CreateFromUserInformation(UserInformationModel userInformation)
        {
            var englishLevels = new[] { userInformation.EnglishLevel };
            var filterModel = new BaseFilterModel()
            {
                GrammarPart = userInformation.FavouriteGrammarParts,
                EnglishLevel = englishLevels,
            };

            return filterModel;
        }
        
        public bool IsEmpty()
        {
            return GrammarPart.IsNullOrEmpty() && TaskType.IsNullOrEmpty() && EnglishLevel.IsNullOrEmpty();
        }
    }
}
