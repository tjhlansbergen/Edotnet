// number

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		new Number someNumber;
		
		someNumber = 42;
		Console:WriteNumber(someNumber);

		Number:Multiply(someNumber, 10);
		Console:WriteNumber(someNumber);

		Number:Divide(someNumber, 7);
		Console:WriteNumber(someNumber);

		Number:Remainder(someNumber, 13);
		Console:WriteNumber(someNumber);

		return true;
	}
}
