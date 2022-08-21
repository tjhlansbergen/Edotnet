namespace EInterpreter.EElements
{
    public class EProperty : EElement
    {
        public string Type { get; }

        public EProperty(string type, string name) : base(name)
        {
             Type = type;
        }
    }
}
