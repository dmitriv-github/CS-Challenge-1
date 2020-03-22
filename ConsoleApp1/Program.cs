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
        static Tuple<string, string> name;
        static ConsolePrinter printer = new ConsolePrinter();

        static JokesFeed jokes;

        static NamesFeed names;

        static void Main(string[] args) {
            AsyncMain(args).Wait();
        }

        static async Task AsyncMain(string[] args)
        {
            printer.printLine("Press ? to get instructions, or type 'exit' to exit.");
            string initialInput;

            do {
                initialInput = Console.ReadLine();

                if (initialInput == "exit") Environment.Exit(0);
            } while (initialInput != "?");

            printer.printLine("Press c to get categories");
            printer.printLine("Press r to get random jokes");
            printer.printLine("Press x at any time to exit");

            do {
                GetEnteredKey();

                if (key == ConsoleKey.C)
                {   
                    printer.printLine();

                    await getCategories();
                    PrintResults();
                }
                
                // if (key == ConsoleKey.R)
                // {
                //     printer.printLine("Want to use a random name? y/n");
                //     GetEnteredKey();

                //     if (key == ConsoleKey.Y) GetNames();

                //     printer.printLine("Want to specify a category? y/n");
                    
                //     if (key == ConsoleKey.Y)
                //     {
                //         printer.printLine("How many jokes do you want? (1-9)");
                //         int n = Int32.Parse(Console.ReadLine());
                //         printer.printLine("Enter a category;");
                //         GetRandomJokes(Console.ReadLine(), n);
                //         PrintResults();
                //     }
                //     else
                //     {
                //         printer.printLine("How many jokes do you want? (1-9)");
                //         int n = Int32.Parse(Console.ReadLine());
                //         GetRandomJokes(null, n);
                //         PrintResults();
                //     }
                // }

                names = null;

            } while (key != ConsoleKey.X);
        }

        private static void PrintResults()
        {
            printer.printLine("[" + string.Join(", ", results) + "]");
        }

        private static ConsoleKey GetEnteredKey() => key = Console.ReadKey().Key;

        // private static void GetRandomJokes(string category, int number)
        // {
        //     new JsonFeed("https://api.chucknorris.io", number);
        //     results = JsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category);
        // }

        private static async Task getCategories()
        {
            jokes = jokes ?? new JokesFeed();
            results = await jokes.getCategories();
        }

        // private static void GetNames()
        // {
        //     new JsonFeed("http://uinames.com/api/", 0);
        //     dynamic result = JsonFeed.Getnames();
        //     names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        // }
    }
}
