namespace EBuildIn;

public static class File
{
    public static List<Types> ReadParameters => [Types.Text];
    public static List<Types> ReadLinesParameters => [Types.Text];

    public static Variable Read(Variable path)
    {
        return new Variable(Types.Text, System.IO.File.ReadAllText((string)path.Value!));
    }

    public static Variable ReadLines(Variable path)
    {
        var lines = System.IO.File.ReadAllLines((string)path.Value!);
        var list = lines.Select(l => new Variable(Types.Text, l)).ToList();
        return new Variable(Types.List, new string[] { Types.Text.ToString() }, list);
    }
}
