// file read

Utility Program
{
	Function Boolean Start(Text arguments)
	{
        new Text contents;
        contents = File:Read("./TestScripts/hello_world.e");
		Console:WriteLine(contents);

		new List<Text> lines;
		lines = File:ReadLines("./TestScripts/hello_world.e");
		Console:WriteLine(List:First(lines));

		return true;
	}
}
