namespace EBuildIn;

public class Variable
{
    public string? Name { get; set; }
    public Types Type { get; private set; }
    public string[]? SubTypes { get; private set; }   // variables that have a underlying type such as lists. This is list because there can be more than one underlying type (e.g. dictionary, tuple, etc.)
    public object? Value { get; set; }
    public string Scope { get; set; }

    public bool IsEmpty => Type == Types.Empty && Value == null && Scope == string.Empty;

    public static Variable Empty => new Variable(Types.Empty, null, string.Empty);

    public Variable(EBuildIn.Types type, object? value, string scope = "")
    {
        Type = type;
        Value = value;
        Scope = scope;
    }

    public Variable(string name, EBuildIn.Types type, object? value, string scope = "")
    {
        Name = name;
        Type = type;
        Value = value;
        Scope = scope;
    }

    public Variable(EBuildIn.Types type, IEnumerable<string> subTypes, object? value, string scope = "")
    {
        Type = type;
        Value = value;
        Scope = scope;
        SubTypes = subTypes.ToArray();
    }
}
