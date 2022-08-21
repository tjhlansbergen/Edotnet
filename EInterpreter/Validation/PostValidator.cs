using EInterpreter.EObjects;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EInterpreter.Validation
{
    public class PostValidator : IValidator<ETree>
    {
        private readonly List<IPostValidationStep> _steps;
        public List<ValidationStepResult> Results { get; private set; }

        public PostValidator(List<IPostValidationStep> steps)
        {
            _steps = steps;
        }

        public bool Validate(ETree tree)
        {
            var results = new ConcurrentBag<ValidationStepResult>();

            // run steps in parallel
            Parallel.ForEach(_steps, (step) =>
            {
                var result = step.Execute(tree);
                results.Add(result);
            });

            Results = results.ToList();

            return results.All(r => r.Valid);
        }
    }
}
