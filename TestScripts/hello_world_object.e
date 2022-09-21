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
        myCar.Brand = "Audi";
        myCar.Wheels = 4;

        new Car myOtherCar;
        myOtherCar.Brand = "Porsche";
        myOtherCar.Wheels = 2;

        Console:WriteLine("Assignment done!");

		Console:WriteLine(myCar.Brand);
        Console:WriteLine(myCar.Wheels);

        Console:WriteLine(myOtherCar.Brand);
        Console:WriteLine(myOtherCar.Wheels);

		return true;
	}
}
