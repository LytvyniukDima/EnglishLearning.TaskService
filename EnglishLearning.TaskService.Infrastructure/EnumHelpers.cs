using System;
using System.Collections.Generic;

namespace EnglishLearning.TaskService.Infrastructure
{
    public static class EnumHelpers
    {
        public static IEnumerable<T> ConverToEnumArray<T>(string[] stringValues)
        {
            foreach (var stringValue in stringValues)
            {
                yield return ConvertToEnum<T>(stringValue);
            }
        }
        
        public static T ConvertToEnum<T>(string stringValue)
        {
            return (T) Enum.Parse(typeof(T), stringValue);
        }
    }
}