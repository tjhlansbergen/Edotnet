using System.Collections.Generic;
using System.Linq;
using EInterpreter.EElements;
using EInterpreter.EObjects;


namespace EInterpreter.Lexer
{
    /// <summary>
    /// Responsible for parsing a list of tokens into an Abstract Source Tree
    /// </summary>
    public class Parser
    {
        private Stack<EElement> _callStack = new Stack<EElement>();

        /// <summary>
        /// Build a ETree out of a list of ETokens
        /// </summary>
        /// <param name="tokens">Unordered list of ETokens</param>
        /// <returns></returns>
        public ETree Parse(IEnumerable<EToken> tokens)
        {
            var tree = new ETree();

            var orderedTokens = tokens.OrderBy(t => t.LineNumber).ToList();

            foreach (var token in orderedTokens)
            {
                _processToken(token, tree);
            }

            return tree;
        }

        private string _getNamespac()
        {
            return _callStack.Any() ? string.Join(".", _callStack.Select(s => s.Name)) + "." : string.Empty;
        }

        private void _processToken(EToken token, ETree tree)
        {
            switch (token.Type)
            {
                case ETokenType.WHITESPACE:
                case ETokenType.COMMENT:
                case ETokenType.OPEN:
                    // do nothing
                    break;
                case ETokenType.CLOSE:
                    _handleClose();
                    break;
                case ETokenType.CONSTANT:
                    _handleConstant(token, tree);
                    break;
                case ETokenType.OBJECT:
                    _handleObject(token, tree);
                    break;
                case ETokenType.PROPERTY:
                    _handleProperty(token);
                    break;
                case ETokenType.UTILITY:
                    _handleUtility(token, tree);
                    break;
                case ETokenType.FUNCTION:
                    _handleFunction(token);
                    break;
                case ETokenType.FUNCTION_STATEMENT:
                    _handleStatement(token);
                    break;
                case ETokenType.DECLARATION:
                    _handleDeclaration(token);
                    break;
                case ETokenType.FUNCTION_CALL:
                    _handleFunctionCall(token);
                    break;
                case ETokenType.FUNCTION_RETURN:
                    _handleFunctionReturn(token);
                    break;
                case ETokenType.ASSIGNMENT:
                    _handleAssignment(token);
                    break;
                default:
                    throw new ParserException($"Unhandled token type {token.Type} at line: {token.LineNumber}");
            }
        }

        private void _handleClose()
        {
            if (_callStack.Any()) _callStack.Pop();
        }

        private void _handleConstant(EToken token, ETree tree)
        {
            EConstant constant;

            try { constant = Parsers.ParseConstant(token.Line); }
            catch { throw new ParserException(_unparsebleMessage("constant", token.LineNumber)); }

            tree.Constants.Add(constant);
        }

        private void _handleObject(EToken token, ETree tree)
        {
            EObject objct;

            try { objct = Parsers.ParseObject(_getNamespac(), token.Line); }
            catch { throw new ParserException(_unparsebleMessage("object", token.LineNumber)); }

            tree.Objects.Add(objct);
            _callStack.Push(objct);
        }

        private void _handleUtility(EToken token, ETree tree)
        {
            EUtility utility;

            try { utility = Parsers.ParseUtility(_getNamespac(), token.Line); }
            catch { throw new ParserException(_unparsebleMessage("utility", token.LineNumber)); }

            tree.Utilities.Add(utility);
            _callStack.Push(utility);
        }

        private void _handleFunction(EToken token)
        {
            if (_callStack.Any() && _callStack.Peek() is EUtility util)
            {
                EFunction function;

                try { function = Parsers.ParseFunction(_getNamespac(), token.Line); }
                catch { throw new ParserException(_unparsebleMessage("function", token.LineNumber)); }

                util.Functions.Add(function);
                _callStack.Push(function);
            }
            else
            {
                throw new ParserException(_unexpectedMessage("function", token.LineNumber));
            }
        }

        private void _handleStatement(EToken token)
        {
            if (_callStack.Any() && (_callStack.Peek() is EFunction || _callStack.Peek() is EStatement))
            {
                EStatement statement;

                try { statement = Parsers.ParseStatement(token.Line); }
                catch { throw new ParserException(_unparsebleMessage("statement", token.LineNumber)); }

                if (_callStack.Peek() is EFunction func)
                {
                    func.Elements.Add(statement);
                }
                else if (_callStack.Peek() is EStatement stat)
                {
                    stat.Elements.Add(statement);
                }
                _callStack.Push(statement);
            }
            else
            {
                throw new ParserException(_unexpectedMessage("statement", token.LineNumber));
            }
        }

        private void _handleDeclaration(EToken token)
        {
            if (_callStack.Any() && (_callStack.Peek() is EFunction || _callStack.Peek() is EStatement))
            {
                EDeclaration init;

                try { init = Parsers.ParseDeclaration(token.Line); }
                catch { throw new ParserException(_unparsebleMessage("object declaration", token.LineNumber)); }

                if (_callStack.Peek() is EFunction func)
                {
                    func.Elements.Add(init);
                }
                else if (_callStack.Peek() is EStatement stat)
                {
                    stat.Elements.Add(init);
                }
            }
            else
            {
                throw new ParserException(_unexpectedMessage("object declaration", token.LineNumber));
            }
        }


        private void _handleAssignment(EToken token)
        {
            if (_callStack.Any() && (_callStack.Peek() is EFunction || _callStack.Peek() is EStatement))
            {
                EAssignment assign;

                try { assign = Parsers.ParseAssignment(token.Line); }
                catch { throw new ParserException(_unparsebleMessage("assignment", token.LineNumber)); }

                if (_callStack.Peek() is EFunction func)
                {
                    func.Elements.Add(assign);
                }
                else if (_callStack.Peek() is EStatement stat)
                {
                    stat.Elements.Add(assign);
                }
            }
            else
            {
                throw new ParserException(_unexpectedMessage("assignment", token.LineNumber));
            }
        }

        private void _handleFunctionCall(EToken token)
        {
            if (_callStack.Any() && (_callStack.Peek() is EFunction || _callStack.Peek() is EStatement))
            {
                EFunctionCall call;

                try { call = Parsers.ParseFunctionCall(token.Line); }
                catch { throw new ParserException(_unparsebleMessage("function call", token.LineNumber)); }

                if (_callStack.Peek() is EFunction func)
                {
                    func.Elements.Add(call);
                }
                else if (_callStack.Peek() is EStatement stat)
                {
                    stat.Elements.Add(call);
                }
            }
            else
            {
                throw new ParserException(_unexpectedMessage("function call", token.LineNumber));
            }
        }

        private void _handleFunctionReturn(EToken token)
        {
            if (_callStack.Any() && (_callStack.Peek() is EFunction || _callStack.Peek() is EStatement))
            {
                EReturn retn;

                try { retn = Parsers.ParseReturnStatement(token.Line); }
                catch { throw new ParserException(_unparsebleMessage("return statement", token.LineNumber)); }

                if (_callStack.Peek() is EFunction func)
                {
                    func.Elements.Add(retn);
                }
                else if (_callStack.Peek() is EStatement stat)
                {
                    stat.Elements.Add(retn);
                }
            }
            else
            {
                throw new ParserException(_unexpectedMessage("return statement", token.LineNumber));
            }
        }

        private void _handleProperty(EToken token)
        {
            if (_callStack.Any() && _callStack.Peek() is EObject obj)
            {
                EProperty prop;

                try { prop = Parsers.ParseProperty(token.Line); }
                catch { throw new ParserException(_unparsebleMessage("property", token.LineNumber)); }

                obj.Properties.Add(prop);
            }
            else
            {
                throw new ParserException(_unexpectedMessage("property", token.LineNumber));
            }
        }

        private static string _unparsebleMessage(string name, int linenr)
        {
            return $"Parse Error: Unparsable {name} at line: {linenr}";
        }

        private static string _unexpectedMessage(string name, int linenr)
        {
            return $"Parse Error: Unexpected {name} at line: {linenr}";
        }


    }
}
