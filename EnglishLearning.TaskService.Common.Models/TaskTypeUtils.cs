using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishLearning.TaskService.Common.Models
{
    public static class TaskTypeUtils
    {
        public static IReadOnlyList<TaskType> GetAllTaskTypes()
        {
            return Enum
                .GetValues(typeof(TaskType))
                .Cast<TaskType>()
                .ToList();
        }
    }
}