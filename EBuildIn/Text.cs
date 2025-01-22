namespace EBuildIn;

public static class Text
{
    public static List<Types> SplitParameters => [Types.Text, Types.Text];
    public static List<Types> AppendParameters => [Types.Text, Types.Text];
    public static List<Types> EqualsParameters => [Types.Text, Types.Text];

    public static Variable Split(Variable text, Variable delimeter)
    {
        var splits = (text.Value as string ?? string.Empty).Split(delimeter.Value as string ?? string.Empty);
        var results = splits.Select(s => new Variable(Types.Text, s)).ToList();

        return new Variable(Types.List, subTypes: [Types.Text.ToString()], results);
    }

    public static Variable Append(Variable var, Variable value)
    {
        if (value.Value is string)
        {
            var.Value += value.Value as string;
            return new Variable(Types.Boolean, true);
        }

        try
        {
            var.Value += value.Value as string;
            return new Variable(Types.Boolean, true);
        }
        catch (Exception)
        {
            return new Variable(Types.Boolean, false);
        }
    }

    public static Variable Equals(Variable var, Variable value)
    {
        // note that null == null will return true here
        return new Variable(Types.Boolean, var.Value as string == value.Value as string);
    }

    public static Variable Contains(Variable var, Variable value)
    {
        if(var?.Value is not string || value?.Value is not string)
        {
            // if either side is not a string (including nill) thats a false
            return new Variable(Types.Boolean, false);
        }

        return new Variable(Types.Boolean, (var.Value as string)?.Contains(value.Value as string ?? string.Empty));
    }
}
