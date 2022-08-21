using System;
using System.Collections.Generic;
using System.IO;
using EInterpreter.EObjects;
using EInterpreter.Validation;

namespace EInterpreter
{
    public class Worker
    {
        public bool Verbose { get; set; } = true;

        private string _name;
        private string[] _lines;
        private ETree _tree;

        public Worker(TextWriter outputChannel = null)
        {
            // divert output if requested
            if (outputChannel != null) Console.SetOut(outputChannel);
        }

        public void Go(string[] lines, string name)
        {
            _tree = null;
            _lines = lines;
            _name = name;

            try
            {
                _preValidate();
                _lex();
                _postValidate();
                _runEngine();
            }
            catch (Exception ex)
            {
                Extensions.WriteColoredLine(ex.Message, ConsoleColor.Red);
            }
        }

        private void _preValidate()
        {
            Extensions.WriteColoredLine("Pre-validation: ", ConsoleColor.DarkCyan);

            var validator = new PreValidator(new List<IPreValidationStep>
            {
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
            });
                
            var preValidationResult = validator.Validate(_lines);

            if (Verbose)
            {
                foreach (var validationStepResult in validator.Results)
                {
                    Extensions.WriteColoredLine(" - " + validationStepResult.Output, validationStepResult.Valid ? ConsoleColor.DarkGreen : ConsoleColor.Red);
                }
            }

            Console.WriteLine();
            Console.WriteLine(preValidationResult ? $"Pre-validation for `{_name}` successful" : $"Pre-validation for {_name} failed!");
            Console.WriteLine();
            if(preValidationResult == false) { throw new PreValidationException("EInterpreter stopped before execution because of failing Pre-validation");}
        }

        private void _lex()
        {
            Extensions.WriteColoredLine("Lexing: ", ConsoleColor.DarkCyan);
            _tree = new Lexer.Lexer().GetTree(_lines);
            if (_tree != null)
            {
                Console.WriteLine(_tree.Summarize());
                return;
            }
            
            Extensions.WriteColoredLine("The parser did not return an object tree!", ConsoleColor.Red);
            throw new LexerException("EInterpreter stopped before execution because of a lexer exception");
        }

        private void _postValidate()
        {
            Extensions.WriteColoredLine("Post-validation: ", ConsoleColor.DarkCyan);
            var validator = new PostValidator(new List<IPostValidationStep>
            {
                new GlobalIdentifiersAreUnique(),
                new ObjectIdentifiersAreUnique(),
                new UtilityIdentifiersAreUnique()
            });
            
            var postValidationResult = validator.Validate(_tree);

            if (Verbose)
            {
                foreach (var validationStepResult in validator.Results)
                {
                    Extensions.WriteColoredLine(" - " + validationStepResult.Output, validationStepResult.Valid ? ConsoleColor.DarkGreen : ConsoleColor.Red);
                }
            }

            Console.WriteLine();
            Console.WriteLine(postValidationResult ? $"Post-validation for `{_name}` successful" : $"Post-validation for {_name} failed!");
            Console.WriteLine();
            if(postValidationResult == false) { throw new PostValidationException("EInterpreter stopped before execution because of failing Post-validation"); }
        }

        private void _runEngine()
        {
            var engine = new Engine.Engine();

            try
            {
                engine.Run(_tree);
            }
            catch (Exception ex)
            {
                throw new EngineException($"Runtime error: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine($"{_name} ran for {engine.Duration.LargestUnit()} and returned {engine.Result}");
        }
    }
}
