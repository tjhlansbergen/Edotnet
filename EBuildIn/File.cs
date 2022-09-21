using System.IO;

namespace EBuildIn
{
    public static class File
    {
        public static List<Types> ReadParameters => new List<Types> { Types.Text };
        public static List<Types> ReadLinesParameters => new List<Types> { Types.Text };

        public static Variable Read(Variable path)
        {
            return new Variable(Types.Text, System.IO.File.ReadAllText((string)path.Value));
        }

        public static Variable ReadLines(Variable path)
        {
            var lines = System.IO.File.ReadAllLines((string)path.Value);
            return new Variable(Types.List, new string[] { Types.Text.ToString() }, lines.Cast<object>().ToList());
        }
    }
}
