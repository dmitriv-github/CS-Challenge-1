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
            printer.PrintLine("Press ? to get instructions, or type 'exit' to exit.");
            string initialInput;

            do {
                initialInput = Console.ReadLine();

                if (initialInput == "exit") Environment.Exit(0);
            } while (initialInput != "?");

            printer.PrintLine("Press c to get categories");
            printer.PrintLine("Press r to get random jokes");
            printer.PrintLine("Press x at any time to exit");

            do {
                GetEnteredKey();

                if (key == ConsoleKey.C)
                {   
                    printer.PrintLine();

                    await GetCategories();
                    PrintResults();
                }
                
                if (key == ConsoleKey.R)
                {
                    printer.PrintLine();
                    printer.PrintLine("Want to use a random name? y/n");
                    GetEnteredKey();

                    if (key == ConsoleKey.Y) {
                        await GetNames();
                    }

                    printer.PrintLine("Want to specify a category? y/n");
                    
                    // if (key == ConsoleKey.Y)
                    // {
                    //     printer.printLine("How many jokes do you want? (1-9)");
                    //     int n = Int32.Parse(Console.ReadLine());
                    //     printer.printLine("Enter a category;");
                    //     GetRandomJokes(Console.ReadLine(), n);
                    //     PrintResults();
                    // }
                    // else
                    // {
                    //     printer.printLine("How many jokes do you want? (1-9)");
                    //     int n = Int32.Parse(Console.ReadLine());
                    //     GetRandomJokes(null, n);
                    //     PrintResults();
                    // }
                }

                name = null;

            } while (key != ConsoleKey.X);
        }

        private static void PrintResults()
        {
            printer.PrintLine("[" + string.Join(", ", results) + "]");
        }

        private static ConsoleKey GetEnteredKey() => key = Console.ReadKey().Key;

        private static void GetRandomJokes(string category, int number)
        {
            jokes = jokes ?? new JokesFeed();
            results = jokes.GetRandomJokes(name?.Item1, name?.Item2, category);
        }

        private static async Task GetCategories()
        {
            jokes = jokes ?? new JokesFeed();
            results = await jokes.GetCategories();
        }

        private static async Task GetNames()
        {
            names = names ?? new NamesFeed();
            dynamic result = await names.GetName();
            name = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
