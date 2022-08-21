namespace EInterpreter.Lexer
{
    public class EToken
    {
        public int LineNumber { get; }
        public ETokenType Type { get; }
        public string Line { get; set; }

        public EToken(int linenr, ETokenType type, string line = "")
        {
            LineNumber = linenr;
            Type = type;
            Line = line;
        }

        public override string ToString()
        {
            return $"{LineNumber}\t{Type}\t{Line}";
        }
    }
}
