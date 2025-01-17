namespace EBuildIn;

public static class Directory
{
    public static List<Types> FilesParameters => new List<Types> { Types.Text };
    
    public static Variable Files(Variable path)
    {
        var files = System.IO.Directory.GetFiles((string)path.Value);
        var list = files.Select(f => new Variable(Types.Text, f)).ToList();
        return new Variable(Types.List, new string[] { Types.Text.ToString() }, list);
    }

    public static List<Types> DirectoriesParameters => new List<Types> { Types.Text };
    
    public static Variable Directories(Variable path)
    {
        var dirs = System.IO.Directory.GetDirectories((string)path.Value);
        var list = dirs.Select(d => new Variable(Types.Text, d)).ToList();
        return new Variable(Types.List, new string[] { Types.Text.ToString() }, list);
    }

    public static List<Types> CurrentParameters => new List<Types> { };

    public static Variable Current()
    {
        return new Variable(Types.Text, System.IO.Directory.GetCurrentDirectory());
    }
}
