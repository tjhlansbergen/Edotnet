using Newtonsoft.Json.Linq;

namespace EBuildIn
{
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

            var jsonstring = (string)json.Value;
            var pathstring = (string)path.Value;

            JToken? token;

            try
            {
                if (jsonstring.StartsWith("["))
                {
                    JArray arr = JArray.Parse(jsonstring);
                    token = arr.SelectToken(pathstring);
                }
                else    // try as object
                {
                    JObject obj = JObject.Parse(jsonstring);
                    token = obj.SelectToken(pathstring);
                }
            }
            catch
            {
                token = null;
            }

            if (token == null)
            {
                return Variable.Empty;
            }

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

        // TODO: multi select with SelectTokens, but that needs some sort of a multi-type collection (a set?)
    }
}