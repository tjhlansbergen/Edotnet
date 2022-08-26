using System;
using System.Collections.Generic;
using System.Reflection;

namespace EBuildIn
{
    public static class Modules
    {
        private static readonly string namespac = "EBuildIn";

        public static List<Types>? FindFunctionAndReturnParameters(string module, string function)
        {
            if(module == "Modules" || module == "Types") { return null; }

            function += "Parameters";
            var type = Type.GetType($"{namespac}.{module}");
            var propertyInfo = type?.GetProperty(function, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);

            return (List<Types>)propertyInfo?.GetValue(null);
        }

        public static Variable Run(string module, string function, object[] parameters)
        {
            var type = Type.GetType($"{namespac}.{module}");
            var method = type?.GetMethod(function, BindingFlags.Static | BindingFlags.Public);

            // each method is responsible for returning a correct Variable
            return (Variable)method?.Invoke(null, parameters);

        }
    }
}
