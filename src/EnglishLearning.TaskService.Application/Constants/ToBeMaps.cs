using System.Collections.Generic;

namespace EnglishLearning.TaskService.Application.Constants
{
    public static class ToBeMaps
    {
        public static readonly IReadOnlyDictionary<string, string> PresentToBeMap = new Dictionary<string, string>()
        {
            { "'s", "is" },
            { "'m", "am" },
            { "'re", "are" },
        };
    }
}