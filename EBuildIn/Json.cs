using Newtonsoft.Json.Linq;

namespace EBuildIn;

public static class Json
{
    public static List<Types> SerializeParameters => new List<Types> { Types.T };
    public static Variable Serialize(Variable obj)
    {
        // TODO (this should serialize any variable into JSON
        return Variable.Empty;
    }

    public static List<Types> SelectParameters => new List<Types> { Types.Text, Types.Text };
    public static Variable Select(Variable json, Variable path)
    {
        if (json.Value == null
        || path.Value == null
        || string.IsNullOrWhiteSpace((string)json.Value)
        || string.IsNullOrWhiteSpace((string)path.Value))
        {
            return Variable.Empty;
        }

        JToken? token;

        try
        {
            if (((string)json.Value).StartsWith("["))
            {
                token = JArray.Parse((string)json.Value)?.SelectToken((string)path.Value);
            }
            else    // try as object
            {
                token = JObject.Parse((string)json.Value)?.SelectToken((string)path.Value);
            }
        }
        catch
        {
            token = null;
        }

        if (token == null) { return Variable.Empty; }
        return _variableForToken(token);
    }

    // TODO: multi select with SelectTokens, but that needs some sort of a multi-type collection (a set?)

    private static Variable _variableForToken(JToken token)
    {
        switch (token.Type)
        {
            case JTokenType.Integer:
            case JTokenType.Float:
                return new Variable(Types.Number, (double)token);
            case JTokenType.Boolean:
                return new Variable(Types.Boolean, (bool)token);
            default:
                return new Variable(Types.Text, token.ToString());
        }
    }
}