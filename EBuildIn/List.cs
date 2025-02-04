namespace EBuildIn;

public static class List
{
    public static List<Types> AddParameters => [Types.List, Types.T];
    public static Variable Add(Variable list, Variable var)
    {
        // test for type mismatch
        if (list.SubTypes?.First() != var.Type.ToString() && list.SubTypes?.First() != var.SubTypes?.First() && !string.IsNullOrEmpty(var.SubTypes?.First()))
        { 
            throw new Exception($"Type mismatch when adding value to list: {list.Name}"); 
        }

        // ensure list
        if (list.Value == null) { list.Value = new List<Variable>(); }

        // add
        ((List<Variable>)list.Value).Add(var);      // this retains the variables name etc if any, not sure about that
        return new Variable(Types.Boolean, true);
    }

    public static List<Types> FirstParameters => [Types.List];
    public static Variable First(Variable list)
    {
        return (list.Value != null && ((List<Variable>)list.Value).Any())
            ? ((List<Variable>)list.Value).First()
            : Variable.Empty;
    }

    public static List<Types> GetParameters => [Types.List, Types.Number];
    public static Variable Get(Variable list, Variable index)
    {
        if (list.Value == null) throw new Exception($"Cannot get item from empty list: {list.Name}"); 
        return ((List<Variable>)list.Value)[Convert.ToInt32(index.Value)];
    }

    public static List<Types> CountParameters => [Types.List];
    public static Variable Count(Variable list)
    {
        if(list.Value == null) { return new Variable(Types.Number, 0); }
        return new Variable(Types.Number, ((List<Variable>)list.Value).Count);
    }

    public static List<Types> RangeParameters => [Types.Number];
    public static Variable Range(Variable range)
    {
        if (range.Type != Types.Number || range.Value is not double)
        {
            throw new Exception($"Type mismatch, expected a number for List:Range"); 
        }
        
        var r = range.Value as double? ?? 0;
        var items = Enumerable.Range(0, (int)r).Select(i => new Variable(Types.Number, i)).ToList();

        var result = new Variable(Types.List, [ "List" ], items);
        return result;
    }
}
