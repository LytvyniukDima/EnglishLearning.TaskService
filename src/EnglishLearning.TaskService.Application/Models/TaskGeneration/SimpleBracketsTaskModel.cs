using System.Collections.Generic;

namespace EnglishLearning.TaskService.Application.Models.TaskGeneration
{
    public class SimpleBracketsTaskModel
    {
        public IReadOnlyList<SimpleBracketsLineModel> Lines { get; set; }
    }
}