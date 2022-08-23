using System;

namespace EBuildIn
{
    public static class Text
    {
        public static List<string> AppendParameters => new List<string> { Types.Text.ToString(), Types.Text.ToString() };
        public static List<string> EqualsParameters => new List<string> { Types.Text.ToString(), Types.Text.ToString() };

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
            return new Variable(Types.Boolean, var.Value.ToString() == value.Value.ToString());
        }

        public static Variable Contains(Variable var, Variable value)
        {
            return new Variable(Types.Boolean, var.Value.ToString().Contains(value.Value.ToString()));
        }
    }
}
