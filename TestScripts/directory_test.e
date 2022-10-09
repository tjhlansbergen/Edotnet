// Directory

Utility Program
{
	Function Boolean Start(Text arguments)
	{
        new Text current;
        new List<Text> files;
        
        current = Directory:Current();
        Console:WriteLine(current);

        files = Directory:Files(current);

        foreach(file in files)
        {
            Console:WriteLine(file);
        }

		return true;
	}
}
