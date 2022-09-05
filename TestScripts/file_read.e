// file read

Utility Program
{
	Function Boolean Start(Text arguments)
	{
        new Text contents;
        contents = File:Read("C:\Users\lansta\source\repos\Tako\Edotnet\TestScripts\hello_world.e");
		Console:WriteLine(contents);

		new List<Text> lines;
		lines = File:ReadLines("C:\Users\lansta\source\repos\Tako\Edotnet\TestScripts\hello_world.e");
		Console:WriteLine(List:First(lines));

		return true;
	}
}
