using EInterpreter.Validation;
using EInterpreter.Lexing;

namespace EInterpreter;

public class Worker
{
    public bool Verbose { get; set; } = true;
    public string Name = string.Empty;

    public Worker(TextWriter? outputChannel = null)
    {
        // divert output if requested
        if (outputChannel != null) Console.SetOut(outputChannel);
    }

    public void Go(string[] lines, string name)
    {
        Name = name;

        Console.WriteLine();
        Extensions.WriteColoredLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", ConsoleColor.Magenta);
        Console.WriteLine();

        try
        {
            PreValidate(lines);

            var tree = Lex(lines);
            PostValidate(tree);
            RunEngine(tree);
        }
        catch (Exception ex)
        {
            Extensions.WriteColoredLine(ex.Message, ConsoleColor.Red);
        }

        Console.WriteLine();
        Extensions.WriteColoredLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", ConsoleColor.Magenta);
        Console.WriteLine();
    }

    private void PreValidate(string[] lines)
    {
        Extensions.WriteColoredLine("Pre-validation: ", ConsoleColor.DarkCyan);

        var validator = new PreValidator(
        [
            new HasValidLineEndsStep(),
            new HasMatchingQuotes(),
            new HasMatchingBraces(),
            new ContainsProgramUtility(),
            new ContainsStartFunction(),
            new BlockOpeningsOk(),
            new BlockDeclarationsOk(),
            new ConstantsOk(),
            new PropertiesOk(),
            new AssignemtnsOk(),
        ]);
            
        var preValidationResult = validator.Validate(lines);

        if (Verbose)
        {
            foreach (var validationStepResult in validator.Results)
            {
                Extensions.WriteColoredLine(" - " + validationStepResult.Output, validationStepResult.Valid ? ConsoleColor.DarkGreen : ConsoleColor.Red);
            }
        }

        Console.WriteLine();
        Console.WriteLine(preValidationResult ? $"Pre-validation for `{Name}` successful" : $"Pre-validation for {Name} failed!");
        Console.WriteLine();
        if(preValidationResult == false) { throw new PreValidationException("EInterpreter stopped before execution because of failing Pre-validation");}
    }

    private static ETree Lex(string[] lines)
    {
        Extensions.WriteColoredLine("Lexing: ", ConsoleColor.DarkCyan);
        var tree = new Lexer().GetTree(lines);
        if (tree != null)
        {
            Console.WriteLine(tree.Summarize());
            return tree;
        }
        
        Extensions.WriteColoredLine("The parser did not return an object tree!", ConsoleColor.Red);
        throw new LexerException("EInterpreter stopped before execution because of a lexer exception");
    }

    private void PostValidate(ETree tree)
    {
        Extensions.WriteColoredLine("Post-validation: ", ConsoleColor.DarkCyan);
        var validator = new PostValidator(
        [
            new GlobalIdentifiersAreUnique(),
            new ObjectIdentifiersAreUnique(),
            new UtilityIdentifiersAreUnique()
        ]);
        
        var postValidationResult = validator.Validate(tree);

        if (Verbose)
        {
            foreach (var validationStepResult in validator.Results)
            {
                Extensions.WriteColoredLine(" - " + validationStepResult.Output, validationStepResult.Valid ? ConsoleColor.DarkGreen : ConsoleColor.Red);
            }
        }

        Console.WriteLine();
        Console.WriteLine(postValidationResult ? $"Post-validation for `{Name}` successful" : $"Post-validation for {Name} failed!");
        Console.WriteLine();
        if(postValidationResult == false) { throw new PostValidationException("EInterpreter stopped before execution because of failing Post-validation"); }
    }

    private void RunEngine(ETree tree)
    {
        var engine = new Engine.Engine(tree);

        try
        {
            engine.Run();
        }
        catch (Exception ex)
        {
            if (ex is System.Reflection.TargetInvocationException)
            {
                throw new EngineException($"Runtime error: {ex?.InnerException?.Message ?? "<<no message>>"}");
            }
            else
            {
                throw new EngineException($"Runtime error: {ex.Message}");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"{Name} ran for {engine.Duration.LargestUnit()} and returned {engine.Result}");
    }
}
