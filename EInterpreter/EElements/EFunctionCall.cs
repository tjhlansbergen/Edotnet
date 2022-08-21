using System.Collections.Generic;

namespace EInterpreter.EElements
{
    public class EFunctionCall : EElement
    {
        public string Parent { get; }

        public List<string> Parameters { get; }

        public string FullName => $"{Parent}.{Name}";

        public EFunctionCall(string parent, string name, List<string> parameters) : base(name)
        {
            Parent = parent;
            Parameters = parameters;
        }
    }
}
