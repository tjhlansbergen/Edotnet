namespace EInterpreter.EElements;

public class EDeclaration : EElement
{
    public EProperty Prop { get; }
    public string[] SubTypes { get; } = new string[] {};

    public EDeclaration(string type, string name) : base(name)
    {
        Prop = new EProperty(type, name);
    }
    public EDeclaration(string type, string name, string[] subtypes) : base(name)
    {
        Prop = new EProperty(type, name);
        SubTypes = subtypes;
    }
}
