// Hello World

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		new Text lineToWrite;

		lineToWrite = "Hello World";
		Console:WriteLine(lineToWrite);

		
		Text:Append(lineToWrite, "!");
		Console:WriteLine(lineToWrite);

		return true;
	}
}
