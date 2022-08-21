using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace EInterpreter.Validation
{
    public class PreValidator : IValidator<string[]>
    {
        private readonly List<IPreValidationStep> _steps;

        public static readonly string[] Blocks = { "Utility", "Object", "Function" };
        public List<ValidationStepResult> Results { get; private set; }

        public PreValidator(List<IPreValidationStep> steps)
        {
            _steps = steps;
        }

        public bool Validate(string[] lines)
        {
            var cleanLines = _cleanCopy(lines);
            var results = new ConcurrentBag<ValidationStepResult>();

            // run steps in parallel
            Parallel.ForEach(_steps, (step) =>
            {
                var result = step.Execute(cleanLines);
                results.Add(result);
            });

            Results = results.ToList();

            return results.All(r => r.Valid);
        }

        private static string[] _cleanCopy(string[] lines)
        {
            var cleanLines = (string[])lines.Clone();

            //strip comments and stuff between double quotes
            var regex = new Regex("\"([^\"]*)\"");

            for (int i = 0; i < cleanLines.Length; i++)
            {
                //remove indentation
                cleanLines[i] = cleanLines[i].Trim();

                //empty line if it is a comment
                if (cleanLines[i].StartsWith("//")) cleanLines[i] = string.Empty;

                //remove text between double quotes
                cleanLines[i] = regex.Replace(cleanLines[i], "\"\"");
            }

            return cleanLines;
        }
    }
}
