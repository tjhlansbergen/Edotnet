using System.Collections.Generic;

namespace EInterpreter.Validation
{
    public interface IValidator<T>
    {
        List<ValidationStepResult> Results { get; }
        bool Validate(T content);

    }
}