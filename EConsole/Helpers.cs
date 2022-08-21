using System;
using System.IO;
using System.Linq;
using System.Text;

namespace EConsole
{
    public static class Helpers
    {
        public static bool _continue()
        {
            ConsoleKeyInfo key;

            do
            {
                Console.WriteLine("Press Enter to run again, or Esc to exit.");
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Escape && key.Key != ConsoleKey.Enter);

            return key.Key == ConsoleKey.Enter;
        }

        public static bool _isValidTextFileAsync(string path, int scanLength = 4096)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var bufferLength = (int)Math.Min(scanLength, stream.Length);
                var buffer = new char[bufferLength];

                var bytesRead = reader.ReadBlock(buffer, 0, bufferLength);
                reader.Close();

                if (Math.Abs(bufferLength-bytesRead) > 3)
                {
                    throw new IOException($"There was an error reading from the file {path}");
                }

                char[] allowedChars = {
                    (char)9,    // Horizontal Tab
                    (char)10,   // New Line 
                    (char)11,   // vertical Tab
                    (char)13    // Carriage Return
                };

                for (int i = 0; i < bytesRead; i++)
                {
                    var c = buffer[i];

                    if (char.IsControl(c) && !allowedChars.Contains(c))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

    }
}
