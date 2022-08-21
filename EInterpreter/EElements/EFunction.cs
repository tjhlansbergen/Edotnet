using System.Collections.Generic;

namespace EInterpreter.EElements
{
    public class EFunction : EElement, IRunnableBlock
    {
        public EProperty ReturnType { get; }

        public List<EProperty> Parameters { get; }

        public List<EElement> Elements { get;  }

        public EFunction(string returnType, string name, List<EProperty> parameters) : base(name)
        {
            ReturnType = new EProperty(returnType, name);
            Parameters = parameters;

            Elements = new List<EElement>();
        }
    }
}
