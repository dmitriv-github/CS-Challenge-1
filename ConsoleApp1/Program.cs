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
        static char key;
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

            if (key == 'c')
            {
                getCategories();
                PrintResults();
            }
            
            if (key == 'r')
            {
                printer.printLine("Want to use a random name? y/n");
                GetEnteredKey();

                if (key == 'y') GetNames();

                printer.printLine("Want to specify a category? y/n");
                
                if (key == 'y')
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

        private static void GetEnteredKey()
        {
            ConsoleKeyInfo enteredKey = Console.ReadKey();

            switch (enteredKey.Key)
            {
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
                    break;
                case ConsoleKey.D3:
                    key = '3';
                    break;
                case ConsoleKey.D4:
                    key = '4';
                    break;
                case ConsoleKey.D5:
                    key = '5';
                    break;
                case ConsoleKey.D6:
                    key = '6';
                    break;
                case ConsoleKey.D7:
                    key = '7';
                    break;
                case ConsoleKey.D8:
                    key = '8';
                    break;
                case ConsoleKey.D9:
                    key = '9';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
            }
        }

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
