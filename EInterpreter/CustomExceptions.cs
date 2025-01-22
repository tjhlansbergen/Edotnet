namespace EInterpreter;

public class ParserException(string message) : Exception(message)
{
}

public class EngineException(string message) : Exception(message)
{
}

class PreValidationException(string message) : Exception(message)
{
}

class LexerException(string message) : Exception(message)
{
}

class PostValidationException(string message) : Exception(message)
{
}
