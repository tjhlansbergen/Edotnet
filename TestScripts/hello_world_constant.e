// Hello World
Constant Text line = "Hello World!";

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		Program:HelloWorld();
		return true;
	}
	
	Function Boolean HelloWorld()
	{
		Console:WriteText(line);
		return true;
	}
}
