

// Hello World List

Utility Program
{
	Function Boolean Start(Text arguments)
	{

        new List<Number> numbers;
        new Number count;
        count = 0;

        while(Number:LessThen(count, 10))
        {
            List:Add(numbers, count);
            count = Number:Add(count, 1);
        }

		Console:WriteLine(List:Count(numbers));
        Console:WriteLine("");

        foreach(number in numbers)
        {
            Console:WriteLine(number);
        }

		return true;
	}
}
