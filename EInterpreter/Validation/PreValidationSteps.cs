using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EInterpreter.Validation
{
    public interface IPreValidationStep
    {
        ValidationStepResult Execute(string[] lines);
    }

    public class HasValidLineEndsStep : IPreValidationStep
    {

        public ValidationStepResult Execute(string[] lines)
        {
            var linesWithProblem = new List<int>();

            //inspect each line
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                //omit object declarations (we don't know what they end with) or blank lines
                if (line.StartsWith("Utility") || line.StartsWith("Object") || string.IsNullOrWhiteSpace(line)) continue;

                //check if line ends with one of
                if (line.EndsWith(";") || line.EndsWith("{") || line.EndsWith("}") || line.EndsWith(")")) continue;

                // Problem found
                linesWithProblem.Add(i + 1);
            }

            var valid = !linesWithProblem.Any();

            return new ValidationStepResult(valid, valid ? "All line endings are valid" : $"Invalid line ending at lines: {string.Join(", ", linesWithProblem)}");
        }
    }

    public class HasMatchingQuotes : IPreValidationStep
    {
        public ValidationStepResult Execute(string[] lines)
        {
            var code = string.Join(string.Empty, lines);

            var quotesComplete = (code.Length - code.Replace("\"", "").Length) % 2 == 0;

            return new ValidationStepResult(quotesComplete, quotesComplete ? "All quotes (\"\") are closed" : "Not all quotes (\"\") are closed");
        }
    }

    public class HasMatchingBraces : IPreValidationStep
    {
        public ValidationStepResult Execute(string[] lines)
        {
            var code = string.Join(string.Empty, lines);

            var braces = new List<Tuple<char, char>>
            {
                new Tuple<char, char>('{', '}'),
                new Tuple<char, char>('[', ']'),
                new Tuple<char, char>('<', '>'),
                new Tuple<char, char>('(', ')')
            };

            var results = new List<KeyValuePair<char, int>>();

            Parallel.ForEach(braces, (brace) =>
            {
                var missingBraces = _missingChars(code, brace.Item1, brace.Item2, out char missing);
                if (missingBraces != 0)
                {
                    results.Add(new KeyValuePair<char, int>(missing, missingBraces));
                }
            });

            if (!results.Any())
            {
                return new ValidationStepResult(true, "All braces match");
            }
            else
            {
                var output = results.Aggregate("Not all braces match, missing: ", (current, result) => current + $"{result.Value}x {result.Key} ");
                return new ValidationStepResult(false, output);
            }
        }

        private static int _missingChars(string code, char a, char b, out char missing)
        {
            var _a = code.Length - code.Replace(a.ToString(), "").Length;
            var _b = code.Length - code.Replace(b.ToString(), "").Length;

            if (_a > _b)
            {
                missing = b;
                return _a - _b;
            }

            if (_a < _b)
            {
                missing = a;
                return _b - _a;
            }

            missing = (char)0;
            return 0;
        }
    }

    public class ContainsProgramUtility : IPreValidationStep
    {
        public ValidationStepResult Execute(string[] lines)
        {
            var allcode = string.Join(string.Empty, lines);

            return allcode.Contains("Utility Program") ? new ValidationStepResult(true, "Utility Program found.") : new ValidationStepResult(false, "Utility Program not found.");
        }
    }

    public class ContainsStartFunction : IPreValidationStep
    {
        public ValidationStepResult Execute(string[] lines)
        {
            var allcode = string.Join(string.Empty, lines);

            if (allcode.Contains("Function Boolean Start"))
            {
                return new ValidationStepResult(true, "Start Function found.");
            }
            else if(allcode.Contains("Function Start"))
            {
                return new ValidationStepResult(false, "Start Function found, but mandatory return type of 'Boolean' missing.");
            }
            else
            {
                return new ValidationStepResult(false, "Start Function not found.");
            }
        }
    }

    public class BlockOpeningsOk : IPreValidationStep
    {
        public ValidationStepResult Execute(string[] lines)
        {
            var results = new List<Tuple<int, string>>();

            for (int i = 0; i < lines.Length; i++)
            {
                foreach (var block in PreValidator.Blocks)
                {
                    if (lines[i].StartsWith(block))
                    {
                        if (!(i < lines.Length - 1) || lines[i + 1].Trim() != "{")
                        {
                            results.Add(new Tuple<int, string>(i + 1, block));
                        }
                    }
                }
            }

            if (!results.Any())
            {
                return new ValidationStepResult(true, "All block openings are OK");
            }
            else
            {
                var output = results.Aggregate("One or more blocks are missing an opening bracket: ", (current, result) => current + $"{result.Item2} at line {result.Item1} ");
                return new ValidationStepResult(false, output);
            }
        }
    }

    public class BlockDeclarationsOk : IPreValidationStep
    {
        public ValidationStepResult Execute(string[] lines)
        {
            var results = new List<Tuple<int, string>>();

            for (int i = 0; i < lines.Length; i++)
            {
                foreach (var block in PreValidator.Blocks)
                {
                    if (block == "Function")
                    {
                        if (lines[i].StartsWith(block) && lines[i].Split('(')[0].Split(' ').Length != 3)
                        {
                            results.Add(new Tuple<int, string>(i + 1, block));
                        }
                    }
                    else
                    {
                        if (lines[i].StartsWith(block) && lines[i].Split(' ').Length != 2)
                        {
                            results.Add(new Tuple<int, string>(i + 1, block));
                        }
                    }

                }
            }

            if (!results.Any())
            {
                return new ValidationStepResult(true, "All block declarations are OK");
            }
            else
            {
                var output = results.Aggregate("One or more block declarations invalid: ", (current, result) => current + $"{result.Item2} at line {result.Item1} ");
                return new ValidationStepResult(false, output);
            }
        }
    }

    public class ConstantsOk : IPreValidationStep
    {
        public ValidationStepResult Execute(string[] lines)
        {
            var errors = new List<int>();
            var constants = lines.Where(ln => ln.StartsWith("Constant")).ToArray();

            for(int i = 0; i < constants.Length; i++)
            {
                if (!constants[i].Contains("="))
                {
                    errors.Add(i + 1);
                    continue;
                }

                if (constants[i].SplitClean('=').Length != 2)
                {
                    errors.Add(i + 1);
                    continue;
                }

                if (constants[i].SplitClean('=')[0].SplitClean(' ').Length != 3)
                {
                    errors.Add(i + 1);
                }
            }

            if (!errors.Any())
            {
                return new ValidationStepResult(true, "All Constant declarations are OK");
            }
            else
            {
                return new ValidationStepResult(false, $"Improper Constant declaration on line(s): {string.Join(" ", errors)}");
            }
        }
    }

    public class PropertiesOk : IPreValidationStep
    {
        public ValidationStepResult Execute(string[] lines)
        {
            var errors = new List<int>();
            var props = lines.Where(ln => ln.StartsWith("Property")).ToArray();

            for (int i = 0; i < props.Length; i++)
            {
                if (props[i].SplitClean(' ').Length != 3)
                {
                    errors.Add(i + 1);
                }
            }

            if (!errors.Any())
            {
                return new ValidationStepResult(true, "All Property declarations are OK");
            }
            else
            {
                return new ValidationStepResult(false, $"Improper Property declaration on line(s): {string.Join(" ", errors)}");
            }
        }
    }

    public class AssignemtnsOk : IPreValidationStep
    {
        public ValidationStepResult Execute(string[] lines)
        {
            var errors = new List<int>();
            var assignment = lines.Where(ln => ln.Contains("=")).ToArray();

            for (int i = 0; i < assignment.Length; i++)
            {
                if (assignment[i].SplitClean('=').Length != 2)
                {
                    errors.Add(i + 1);
                }
            }

            if (!errors.Any())
            {
                return new ValidationStepResult(true, "All assignments are OK");
            }
            else
            {
                return new ValidationStepResult(false, $"Improper assignment on line(s): {string.Join(" ", errors)}");
            }
        }
    }
}
