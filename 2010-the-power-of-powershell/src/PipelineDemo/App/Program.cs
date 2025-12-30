using System;
using System.IO;

namespace PipelineDemo.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ParseArgs(args);
            ParseInputStream();
        }

        private static void ParseInputStream()
        {
            StreamReader reader = new StreamReader(Console.OpenStandardInput());
            WriteWithColor("---------------------------------");
            WriteWithColor("Standard Input:");
            Console.WriteLine(reader.ReadToEnd());
            WriteWithColor("---------------------------------");
        }

        private static void ParseArgs(string[] args)
        {
            WriteWithColor("---------------------------------");
            WriteWithColor("Argument list:");
            foreach (string arg in args)
            {
                Console.WriteLine(arg);
            }
            WriteWithColor("---------------------------------");
        }

        private static void WriteWithColor(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}