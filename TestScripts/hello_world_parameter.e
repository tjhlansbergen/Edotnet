// Hello World

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		Program:HelloWorld("Hello World!");
		return true;
	}
	
	Function Boolean HelloWorld(Text line)
	{
		Console:WriteText(line);
		return true;
	}
}
