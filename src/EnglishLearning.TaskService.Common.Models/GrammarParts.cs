using System.Collections.Generic;

namespace EnglishLearning.TaskService.Common.Models
{
    public static class GrammarParts
    {
        public const string PresentSimple = "Present Simple";
        public const string QuestionTags = "Question Tags";
        public const string Suffixes = "Suffixes";
        public const string PresentPerfect = "Present Perfect";
        public const string Test = "Test";
        public const string FuturePlans = "Future Plans";
        public const string PresentSimpleAndPresentContinuous = "Present Simple And Present Continuous";
        public const string PossessiveAdjectives = "PossessiveAdjectives";

        public static readonly IReadOnlyDictionary<string, IReadOnlyList<string>> GrammarPartSentTypesMap = new Dictionary<string, IReadOnlyList<string>>()
        {
            {
                PresentSimple,
                new[]
                {
                    SentTypes.PresentSimpleNegative,
                    SentTypes.PresentSimplePositive,
                    SentTypes.PresentSimpleQuestion,
                }
            },
        };

        public static readonly IReadOnlyList<string> AvailableGrammarParts = new[]
        {
            GrammarParts.Suffixes,
            GrammarParts.Test,
            GrammarParts.FuturePlans,
            GrammarParts.PossessiveAdjectives,
            GrammarParts.PresentPerfect,
            GrammarParts.PresentSimple,
            GrammarParts.QuestionTags,
            GrammarParts.PresentSimpleAndPresentContinuous,
        };
    }
}