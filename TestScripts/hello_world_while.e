// Hello World

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		Console:WriteText("Before while");
		
		Program:While1();
		Program:While2();
		Program:While3();
		
		Console:WriteText("");
		Console:WriteText("Hello World!");
		
		return true;
	}

	Function Boolean While1()
	{
		Console:WriteText("");
		Console:WriteText("---------------");
		Console:WriteText("");

		new Boolean keepLooping;
		new Number count;

		keepLooping = true;
		count = 0;		

		while(keepLooping)
		{
			Console:WriteNumber(count);
			Number:Add(count, 1);

			if(Number:AreEqual(count, 10))
			{
				keepLooping = false;
			}		
		}

		return true;
	}

	Function Boolean While2()
	{
		Console:WriteText("");
		Console:WriteText("---------------");
		Console:WriteText("");

		new Number count2;
		count2 = 5;

		while(Number:LessThen(count2, 15))
		{
			Console:WriteNumber(count2);
			Number:Add(count2, 2);
		}

		return true;
	}

	Function Boolean While3()
	{

		Console:WriteText("");
		Console:WriteText("---------------");
		Console:WriteText("");

		new Number count3;
		count3 = 100;

		while(Number:GreaterThen(count3, -1))
		{
			Console:WriteNumber(count3);
			Number:Subtract(count3, 10);
		}
		
		return true;
	}
}
