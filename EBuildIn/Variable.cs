namespace EBuildIn
{
    public class Variable
    {
        public string? Name { get; set; }
        public string Type { get; private set; }
        public List<Types> SubTypes { get; private set; } = new List<Types>();   // variables that have a underlying type such as lists. This is list because there can be more than one underlying type (e.g. dictionary, tuple, etc.)
        public object? Value { get; set; }
        public string Scope { get; set; }

        public bool IsEmpty => Type == string.Empty && Value == null && Scope == string.Empty;

        public static Variable Empty => new Variable(string.Empty, null, string.Empty);

        public Variable(string type, object? value, string scope = "")
        {
            Type = type;
            Value = value;
            Scope = scope;
        }
        public Variable(EBuildIn.Types type, object? value, string scope = "")
        {
            Type = type.ToString();
            Value = value;
            Scope = scope;
        }
    }
}
