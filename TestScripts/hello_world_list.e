// Hello World List

Utility Program
{
	Function Boolean Start(Text arguments)
	{

        new List<Number> numbers;

		List:Add(numbers, 1);

		Console:WriteLine(List:First(numbers));

		List:Add(numbers, 2);

		// zero indexed!
		//new Number result;
		//result = List:Get(numbers, 1);
		Console:WriteLine(List:Get(numbers, 1));	

		return true;
	}
}
