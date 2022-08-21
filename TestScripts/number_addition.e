// number

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		new Number someNumber;
		
		someNumber = 42;
		Console:WriteNumber(someNumber);

		Number:Add(someNumber, 42);
		Console:WriteNumber(someNumber);

		Number:Subtract(someNumber, 21);
		Console:WriteNumber(someNumber);
	
		return true;
	}
}
