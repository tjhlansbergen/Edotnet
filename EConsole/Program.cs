using System;
using System.IO;

namespace EConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //check if there are args
            if (args.Length == 0)
            {
                Console.Write("Please provide the path of the E# file to run: ");
                Array.Resize(ref args, 1);
                args[0] = Console.ReadLine();
            }

            var path = args[0].Replace("\"", "");

            _runInterpreter(path);
        }

        private static void _runInterpreter(string path)
        {
            // check if the file exists
            if (!File.Exists(path))
            {
                Console.WriteLine($"No file found at:\n{path}");
                Console.ReadLine();
                return;
            }

            // check if the file is readable text
            if (!Helpers._isValidTextFileAsync(path))
            {
                Console.WriteLine($"File {path}");
                Console.WriteLine("is not recognized as a valid E# file.");
                Console.ReadLine();
                return;
            }

            var interpreter = new EInterpreter.Worker();

            do
            {
                interpreter.Go(File.ReadAllLines(path), Path.GetFileName(path));
            } while (Helpers._continue());
        }
    }
}
