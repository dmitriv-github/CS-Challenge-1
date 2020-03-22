using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static string[] results = new string[50];
        static ConsoleKey key;
        static Tuple<string, string> names;
        static ConsolePrinter printer = new ConsolePrinter();

        static void Main(string[] args)
        {
            printer.printLine("Press ? to get instructions, or type 'exit' to exit.");
            string initialInput;

            do {
                initialInput = Console.ReadLine();

                if (initialInput == "exit") Environment.Exit(0);
            } while (initialInput != "?");

            printer.printLine("Press c to get categories");
            printer.printLine("Press r to get random jokes");

            GetEnteredKey();

            if (key == ConsoleKey.C)
            {
                getCategories();
                PrintResults();
            }
            
            if (key == ConsoleKey.R)
            {
                printer.printLine("Want to use a random name? y/n");
                GetEnteredKey();

                if (key == ConsoleKey.Y) GetNames();

                printer.printLine("Want to specify a category? y/n");
                
                if (key == ConsoleKey.Y)
                {
                    printer.printLine("How many jokes do you want? (1-9)");
                    int n = Int32.Parse(Console.ReadLine());
                    printer.printLine("Enter a category;");
                    GetRandomJokes(Console.ReadLine(), n);
                    PrintResults();
                }
                else
                {
                    printer.printLine("How many jokes do you want? (1-9)");
                    int n = Int32.Parse(Console.ReadLine());
                    GetRandomJokes(null, n);
                    PrintResults();
                }
            }
            names = null;
        }

        private static void PrintResults()
        {
            printer.printLine("[" + string.Join(",", results) + "]");
        }

        private static ConsoleKey GetEnteredKey() => key = Console.ReadKey().Key;

        private static void GetRandomJokes(string category, int number)
        {
            new JsonFeed("https://api.chucknorris.io", number);
            results = JsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category);
        }

        private static void getCategories()
        {
            new JsonFeed("https://api.chucknorris.io", 0);
            results = JsonFeed.GetCategories();
        }

        private static void GetNames()
        {
            new JsonFeed("http://uinames.com/api/", 0);
            dynamic result = JsonFeed.Getnames();
            names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
