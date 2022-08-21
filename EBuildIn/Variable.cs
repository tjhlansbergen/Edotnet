namespace EBuildIn
{
    public class Variable
    {
        public string? Name { get; set; }
        public string Type { get; private set; }
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
    }
}
