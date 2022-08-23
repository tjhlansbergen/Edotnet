using System;
using System.Collections.Generic;

namespace EBuildIn
{
    public static class Console
    {
        public static List<string> WriteTextParameters => new List<string> { Types.Text.ToString() };
        public static List<string> WriteNumberParameters => new List<string> { Types.Number.ToString() };
        public static List<string> WriteBooleanParameters => new List<string> { Types.Boolean.ToString() };

        public static Variable WriteText(Variable text)
        {
            var line = text.Value.ToString();
            return WriteLine(line);
        }

        public static Variable WriteNumber(Variable number)
        {
            var line = number.Value.ToString();
            return WriteLine(line);
        }

        public static Variable WriteBoolean(Variable boolean)
        {
            var line = boolean.Value.ToString();
            return WriteLine(line);
        }


        private static Variable WriteLine(string? line)
        {
            var currentColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.DarkYellow;
            System.Console.WriteLine("| " + line);
            System.Console.ForegroundColor = currentColor;

            return new Variable(Types.Boolean, true);
        }
    }
}
