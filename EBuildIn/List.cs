using System;
using System.Collections.Generic;

namespace EBuildIn
{
    public static class List
    {
        public static List<Types> AddParameters => new List<Types> { Types.List, Types.T };
        public static List<Types> FirstParameters => new List<Types> { Types.List };
        public static List<Types> GetParameters => new List<Types> { Types.List, Types.Number };
        public static List<Types> CountParameters => new List<Types> { Types.List };

        public static Variable Add(Variable list, Variable var)
        {
            // test for type mismatch
            if (list.SubTypes?.First() != var.Type) { return new Variable(Types.Boolean, false); }

            // ensure list
            if (list.Value == null) { list.Value = new List<Variable>(); }

            // add
            ((List<Variable>)list.Value).Add(var);      // this retains the variables name etc if any, not sure about that
            return new Variable(Types.Boolean, true);
        }

        public static Variable First(Variable list)
        {
            return (list.Value != null && ((List<Variable>)list.Value).Any())
                ? ((List<Variable>)list.Value).First()
                : Variable.Empty;
        }

        public static Variable Get(Variable list, Variable index)
        {
            return ((List<Variable>)list.Value)[Convert.ToInt32(index.Value)];
        }

        public static Variable Count(Variable list)
        {
            if(list.Value == null) { return new Variable(Types.Number, 0); }
            return new Variable(Types.Number, ((List<Variable>)list.Value).Count);
        }
    }
}
