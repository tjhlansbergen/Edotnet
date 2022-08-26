// Hello World

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		Console:WriteLine("Before while");
		
		Program:While1();
		Program:While2();
		Program:While3();
		
		Console:WriteLine("");
		Console:WriteLine("Hello World!");
		
		return true;
	}

	Function Boolean While1()
	{
		Console:WriteLine("");
		Console:WriteLine("---------------");
		Console:WriteLine("");

		new Boolean keepLooping;
		new Number count;

		keepLooping = true;
		count = 0;		

		while(keepLooping)
		{
			Console:WriteLine(count);
			count = Number:Add(count, 1);

			if(Number:AreEqual(count, 10))
			{
				keepLooping = false;
			}		
		}

		return true;
	}

	Function Boolean While2()
	{
		Console:WriteLine("");
		Console:WriteLine("---------------");
		Console:WriteLine("");

		new Number count2;
		count2 = 5;

		while(Number:LessThen(count2, 15))
		{
			Console:WriteLine(count2);
			count2 = Number:Add(count2, 2);
		}

		return true;
	}

	Function Boolean While3()
	{

		Console:WriteLine("");
		Console:WriteLine("---------------");
		Console:WriteLine("");

		new Number count3;
		count3 = 100;

		while(Number:GreaterThen(count3, -1))
		{
			Console:WriteLine(count3);
			count3 = Number:Subtract(count3, 10);
		}
		
		return true;
	}
}
