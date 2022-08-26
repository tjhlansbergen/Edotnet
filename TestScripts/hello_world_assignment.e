// Hello World

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		new Text lineToWrite;
		
		lineToWrite = "Hello World!";		

		Console:WriteLine(lineToWrite);

		// part two
		new Number someNumber;
		someNumber = 42;

		Console:WriteLine(someNumber);

		new Number someOtherNumber;
		someOtherNumber = someNumber;

		Console:WriteLine(someOtherNumber);

		return true;
	}
}