namespace EInterpreter.EElements
{
    public class EReturn : EElement
    {
        public string Parameter { get; }

        public EReturn(string name, string parameter) : base(name)
        {
            Parameter = parameter;
        }
    }
}
