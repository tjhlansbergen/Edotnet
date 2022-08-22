// Hello World

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		new Text lineToWrite;
		
		lineToWrite = "Hello World!";		

		Console:WriteText(lineToWrite);

		// part two
		new Number someNumber;
		someNumber = 42;

		Console:WriteNumber(someNumber);

		new Number someOtherNumber;
		someOtherNumber = someNumber;

		Console:WriteNumber(someOtherNumber);

		return true;
	}
}