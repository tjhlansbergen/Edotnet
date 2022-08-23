// number

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		new Number someNumber;
		new Number someOtherNumber;
		
		someNumber = 42;
		Console:WriteNumber(someNumber);

		someOtherNumber = Number:Add(someNumber, 42);
		Console:WriteNumber(someNumber);
		Console:WriteNumber(someOtherNumber);

		someOtherNumber = Number:Subtract(someOtherNumber, 21);
		Console:WriteNumber(someOtherNumber);
	
		return true;
	}
}
