// Fizz Buzz

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		Program:FizzBuzz();
		
		return true;
	}

	Function Boolean FizzBuzz()
	{
		new Number count;	
		count = 0;		

		while(Number:LessThen(count, 100))
		{
			count = Number:Add(count, 1);

			new Text message;
			message = "";

			new Number testFizz;
			new Number testBuzz;

			testFizz = Number:Remainder(count, 3);
			testBuzz = Number:Remainder(count, 5);

			if(Number:AreEqual(testFizz, 0))
			{
				message = "Fizz";
				Text:Append(message, "Fizz");
			}
			if(Number:AreEqual(testBuzz, 0))
			{
				Text:Append(message, "Buzz");
			}
			if(Text:Equals(message, ""))
			{
				message = Number:ToText(count);
			}

			Console:WriteText(message);	
		}

		return true;
	}
}
