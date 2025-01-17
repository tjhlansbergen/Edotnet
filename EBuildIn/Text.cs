namespace EBuildIn;

public static class Text
{
    public static List<Types> SplitParameters => new List<Types> { Types.Text, Types.Text };
    public static List<Types> AppendParameters => new List<Types> { Types.Text, Types.Text };
    public static List<Types> EqualsParameters => new List<Types> { Types.Text, Types.Text };

    public static Variable Split(Variable text, Variable delimeter)
    {
        var splits = ((string)text.Value).Split((string)delimeter.Value);
        var results = splits.Select(s => new Variable(Types.Text, s)).ToList();

        return new Variable(Types.List, subTypes: new string[] { Types.Text.ToString() }, results);
    }

    public static Variable Append(Variable var, Variable value)
    {
        if (value.Value is string)
        {
            var.Value += value.Value.ToString();
            return new Variable(Types.Boolean, true);
        }

        try
        {
            var.Value += value.Value.ToString();
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
        return new Variable(Types.Boolean, var.Value?.ToString() == value.Value?.ToString());
    }

    public static Variable Contains(Variable var, Variable value)
    {
        if(var?.Value == null || value?.Value == null)
        {
            // if either side is null thats a false
            return new Variable(Types.Boolean, false);
        }
        return new Variable(Types.Boolean, var.Value.ToString().Contains(value.Value.ToString()));
    }
}
