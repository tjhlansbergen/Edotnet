namespace EInterpreter.EElements;

public class EStatement : EElement, IRunnableBlock
{
    public EStatementType Type { get; set; }
    public string Body { get; }

    public List<EElement> Elements { get; }

    public EStatement(string name, EStatementType type, string body) : base(name)
    {
        Type = type;
        Body = body;
        Elements = new List<EElement>();
    }
}
