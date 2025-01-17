namespace EBuildIn;

public static class Console
{
    public static List<Types> WriteLineParameters => new List<Types> { Types.T };

    public static Variable WriteLine(Variable line)
    {
        var currentColor = System.Console.ForegroundColor;
        System.Console.ForegroundColor = ConsoleColor.DarkYellow;
        System.Console.WriteLine("| " + line.Value);
        System.Console.ForegroundColor = currentColor;

        return new Variable(Types.Boolean, true);
    }
}
