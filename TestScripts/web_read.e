// Web Read

Utility Program
{
	Function Boolean Start(Text arguments)
	{
        new Text content;
        new List<Text> splits;
        new Text result;
        

        content = Web:Read("https://data.buienradar.nl/2.0/feed/json")
        splits = Text:Split(content, "summary");
        splits = Text:Split(List:Get(splits, 1), "\r\n")
        result = List:Get(splits, 0);

		Console:WriteLine(result);
		return true;
	}
}
