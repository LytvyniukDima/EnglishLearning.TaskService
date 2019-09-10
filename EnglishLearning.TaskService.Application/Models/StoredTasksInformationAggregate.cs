using System.Collections.Generic;
using System.Linq;
using EnglishLearning.TaskService.Common.Models;

namespace EnglishLearning.TaskService.Application.Models
{
    public class StoredTasksInformationAggregate
    {
        public StoredTasksInformationAggregate(
            Dictionary<EnglishLevel, PerEnglishLevelGrammarPartInfoModel> tasksInformationModels,
            IReadOnlyList<string> existedGrammarParts)
        {
            TasksInformationModels = tasksInformationModels;
            ExistedGrammarParts = existedGrammarParts;
        }
        
        public IReadOnlyDictionary<EnglishLevel, PerEnglishLevelGrammarPartInfoModel> TasksInformationModels { get; }
        public IReadOnlyList<string> ExistedGrammarParts { get; }
        
        public BaseFilterModel GetFilterModelForUser(int neededCount, UserInformationModel userInformation)
        {
            var totalCountOfTasks = TasksInformationModels.Values.Sum(x => x.GrammarPartCount.Values.Sum());
            if (totalCountOfTasks < neededCount)
            {
                return GetDefaultFilterModel();
            }
            
            if (!TasksInformationModels.TryGetValue(userInformation.EnglishLevel, out var userLevelInfo))
            {
                return GetDefaultFilterModel();
            }

            var forUserLevelFilter = GetFilterForUserEnglishLevel(neededCount, userInformation, userLevelInfo);
            if (forUserLevelFilter != null)
            {
                return forUserLevelFilter;
            }

            return GetFilterWithTheNearestEnglishLevel(neededCount, userInformation.EnglishLevel);
        }

        public BaseFilterModel GetDefaultFilterModel()
        {
            var filterModel = new BaseFilterModel();
            filterModel.EnglishLevel = EnglishLevelUtils.GetAllEnglishLevels();
            filterModel.GrammarPart = ExistedGrammarParts;

            return filterModel;
        }

        private BaseFilterModel GetFilterForUserEnglishLevel(
            int neededCount,
            UserInformationModel userInformation,
            PerEnglishLevelGrammarPartInfoModel userLevelInfo)
        {
            var totalCountOfUserLevelTasks = userLevelInfo.GrammarPartCount.Values.Sum();
            if (totalCountOfUserLevelTasks < neededCount)
            {
                return null;
            }

            var countWithUsersGrammarParts = 0;
            foreach (var grammarPart in userInformation.FavouriteGrammarParts)
            {
                if (userLevelInfo.GrammarPartCount.TryGetValue(grammarPart, out neededCount))
                {
                    countWithUsersGrammarParts += neededCount;
                }
            }

            if (countWithUsersGrammarParts >= neededCount)
            {
                return BaseFilterModel.CreateFromUserInformation(userInformation);
            }
            
            var filterWithAllGrammarPartsForLevel = new BaseFilterModel();
            filterWithAllGrammarPartsForLevel.EnglishLevel = new[] { userInformation.EnglishLevel };
            filterWithAllGrammarPartsForLevel.GrammarPart = userLevelInfo.GrammarPartCount.Keys.ToList();

            return filterWithAllGrammarPartsForLevel;
        }

        private BaseFilterModel GetFilterWithTheNearestEnglishLevel(int neededCount, EnglishLevel userEnglishLevel)
        {
            var grammarParts = new List<string>();
            var englishLevels = new List<EnglishLevel>();
            var totalCount = 0;
            
            englishLevels.Add(userEnglishLevel);
            grammarParts.AddRange(TasksInformationModels[userEnglishLevel].GrammarPartCount.Keys);
            totalCount += TasksInformationModels[userEnglishLevel].GrammarPartCount.Values.Sum();
            
            var sortedEnglishLevelsByNearerToUser = EnglishLevelUtils.GetSortedEnglishLevelsByNearest(userEnglishLevel);
            foreach (var englishLevel in sortedEnglishLevelsByNearerToUser)
            {
                if (TasksInformationModels.TryGetValue(englishLevel, out var levelInformation))
                {
                    englishLevels.Add(englishLevel);
                    grammarParts.AddRange(TasksInformationModels[englishLevel].GrammarPartCount.Keys);
                    totalCount += TasksInformationModels[englishLevel].GrammarPartCount.Values.Sum();
                    if (totalCount >= neededCount)
                    {
                        break;
                    }
                }
            }
            
            var baseFilterModel = new BaseFilterModel();
            baseFilterModel.EnglishLevel = englishLevels;
            baseFilterModel.GrammarPart = grammarParts;
            return baseFilterModel;
        }
    }
}
