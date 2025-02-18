// Web Read

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		new Text content;
        	new Text forecast;
        	new List<Text> fivedays;
        
        	content = Web:Read("https://data.buienradar.nl/2.0/feed/json");
        	forecast = Json:Select(content, "$.forecast.shortterm.forecast");

		Console:WriteLine("Weerbericht:");
		Console:WriteLine(forecast);
		Console:WriteLine("");

		new Text day0;
		Text:Append(day0, "0: ");
		Text:Append(day0, Json:Select(content, "$.forecast.fivedayforecast[0].maxtemperature"));
		List:Add(fivedays, day0);
		
		new Text day1;
		Text:Append(day1, "1: ");
		Text:Append(day1, Json:Select(content, "$.forecast.fivedayforecast[1].maxtemperature"));
		List:Add(fivedays, day1);
		
		new Text day2;
		Text:Append(day2, "2: ");
		Text:Append(day2, Json:Select(content, "$.forecast.fivedayforecast[2].maxtemperature"));
		List:Add(fivedays, day2);
		
		new Text day3;
		Text:Append(day3, "3: ");
		Text:Append(day3, Json:Select(content, "$.forecast.fivedayforecast[3].maxtemperature"));
		List:Add(fivedays, day3);
		
 		new Text day4;
		Text:Append(day4, "4: ");
		Text:Append(day4, Json:Select(content, "$.forecast.fivedayforecast[4].maxtemperature"));
		List:Add(fivedays, day4);
		
		foreach(day in fivedays)
		{
			Console:WriteLine(day);
		}
		
		return true;
	}
}
