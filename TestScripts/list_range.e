// list range

Utility Program
{
    Function Boolean Start(Text arguments)
    {
        new List<Number> myList;
        myList = List:Range(7);

        foreach(item in myList)
        {
            Console:WriteLine(item);
        }

        return true;
    }
}