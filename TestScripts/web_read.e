// Web Read

Utility Program
{
	Function Boolean Start(Text arguments)
	{
        new Text content;
        new Text result;
        
        content = Web:Read("https://data.buienradar.nl/2.0/feed/json")
        result = Json:Select(content, "$.forecast.shortterm.forecast");

		Console:WriteLine(result);
		return true;
	}
}
