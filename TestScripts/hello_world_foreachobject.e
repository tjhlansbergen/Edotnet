// Hello World foreach-userobject

Utility Program
{
    Object Cat
    {
        Property Text Name;
        Property Number Age;
        Property Boolean Alive;
    }

	Function Boolean Start(Text arguments)
	{
        new List<Cat> myCats;
        new Number count;
        count = 0;

        while(Number:LessThen(count, 10))
        {
            new Cat cat;
            cat.Name = "Fiffy";
            Text:Append(cat.Name, Number:ToText(Number:Add(count, 1)));
            cat.Age = 7;
            // cat.Alive = true;

            List:Add(myCats, cat);
            count = Number:Add(count, 1);
        }

		Console:WriteLine(List:Count(myCats));
        Console:WriteLine("");

        foreach(cat in myCats)
        {
            Console:WriteLine(cat.Name);
            Console:WriteLine(cat.Age);
            Console:WriteLine(cat.Alive);
        }

		return true;
	}
}
