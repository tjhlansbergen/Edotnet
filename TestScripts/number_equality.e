// number

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		new Number someNumber;
		someNumber = 42;

		new Number someOtherNumber;
		someOtherNumber = 21;

		new Boolean equal;

		equal = Number:AreEqual(someNumber, someOtherNumber);
		Console:WriteLine(equal);

		someOtherNumber = 42;
		equal = Number:AreEqual(someNumber, someOtherNumber);
		Console:WriteLine(equal);

		if(equal)
		{
			Console:WriteLine("Hello World!");
		}

		return true;
	}
}
