﻿using System.Linq;

namespace EnglishLearning.TaskService.Infrastructure.Extensions
{
    public static class ArrayExtensions
    {
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            if (array == null)
                return true;

            if (!array.Any())
                return true;

            return false;
        }
    }
}