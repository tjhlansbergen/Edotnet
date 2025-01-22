namespace EBuildIn;

public static class Number
{
    public static List<Types> AddParameters => [Types.Number, Types.Number];
    public static List<Types> SubtractParameters => [Types.Number, Types.Number];
    public static List<Types> MultiplyParameters => [Types.Number, Types.Number];
    public static List<Types> DivideParameters => [Types.Number, Types.Number];
    public static List<Types> RemainderParameters => [Types.Number, Types.Number];
    
    public static List<Types> AreEqualParameters => [Types.Number, Types.Number];
    public static List<Types> NotEqualParameters => [Types.Number, Types.Number];
    public static List<Types> LessThenParameters => [Types.Number, Types.Number];
    public static List<Types> GreaterThenParameters => [Types.Number, Types.Number];

    public static List<Types> ToTextParameters => [Types.Number];

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

    public static Variable ToText(Variable a)
    {
        return new Variable(Types.Text, a.Value?.ToString());
    }

    private static Variable Comparison(Variable x, Variable y, Func<double, double, bool> compare)
    {
        if (TryConvertValue(x, out double xx) && TryConvertValue(y, out double yy) && compare(xx, yy))
        {
            return new Variable(Types.Boolean, true);
        }
        else
        {
            return new Variable(Types.Boolean, false);
        }
    }

    private static Variable Operator(Variable var, Variable value, Func<double, double, double> operate)
    {
        TryConvertValue(value, out var result); 
        return new Variable(Types.Number, operate((double)(var.Value ?? double.NaN), result));
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
