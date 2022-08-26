﻿using System;
using System.Collections.Generic;

namespace EBuildIn
{
    public static class List
    {
        public static List<Types> AddParameters => new List<Types> { Types.List, Types.T };
        public static List<Types> FirstParameters => new List<Types> { Types.List };

        public static Variable Add(Variable list, Variable var)
        {
            // test for type mismatch
            if (list.SubTypes?.First() != var.Type) { return new Variable(Types.Boolean, false); }

            // ensure list
            if (list.Value == null) { list.Value = new List<object>(); }

            // add
            ((List<object>)list.Value).Add(var.Value);
            return new Variable(Types.Boolean, true);
        }

        public static Variable First(Variable list)
        {
            return new Variable(list.SubTypes.First(), ((List<object>)list.Value).FirstOrDefault());
        }
    }
}
