// number

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		new Number someNumber;
		new Number someOtherNumber;
		
		someNumber = 42;

		someOtherNumber = Number:Multiply(someNumber, 10);
		Console:WriteNumber(someNumber);
		Console:WriteNumber(someOtherNumber);

		someOtherNumber = Number:Divide(someNumber, 7);
		Console:WriteNumber(someNumber);
		Console:WriteNumber(someOtherNumber);

		someOtherNumber = Number:Remainder(someNumber, 13);
		Console:WriteNumber(someNumber);
		Console:WriteNumber(someOtherNumber);

		return true;
	}
}
