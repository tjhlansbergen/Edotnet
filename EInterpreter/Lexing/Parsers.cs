﻿using EInterpreter.EElements;

namespace EInterpreter.Lexing;

public static class Parsers
{
    public static EConstant ParseConstant(string line)
    {
        if (line.StartsWith("Constant"))
        {
            line = line.Remove(0, "Constant".Length);
        }
        var left = line.SplitClean('=')[0].Trim();
        var right = line.SplitClean('=')[1].Trim();
        

        return new EConstant(left.SplitClean(' ')[0], left.SplitClean(' ')[1], right.Split(';')[0]);
    }

    public static EObject ParseObject(string line)
    {
        var lineArr = line.SplitClean(' ');

        if(lineArr.Length != 2 || lineArr[0] != "Object") { throw new ParserException("Unparsable object"); }

        return new EObject(lineArr[1]);
    }

    public static EUtility ParseUtility(string namespac, string line)
    {
        var lineArr = line.SplitClean(' ');

        if (lineArr.Length != 2 || lineArr[0] != "Utility") { throw new ParserException("Unparsable utility"); }

        return new EUtility(namespac + lineArr[1]);
    }

    public static EFunction ParseFunction(string namespac, string line)
    {
        if(!line.Contains("(") || !line.Contains(")")) { throw new ParserException("Unparsable function"); }

        line = line.SplitClean(')')[0];
        var left = line.SplitClean('(')[0].SplitClean(' ');

        if(left.Length != 3 || left[0] != "Function") { throw new ParserException("Unparsable function"); }

        var parameters = new List<EProperty>();

        if (line.SplitClean('(').Length  > 1)
        {
            foreach (var p in line.SplitClean('(')[1].SplitClean(','))
            {
                parameters.Add(ParseProperty(p));
            }
        }
        return new EFunction(left[1], namespac + left[2], parameters);
    }

    public static EProperty ParseProperty(string line)
    {
        var lineArr = line.SplitClean(' ');

        if(lineArr.Length != 2) { throw new ParserException("Unparsable property"); }

        return new EProperty(lineArr[0], lineArr[1]);
    }

    public static EStatement ParseStatement(string line)
    {
        var lineArr = line.Substring(0, line.Length - 1).SplitClean('(', 2);

        if(lineArr.Length != 2 || !Enum.TryParse<EStatementType>(lineArr[0], true, out EStatementType type)) { throw new ParserException("Unparsable statement"); }
        
        return new EStatement(Guid.NewGuid().ToString(), type, lineArr[1]); 
    }

    public static EDeclaration ParseDeclaration(string line)
    {
        var lineArr = line.SplitClean(';')[0].SplitClean(' ');

        if(lineArr.Length != 3 || (lineArr[0] != "new" && lineArr[0] != "Property")) { throw new ParserException("Unparsable declaration");}

        // check for subtypes
        if (lineArr[1].Contains("<") && lineArr[1].Contains(">"))
        {
            var parts = lineArr[1].SplitClean('>')[0].SplitClean('<');
            return new EDeclaration(parts[0], lineArr[2], parts[1].SplitClean(','));
        }
        else
        {
            // no subtypes
            return new EDeclaration(lineArr[1], lineArr[2]);
        }
    }

    public static EFunctionCall ParseFunctionCall(string line)
    {
        if(!line.Contains(":") || !line.Contains("(") || !line.Contains(")")) { throw new ParserException("Unparseble function call"); }

        var lineArr = line.SplitClean(':', 2);

        if (lineArr.Length < 2) { throw new ParserException("Unparsable function call"); }

        var left = lineArr[0];
        var right = lineArr[1];

        var rightArr = right.SplitClean('(', 2);
        var funcName = rightArr[0];
        string parameterString;
        if (rightArr[1] == ")")
        {
            parameterString = string.Empty;
        }
        else
        {
            parameterString = rightArr[1].Replace(';', ' ').Trim();
            parameterString = parameterString.Substring(0, parameterString.Length - 1);
        }
        
        var parameters = new List<string>();

        if (parameterString.Length > 0)
        {
            // commas are a difficult case here, they may denote multiple parameters, but could be part of a chained function call
            // ideally we should tokenize such chained calls, for now we traverse them at runtime
            // ugly hack: find commas that are not part of a chained call, split on their indiced
            // note that we also have to take quotes into account...
            var indices = GetRealCommas(parameterString);
            
            foreach (var p in parameterString.SplitAt(indices))
            {
                parameters.Add(p.Trim());
            }
        }

        return new EFunctionCall(left, funcName, parameters);
    }

    public static EReturn ParseReturnStatement(string line)
    {
        if(!line.StartsWith("return ") || !line.Contains(";") || line.SplitClean(' ').Length < 2 || line.Contains("=")) { throw new ParserException("Unparseble return statement"); }

        var parameter = line.SplitClean(';')[0].Remove(0, "return ".Length);
        return new EReturn("", parameter);
    }

    public static EAssignment ParseAssignment(string line)
    {
        if(!line.Contains("=")) { throw new ParserException("Unparseble assignment"); }
        return new EAssignment(line.SplitClean('=')[0].Trim(), line.SplitClean('=')[1].SplitClean(';')[0].Trim());
    }

    // Gets indices of all commas that are NOT wrapped in doublequotes or round brackets
    public static int[] GetRealCommas(string input)
    {
        bool inQuotes = false, inBrackets = false;
        var count = 0;
        var result = new List<int>();

        foreach (var c in input)
        {
            if (c == '"') { inQuotes = !inQuotes; }
            if (c == '(' && !inQuotes) { inBrackets = true; }
            if (c == ')' && !inQuotes) { inBrackets = false; }
            if (c == ',' && !inQuotes && !inBrackets) { result.Add(count); }
            count++;
        }

        return result.ToArray();
    }
}
