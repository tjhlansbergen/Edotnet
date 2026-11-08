// Display system and UTC time

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		new Text systemDateTime;
		systemDateTime = Date:System();

		Text:Append(systemDateTime, " ");
		Text:Append(systemDateTime, Time:System());

		Console:WriteLine("System date & time:");
		Console:WriteLine(systemDateTime);

		Console:WriteLine("");

		Console:WriteLine("Utc now:");
		Console:WriteLine(Time:Utc());

		return true;
	}
}
