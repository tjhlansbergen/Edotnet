using System.Collections.Generic;

namespace EInterpreter.EElements
{
    public class EObject : EElement
    {
        public List<EProperty> Properties { get; set; } = new List<EProperty>();

        public EObject(string name) : base(name) { }
    }
}
