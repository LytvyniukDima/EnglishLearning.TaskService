using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishLearning.TaskService.Application.Common
{
    public static class IEnumerableExtensions
    {
        private static Random random = new Random();
        
        public static T GetRandomValue<T>(this IEnumerable<T> sequence)
        {
            if (!sequence.Any())
                return default(T);

            return sequence.ElementAt(random.Next(0, sequence.Count()));
        }
    }
}