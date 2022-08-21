using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace EBuildIn
{
    public static class Number
    {
        public static List<string> AddParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };
        public static List<string> SubtractParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };
        public static List<string> MultiplyParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };
        public static List<string> DivideParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };
        public static List<string> RemainderParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };
        
        public static List<string> AreEqualParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };
        public static List<string> NotEqualParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };
        public static List<string> LessThenParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };
        public static List<string> GreaterThenParameters => new List<string> { Types.Number.ToString(), Types.Number.ToString() };

        public static Variable Add(Variable var, Variable value)
        {
            return Operator(var, value, (x, y) => x + y);
        }
        public static Variable Subtract(Variable var, Variable value)
        {
            return Operator(var, value, (x, y) => x - y);
        }
        public static Variable Divide(Variable var, Variable value)
        {
            return Operator(var, value, (x, y) => x / y);
        }
        public static Variable Multiply(Variable var, Variable value)
        {
            return Operator(var, value, (x, y) => x * y);
        }
        public static Variable Remainder(Variable var, Variable value)
        {
            return Operator(var, value, (x, y) => x % y);
        }

        public static Variable AreEqual(Variable a, Variable b)
        {
            return Comparison(a, b, (x, y) => x == y);
        }
        public static Variable NotEqual(Variable a, Variable b)
        {
            return Comparison(a, b, (x, y) => x != y);
        }
        public static Variable LessThen(Variable a, Variable b)
        {
            return Comparison(a, b, (x, y) => x < y);
        }
        public static Variable GreaterThen(Variable a, Variable b)
        {
            return Comparison(a, b, (x, y) => x > y);
        }

        private static Variable Comparison(Variable x, Variable y, Func<double, double, bool> compare)
        {
            if (TryConvertValue(x, out double xx) && TryConvertValue(y, out double yy) && compare(xx, yy))
            {
                return new Variable(Types.Boolean.ToString(), true);
            }
            else
            {
                return new Variable(Types.Boolean.ToString(), false);
            }
        }

        private static Variable Operator(Variable var, Variable value, Func<double, double, double> operate)
        {
            if (TryConvertValue(value, out var result))
            {
                var.Value = operate((double)var.Value, result);
                return new Variable(Types.Boolean.ToString(), true);
            }
            else
            {
                return new Variable(Types.Boolean.ToString(), false);
            }
        }

        private static bool TryConvertValue(Variable value, out double result)
        {
            try
            {
                result = value.Value is double d ? d : Convert.ToDouble(value.Value);
                return true;
            }
            catch
            {
                result = double.NaN;
                return false;
            }
        }
    }
}
