namespace EInterpreter.EElements
{
    public class EDeclaration : EElement
    {
        public EProperty Prop { get; }

        public EDeclaration(string type, string name) : base(name)
        {
            Prop = new EProperty(type, name);
        }
    }
}
