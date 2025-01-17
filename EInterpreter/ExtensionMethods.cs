namespace EInterpreter;

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

    /// <summary>
    ///     Returns a string array that contains the substrings in this instance that are delimited by specified indexes.
    /// </summary>
    /// <param name="source">The original string.</param>
    /// <param name="index">An index that delimits the substrings in this string.</param>
    /// <returns>An array whose elements contain the substrings in this instance that are delimited by one or more indexes.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="index" /> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">An <paramref name="index" /> is less than zero or greater than the length of this instance.</exception>
    public static string[] SplitAt(this string source, params int[] index)
    {
        index = index.Distinct().OrderBy(x => x).ToArray();
        string[] output = new string[index.Length + 1];
        int pos = 0;

        for (int i = 0; i < index.Length; pos = index[i++] + 1)
        {
            output[i] = source.Substring(pos, index[i] - pos);
        }
            

        output[index.Length] = source.Substring(pos);
        return output;
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
