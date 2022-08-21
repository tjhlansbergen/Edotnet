// Hello World

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		Console:WriteText("Before if");

		new Boolean enterIf;

		enterIf = false;
		
		if(enterIf)
		{
			Console:WriteText("in if 1!");
			return true;
		}

		enterIf = true;

		if(enterIf)
		{
			Console:WriteText("in if 2!");
			//return true;
		}
		
		Console:WriteText("after if!");
		Console:WriteText("Hello World!");
		
		return true;
	}
}
