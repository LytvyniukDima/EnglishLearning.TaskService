using System;
using System.Collections.Generic;
using System.Linq;
using EnglishLearning.TaskService.Application.Abstract.TaskGeneration;
using EnglishLearning.TaskService.Application.Constants;
using EnglishLearning.TaskService.Application.Models;
using EnglishLearning.TaskService.Application.Models.TaskGeneration;
using EnglishLearning.TaskService.Application.Models.TextAnalyze;
using EnglishLearning.TaskService.Common.Models;
using EnglishLearning.Utilities.Linq.Extensions;
using MongoDB.Bson;
using static EnglishLearning.TaskService.Application.Constants.TaskGenerationConstants;

namespace EnglishLearning.TaskService.Application.Services.TaskGeneration
{
    public class PresentSimpleBracketsTaskItemsGenerator : ITaskItemsGeneratorService
    {
        private static readonly IReadOnlyList<string> PositiveVerbTags = new[]
        {
            GrammarTags.VBP,
            GrammarTags.VBZ,
        };

        public IReadOnlyList<CreateTaskItemModel> GenerateTaskItems(
            string generationId,
            GenerateTaskModel generateTaskModel,
            IReadOnlyList<ParsedSentModel> parsedSentModels)
        {
            return parsedSentModels
                .Select(x => GenerateTaskItem(generationId, generateTaskModel, x))
                .ToList();
        }

        private CreateTaskItemModel GenerateTaskItem(
            string generationId,
            GenerateTaskModel generateTaskModel,
            ParsedSentModel parsedSent)
        {
            var bracketsModel = parsedSent.SentType switch
            {
                SentTypes.PresentSimpleNegative => GenerateFromPresentSimpleNegative(parsedSent),
                SentTypes.PresentSimpleQuestion => GenerateFromPresentSimpleQuestion(parsedSent),
                SentTypes.PresentSimplePositive => GenerateFromPresentSimplePositive(parsedSent),
                _ => throw new ArgumentException($"SentType not supported {parsedSent.SentType}")
            };

            if (bracketsModel == null)
            {
                return null;
            }
            
            return new CreateTaskItemModel
            {
                TaskGenerationId = generationId,
                SourceSentId = parsedSent.Id,
                GrammarPart = generateTaskModel.GrammarPart,
                SentType = parsedSent.SentType,
                TaskType = TaskType.SimpleBrackets,
                Content = bracketsModel.ToBsonDocument(),
            };
        }

        private SimpleBracketsTaskModel GenerateFromPresentSimpleNegative(ParsedSentModel parsedSent)
        {
            return new SimpleBracketsTaskModel();
        }

        private SimpleBracketsTaskModel GenerateFromPresentSimplePositive(ParsedSentModel parsedSent)
        {
            var verbIndex = parsedSent.Tokens
                .FindIndexOf(x => PositiveVerbTags.Contains(x.Tag));

            if (verbIndex == -1)
            {
                return null;
            }

            var verbToken = parsedSent.Tokens[verbIndex];

            var startItems = parsedSent.Tokens
                .Take(verbIndex)
                .Select(x => x.Word);
            var endItems = parsedSent.Tokens
                .Skip(verbIndex + 1)
                .SkipLast(1)
                .Select(x => x.Word);

            var allTaskTokens = startItems
                .Concat(new[] { OptionString })
                .Concat(endItems)
                .ToList();

            var sent = string.Join(' ', allTaskTokens);
            sent = $"{sent}{parsedSent.Tokens[^1].Word}";
            
            var bracketsLine = new SimpleBracketsLineModel()
            {
                Content = sent,
                Option = verbToken.Lemma,
                Answer = new[] { MapTokenToAnswer(verbToken) },
            };

            return new SimpleBracketsTaskModel()
            {
                Lines = new[] { bracketsLine },
            };
        }
        
        private SimpleBracketsTaskModel GenerateFromPresentSimpleQuestion(ParsedSentModel parsedSent)
        {
            return new SimpleBracketsTaskModel();
        }

        private string MapTokenToAnswer(SentTokenModel sentToken)
        {
            if (sentToken.Lemma == "be"
                && ToBeMaps.PresentToBeMap.TryGetValue(sentToken.Word, out var mappedValue))
            {
                return mappedValue;
            }

            return sentToken.Word;
        }
    }
}