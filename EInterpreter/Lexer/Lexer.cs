using EInterpreter.EObjects;
using System.Collections.Generic;

namespace EInterpreter.Lexer
{

    /// <summary>
    /// The lexer is responsible for tokenizing and parsing E source code
    /// </summary>
    public class Lexer
    {
        public List<EToken> Tokens { get; private set; }

        /// <summary>
        /// Create a Abstract Source Tree out lines of E source code
        /// </summary>
        /// <param name="lines">Order array of lines of E source code</param>
        /// <returns></returns>
        public ETree GetTree(string[] lines)
        {
            // tokenize
            Tokens = new Tokenizer().Tokenize(lines);

            // parse
            var parser = new Parser();
            var tree = parser.Parse(Tokens);

            return tree;
        }
    }
}
