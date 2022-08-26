// number

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		new Number someNumber;
		new Number someOtherNumber;
		
		someNumber = 42;
		Console:WriteLine(someNumber);

		someOtherNumber = Number:Add(someNumber, 42);
		Console:WriteLine(someNumber);
		Console:WriteLine(someOtherNumber);

		someOtherNumber = Number:Subtract(someOtherNumber, 21);
		Console:WriteLine(someOtherNumber);
	
		return true;
	}
}
