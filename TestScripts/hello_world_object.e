// Hello World Object

Object Car
{
    Property Text Brand;
    Property Number Wheels;
}

Utility Program
{
	Function Boolean Start(Text arguments)
	{
        new Car myCar;
        myCar.Brand = "audi";
        myCar.Wheels = 4;

		Console:WriteLine(myCar.Brand);
		return true;
	}
}
