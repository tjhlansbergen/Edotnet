namespace EBuildIn;

public static class Directory
{
    public static List<Types> FilesParameters => [Types.Text];
    
    public static Variable Files(Variable path)
    {
        var files = System.IO.Directory.GetFiles(path.Value as string ?? string.Empty);
        var list = files.Select(f => new Variable(Types.Text, f)).ToList();
        return new Variable(Types.List, [Types.Text.ToString()], list);
    }

    public static List<Types> DirectoriesParameters => [Types.Text];
    
    public static Variable Directories(Variable path)
    {
        var dirs = System.IO.Directory.GetDirectories(path.Value as string ?? string.Empty);
        var list = dirs.Select(d => new Variable(Types.Text, d)).ToList();
        return new Variable(Types.List, [Types.Text.ToString()], list);
    }

    public static List<Types> CurrentParameters => [];

    public static Variable Current()
    {
        return new Variable(Types.Text, System.IO.Directory.GetCurrentDirectory());
    }
}
