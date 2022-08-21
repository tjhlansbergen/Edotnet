using System.Collections.Generic;
using System.Linq;
using EInterpreter.EObjects;

namespace EInterpreter.Validation
{
    public interface IPostValidationStep
    {
        ValidationStepResult Execute(ETree tree);
    }

    public class GlobalIdentifiersAreUnique : IPostValidationStep
    {
        public ValidationStepResult Execute(ETree tree)
        {
            var identifiers = new List<string>();
            identifiers.AddRange(tree.Constants.Select(c => c.Name));
            identifiers.AddRange(tree.Utilities.Select(u => u.Name));
            identifiers.AddRange(tree.Objects.Select(o => o.Name));

            var dups = identifiers.GetNonDistinctValues().ToArray();

            return dups.Any() ? new ValidationStepResult(false, $"Not all global identifiers have unique names: {string.Join(", ", dups)}") : new ValidationStepResult(true, "All global identifiers have unique names");
        }
    }

    public class ObjectIdentifiersAreUnique : IPostValidationStep
    {
        public ValidationStepResult Execute(ETree tree)
        {
            var dups = new List<string>();

            foreach (var obj in tree.Objects)
            {
                dups.AddRange(obj.Properties.Select(p => p.Name).GetNonDistinctValues());
            }

            return dups.Any() ? new ValidationStepResult(false, $"Not all object identifiers have unique names: {string.Join(", ", dups)}") : new ValidationStepResult(true, "All object identifiers have unique names");
        }
    }

    public class UtilityIdentifiersAreUnique : IPostValidationStep
    {
        public ValidationStepResult Execute(ETree tree)
        {
            var dups = new List<string>();

            foreach (var util in tree.Utilities)
            {
                dups.AddRange(util.Functions.Select(p => p.Name).GetNonDistinctValues());
            }

            return dups.Any() ? new ValidationStepResult(false, $"Not all utility identifiers have unique names: {string.Join(", ", dups)}") : new ValidationStepResult(true, "All utility identifiers have unique names");
        }
    }

    // TODO more post-validation
}
