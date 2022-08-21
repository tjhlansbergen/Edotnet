using System;
using System.Collections.Generic;
using System.Linq;

namespace EInterpreter
{
    public static class ExtensionMethods
    {
        public static string[] SplitClean(this string str, char ch, int count = -1)
        {
            if (count == -1)
            {
                return str.Split(new[] { ch }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                return str.Split(new[] { ch }, count, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public static IEnumerable<T> GetNonDistinctValues<T>(this IEnumerable<T> list)
        {
            return list.GroupBy(i => i).Where(g => g.Count() > 1).Select(g => g.Key);
        }

        public static string LargestUnit(this TimeSpan span)
        {
            if (span.TotalSeconds < 2) return $"{span.TotalMilliseconds} milliseconds";

            if (span.TotalMinutes < 2) return $"{span.TotalSeconds} seconds";

            if (span.TotalHours < 2) return $"{span.TotalMinutes} minutes";

            return $"{span.TotalHours} hours";
        }
    }

    public static class Extensions
    {
        public static void WriteColoredLine(string line, ConsoleColor color)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ForegroundColor = currentColor;
        }
    }
}
