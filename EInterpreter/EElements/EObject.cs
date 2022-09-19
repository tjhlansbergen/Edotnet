using System.Collections.Generic;

namespace EInterpreter.EElements
{
    public class EObject : EElement
    {
        public List<EDeclaration> Properties { get; set; } = new List<EDeclaration>();

        public EObject(string name) : base(name) { }
    }
}
