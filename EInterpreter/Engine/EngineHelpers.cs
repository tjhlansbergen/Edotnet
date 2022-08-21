using System.Collections.Generic;
using System.Linq;
using EBuildIn;
using EInterpreter.EElements;

namespace EInterpreter.Engine
{
    public static class EngineHelpers
    {
        public static bool MatchAndNameParameters(List<Variable> callerParameters, EFunction subject)
        {
            // invalid call
            if (callerParameters == null || subject == null) { return false; }

            // first, check the number of parameters
            if (callerParameters.Count != subject.Parameters.Count) { return false; }

            // match and name all parameters by type, in order of appearance
            var match = true;
            for (var i = 0; i < callerParameters.Count; i++)
            {
                if (callerParameters[i].Type != subject.Parameters[i].Type)
                {
                    match = false;
                    break;
                }

                callerParameters[i].Name = subject.Parameters[i].Name;
            }

            return match;
        }

        public static bool MatchParameters(List<Variable> callerParameters, List<string> targetParameterTypes)
        {
            // invalid call
            if (callerParameters == null || targetParameterTypes == null) { return false; }

            // first, check the number of parameters
            if (callerParameters.Count != targetParameterTypes.Count) { return false; }

            return !callerParameters.Where((t, i) => t.Type != targetParameterTypes[i]).Any();
        }
    }
}
