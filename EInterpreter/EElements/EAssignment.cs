namespace EInterpreter.EElements
{
    public class EAssignment : EElement
    {
        public string Parameter { get; }

        public EAssignment(string name, string parameter) : base(name)
        {
            Parameter = parameter;
        }
    }
}
