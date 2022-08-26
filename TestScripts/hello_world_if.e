// Hello World

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		Console:WriteLine("Before if");

		new Boolean enterIf;

		enterIf = false;
		
		if(enterIf)
		{
			Console:WriteLine("in if 1!");
			return true;
		}

		enterIf = true;

		if(enterIf)
		{
			Console:WriteLine("in if 2!");
			//return true;
		}
		
		Console:WriteLine("after if!");
		Console:WriteLine("Hello World!");
		
		return true;
	}
}
