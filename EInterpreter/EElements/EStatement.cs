using System.Collections.Generic;

namespace EInterpreter.EElements
{
    public class EStatement : EElement, IRunnableBlock
    {
        public EStatementType Type { get; set; }
        public string Evaluable { get; }

        public List<EElement> Elements { get; }

        public EStatement(string name, EStatementType type, string evaluable) : base(name)
        {
            Type = type;
            Evaluable = evaluable;
            Elements = new List<EElement>();
        }
    }
}
