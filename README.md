# Edotnet

Experimental scripting/programming language implemented in C# (dotnet).

# Getting started: 
- Navigate to EConsole
- `dotnet run`
- Paste path to .e script
- Enjoy

# Running tests:
From the root directory (the one where this readme is), run `dotnet test -v=quiet`. Expect results like:

```
Passed!  - Failed:     0, Passed:    27, Skipped:     0, Total:    27, Duration: 527 ms - EBuildIn.Tests.dll (net10.0)
Passed!  - Failed:     0, Passed:   149, Skipped:     0, Total:   149, Duration: 871 ms - EInterpreter.Tests.dll (net10.0)
Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:    1, Duration:  94 ms - EConsole.Tests.dll (net10.0)
```

# Adding a 'BuildIn' module:
- Add a new .cs file to EBuildIn with the name of the new module
- Implement it's methods. For each method expose it's parameters as an array of `EBuildIn.Types` 
- Create a test in EBuiltIn.Tests
- Create a test script in /TestScripts

# Sample E script
```
// Fizz Buzz

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		Program:FizzBuzz();
		
		return true;
	}

	Function Boolean FizzBuzz()
	{
		new Number count;	
		count = 0;		

		while(Number:LessThen(count, 100))
		{
			count = Number:Add(count, 1);

			new Text message;
			message = "";

			new Number testFizz;
			new Number testBuzz;

			testFizz = Number:Remainder(count, 3);
			testBuzz = Number:Remainder(count, 5);

			if(Number:AreEqual(testFizz, 0))
			{
				message = "Fizz";
				Text:Append(message, "Fizz");
			}
			if(Number:AreEqual(testBuzz, 0))
			{
				Text:Append(message, "Buzz");
			}
			if(Text:Equals(message, ""))
			{
				message = Number:ToText(count);
			}

			Console:WriteLine(message);	
		}

		return true;
	}
}
``` 

