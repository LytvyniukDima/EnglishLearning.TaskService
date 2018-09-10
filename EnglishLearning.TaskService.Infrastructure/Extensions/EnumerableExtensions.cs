using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishLearning.TaskService.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        private static Random random = new Random();
        
        public static T GetRandomElement<T>(this IEnumerable<T> sequence)
        {
            if (!sequence.Any())
                return default(T);

            return sequence.ElementAt(random.Next(0, sequence.Count()));
        }

        public static IEnumerable<T> GetRandomCountOfElements<T>(this IEnumerable<T> sequence, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException();
            
            if (!sequence.Any())
                return Enumerable.Empty<T>();

            var list = new List<T>(sequence);
            
            if (list.Count <= count)
                return sequence;

            var resultList = new List<T>();
            
            for (var i = 0; i < count; i++)
            {
                var index = random.Next(0, list.Count);

                resultList.Add(list.RemoveAndGetFromList(index));
            }

            return resultList;
        }
    }
}