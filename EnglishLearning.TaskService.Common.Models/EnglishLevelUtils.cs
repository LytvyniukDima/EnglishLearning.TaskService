using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishLearning.TaskService.Common.Models
{
    public static class EnglishLevelUtils
    {
        public static IReadOnlyList<EnglishLevel> GetAllEnglishLevels()
        {
            return Enum
                .GetValues(typeof(EnglishLevel))
                .Cast<EnglishLevel>()
                .ToList();
        }

        public static EnglishLevel[] GetSortedEnglishLevelsByNearest(EnglishLevel englishLevel)
        {
            var englishLevels = Enum
                .GetValues(typeof(EnglishLevel))
                .Cast<int>()
                .ToList();
            
            englishLevels.Sort();
            var englishLevelsCount = englishLevels.Count;
            
            var sortedEnglishLevels = new EnglishLevel[englishLevelsCount - 1];
            var leftLevel = (int)englishLevel - 1;
            var rightLevel = (int)englishLevel + 1;
            int index = 0;
            while (leftLevel >= 0 || rightLevel < englishLevelsCount)
            {
                if (leftLevel >= 0)
                {
                    sortedEnglishLevels[index] = (EnglishLevel)leftLevel;
                    index++;
                    leftLevel--;
                }

                if (rightLevel < englishLevelsCount)
                {
                    sortedEnglishLevels[index] = (EnglishLevel)rightLevel;
                    index++;
                    rightLevel++;
                }
            }

            return sortedEnglishLevels;
        }
    }
}
