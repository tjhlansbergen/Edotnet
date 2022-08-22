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
			Number:Add(count, 1);

			new Text message;
			message = "";


			//if(Number:Remainder(count, 3))
			//{
			//	Text:Append(message, "Fizz");
			//}
			//if(Number:Remainder(count, 5))
			//{
			//	Text:Append(message, "Buzz");
			//}

			Console:WriteNumber(count);
			Console:WriteText(message);	
		}

		return true;
	}
}
