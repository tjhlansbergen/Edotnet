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
		Console:WriteBoolean(equal);

		someOtherNumber = 42;
		equal = Number:AreEqual(someNumber, someOtherNumber);
		Console:WriteBoolean(equal);

		if(equal)
		{
			Console:WriteText("Hello World!");
		}

		return true;
	}
}
