using EInterpreter.EElements;
using System.Diagnostics;
using EBuildIn;
using EInterpreter.Lexing;

namespace EInterpreter.Engine;

public class Engine
{
    public TimeSpan Duration { get; private set; }
    public bool Result { get; private set; }

    private readonly ETree _tree;
    private readonly List<Variable> _stack = [];

    public Engine(ETree tree)
    {
        _tree = tree;
    }

    public void Run()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Restart();

        _run();

        stopwatch.Stop();
        Duration = stopwatch.Elapsed;
    }

    private void _run()
    {
        // throw all constants on the stack
        var globals = _tree.Constants.Select(_expandConstant);
        _stack.AddRange(globals);

        //TODO get args
        var args = new List<Variable>();

        // find and run the singular Start Function, in the singular Program Utility
        var entrypoint = _tree.Utilities
            .Single(u => u.Name == "Program").Functions
            .Single(f => f.Name == "Program.Start");
        Result = (bool)(_runBlock(entrypoint, args).Value ?? false);
    }

    private Variable _runBlock(IRunnableBlock block, List<Variable> variables)
    {
        var scope = block.Name;
        variables.ForEach(v => v.Scope = scope);
        _stack.AddRange(variables);

        foreach (var element in block.Elements)
        {
            switch (element)
            {
                case EFunctionCall functionCall:
                    {
                        var result = _handleFunctionCall(functionCall);
                        result.Scope = scope;
                        _stack.Add(result);
                    }
                    break;
                case EDeclaration declaration:
                    {
                        _handleDeclaration(declaration, scope);
                    }
                    break;
                case EReturn returnStatement:
                    {
                        var result = _expandParameter(returnStatement.Parameter);
                        _stack.RemoveAll(v => v.Scope == scope);
                        return result;
                    }
                case EStatement statement:
                    {
                        var result = _handleStatement(statement, scope);
                        if (!result.IsEmpty)
                        {
                            _stack.RemoveAll(v => v.Scope == scope);
                            return result;
                        }
                    }
                    break;
                case EAssignment assignment:
                    {
                        _handleAssignment(assignment, scope);
                    }
                    break;
            }
        }

        _stack.RemoveAll(v => v.Scope == scope);

        if (block is EStatement)
        {
            // statements can finish without returning something, we return a dummy that's get ignored by the parent/caller
            return Variable.Empty;
        }
        else
        {
            // by now, we should have come across at least one return statement, if not that's an error
            throw new EngineException($"Function {block.Name} exited without return statement.");
        }
    }

    private void _handleDeclaration(EDeclaration declaration, string scope)
    {
        _stack.Add(_getVariable(declaration, scope));
    }

    private Variable _getVariable(EDeclaration declaration, string scope)
    {
        if (Enum.TryParse<Types>(declaration.Prop.Type, out Types type))
        {
            // simple (build-in) object

            // check for subtypes
            var subTypes = new List<string>();
            foreach (var sb in declaration.SubTypes)
            {
                // test if it is either a build-in or a user object
                if (!Enum.TryParse<Types>(sb, out _) && !_tree.Objects.Any(o => o.Name == sb))
                {
                    throw new EngineException($"Declaration with unknown type: {declaration.Prop.Type}");
                }
                subTypes.Add(sb);
            }

            var var = new Variable(type, subTypes, null, scope);
            var.Name = declaration.Name;

            return var;
        }


        if (_tree.Objects.Any(o => o.Name == declaration.Prop.Type))
        {
            // user object 
            var parsedObject = _tree.Objects.Single(o => o.Name == declaration.Prop.Type);

            var userObject = new List<Variable>();
            foreach (var objectProperty in parsedObject.Properties)
            {
                var objectMember = _getVariable(objectProperty, scope);
                userObject.Add(objectMember);
            }

            var var = new Variable(Types.Object, subTypes: new string[] { parsedObject.Name }, value: userObject, scope);
            var.Name = declaration.Name;

            return var;
        }

        throw new EngineException($"Declaration with unknown type: {declaration.Prop.Type}");
    }

    private void _handleAssignment(EAssignment assignment, string scope)
    {
        var result = _expandParameter(assignment.Parameter);

        // test for local variable first
        var existingVariable = _stack.SingleOrDefault(v => v.Name == assignment.Name);
        if (existingVariable != null)
        {
            if (existingVariable.Type != result.Type)
            {
                // don't assign when types do not match
                throw new EngineException($"Cannot assign value of type {result.Type} to variable of type {existingVariable.Type}");
            }

            existingVariable.Value = result.Value;
            return;
        }

        // not a local variable, user object maybe?
        var parts = assignment.Name.SplitClean('.');
        
        if (parts.Length == 2 && _stack.Any(v => v.Type == Types.Object && v.Name == parts[0]))
        {
            var userObject = _stack.Single(v => v.Type == Types.Object && v.Name == parts[0]);
            var objectVariable = (userObject.Value as List<Variable> ?? []).SingleOrDefault(p => p.Name == parts[1]);
            
            if(objectVariable == null)
            {
                throw new EngineException($"Attempt to assign value with name {assignment.Name} to non-existing property of object: {userObject.Name}");        
            }
            if(objectVariable.Type != result.Type)
            {
                // don't assign when types do not match
                throw new EngineException($"Cannot assign value of type {result.Type} to variable of type {objectVariable.Type} in object {userObject.Name}");
            }

            objectVariable.Value = result.Value;
            return;
        }

        // don't add to non existing variable, that would be weak typing
        throw new EngineException($"Attempt to assign value to non-existing variable: {assignment.Name}");
    }

    private Variable _handleStatement(EStatement statement, string scope)
    {
        switch (statement.Type)
        {
            case EStatementType.FOREACH:
                return _handleLoop(statement, scope);
            case EStatementType.IF:
            case EStatementType.WHILE:
            default:
                return _handleEvaluable(statement, scope);

        }
    }

    private Variable _handleEvaluable(EStatement statement, string scope)
    {
        // evaluate
        var evaluation = _expandParameter(statement.Body);

        if (evaluation.Type != Types.Boolean) throw new EngineException($"Variable for {statement.Type} statement in {scope} is of type {evaluation.Type}, but a {Types.Boolean} was expected");
        if (evaluation.Value == null) throw new EngineException($"Variable for {statement.Type} statement in {scope} is unassigned");

        if ((bool)evaluation.Value != true)
        {
            return Variable.Empty;
        }

        // call _runBlock, as if running a function, but empty set of parameters
        // the return value will be empty, if the statement completed, signaling we can continue,
        // or has a value, signaling we encountered a return inside the statement.
        var result = _runBlock(statement, new List<Variable>());

        // only continue the while if result is empty (meaning a return was not encountered)
        if (statement.Type == EStatementType.WHILE && result.IsEmpty)
        {
            _handleEvaluable(statement, scope);
        }

        return result;
    }

    private Variable _handleLoop(EStatement statement, string scope)
    {
        // validate body
        var parts = statement.Body.SplitClean(' ');
        if (parts.Length != 3 || parts[1] != "in") throw new EngineException($"Body of {statement.Type} statement in {scope} is unparsable");

        // inspect the body
        var list = _expandParameter(parts[2]);
        if (list.Type != Types.List) throw new EngineException($"Variable {parts[2]} is used in a foreach loop but is not a list");

        // see if there are items in the list to iterate
        if (list.Value == null || !((List<Variable>)list.Value).Any()) { return Variable.Empty; }

        // run the loop, a non-empty variable signals a return
        foreach (var item in (List<Variable>)list.Value)
        {
            Variable result;
            if (Enum.TryParse<Types>(list.SubTypes?.First(), out Types type))
            {
                // list of simple type
                result = _runBlock(statement, new List<Variable> { new Variable(parts[0], list.Type, item.Value) });
            }
            else
            {
                // list of object
                var var = new Variable(Types.Object, subTypes: list.SubTypes ?? [], item.Value)
                {
                    Name = parts[0]
                };
                result = _runBlock(statement, [var]);
            }

            // TODO list of list?
            
            if (!result.IsEmpty) return result;
        }

        return Variable.Empty;
    }

    private Variable _handleFunctionCall(EFunctionCall call)
    {
        var parameters = call.Parameters.Select(_expandParameter).ToList();

        // try as non-build-in function first, this way the user can hide build-in functions if desired
        var localFunction = _tree.Functions.SingleOrDefault(f => f.Name == call.FullName);


        // if we found a matching function, and it's parameters match, then run it
        if (localFunction != null && EngineHelpers.MatchAndNameParameters(parameters, localFunction))
        {
            return _runBlock(localFunction, parameters.ToList());
        }

        // check if we have a build-in function, and get it's parameters
        var targetParameters = EBuildIn.Modules.FindFunctionAndReturnParameters(call.Parent, call.Name);

        if (targetParameters != null && EngineHelpers.MatchParameters(parameters, targetParameters))
        {
            // we have match for a build in function, run it
            return Modules.Run(call.Parent, call.Name, parameters.ToArray());
        }

        throw new EngineException($"Invalid function call: {call.Parent}:{call.Name}");
    }



    private Variable _expandParameter(string parameter)
    {
        // determine the type step by step

        // is it true/false?
        if (bool.TryParse(parameter, out bool boolean)) { return new Variable(Types.Boolean, boolean); }

        // is it a literal double?
        if (double.TryParse(parameter, out double number)) { return new Variable(Types.Number, number); }

        // is it a string-literal
        if (parameter.StartsWith("\"") && parameter.EndsWith("\"")) { return new Variable(Types.Text, parameter.Replace("\"", "")); }

        // we don't support inline-lists (yet), no need to check for that

        // not some literal value, it can be a variable
        if (_stack.Exists(v => v.Name == parameter)) 
	{
		var v = _stack.Single(v => v.Name == parameter);
		return v; // should be returning a copy no?
       	}


        // not a local variable, an object property maybe?
        var parts = parameter.SplitClean('.');
        if (parts.Length == 2 && _stack.Any(v => v.Type == Types.Object && v.Name == parts[0]))
        {
            var userObjectVariables = _stack.Single(v => v.Type == Types.Object && v.Name == parts[0]).Value as List<Variable>;
            if (userObjectVariables?.Exists(p => p.Name == parts[1]) ?? false) { return userObjectVariables.Single(p => p.Name == parts[1]); }
        }

        // also not a variable, a function call then? if so call it inline and return its return value
        EFunctionCall? call;
        try { call = Parsers.ParseFunctionCall(parameter); }
        catch { call = null; }
        if (call != null) { return _handleFunctionCall(call); }

        // ran out of options
        throw new EngineException($"Invalid parameter: {parameter}");
    }

    private Variable _expandConstant(EConstant constant)
    {
        var variable = _expandParameter(constant.Value);
        variable.Name = constant.Name;

        if (variable.Type.ToString() != constant.Prop.Type) { throw new ParserException($"Type mismatch in constant: {constant.Name}"); }

        return variable;
    }
}
