namespace EInterpreter.Validation
{
    public readonly struct ValidationStepResult
    {
        public readonly bool Valid;
        public readonly string Output;

        public ValidationStepResult(bool valid, string output)
        {
            Valid = valid;
            Output = output;
        }
    }
}
