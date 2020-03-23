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
        static string[] categories;
        static string[] results;
        static ConsoleKey key;
        static (string first, string last) name;
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

            do {
                printer.PrintLine("Press c to get categories");
                printer.PrintLine("Press r to get random jokes");
                printer.PrintLine("Press x to exit");
                GetEnteredKey();

                if (key == ConsoleKey.C)
                {   
                    printer.PrintLine();
                    printer.PrintArray(await GetCategories(), true);
                }
                
                if (key == ConsoleKey.R)
                {
                    printer.PrintLine("\nWant to use a random name? y/n");
                    GetEnteredKey();

                    if (key == ConsoleKey.Y) {
                        await GetRandomName();
                    }

                    printer.PrintLine("\nWant to specify a category? y/n");
                    GetEnteredKey();

                    string category = key == ConsoleKey.Y ? await GetCategoryFromUSer() : null;

                    printer.PrintLine("\nHow many jokes do you want? (1-9)");
                    
                    int numJokes = 0;

                    try {
                        numJokes = Int32.Parse(Console.ReadLine());

                        if (numJokes < 1 || numJokes > 9) {
                            printer.PrintLine("Please enter a number between 1 and 9.\n");
                            continue;
                        }

                        await GetRandomJokes(category, numJokes);
                        printer.PrintArray(results, false);
                        (name.last, name.first) = (null, null);
                    } catch (FormatException e) {
                        printer.PrintLine("Please enter a number between 1 and 9.\n");
                    }
                }

            } while (key != ConsoleKey.X);
        }

        private static ConsoleKey GetEnteredKey() => key = Console.ReadKey().Key;

        private static async Task GetRandomJokes(string category, int number)
        {
            jokes = jokes ?? new JokesFeed();
            results = await jokes.GetRandomJokes(number, category, name.first, name.last);
        }

        private static async Task<string[]> GetCategories()
        {   
            if (categories is null) {
                jokes = jokes ?? new JokesFeed();
                categories = await jokes.GetCategories();
            } 

            return categories;
        }

        private static async Task GetRandomName()
        {
            names = names ?? new NamesFeed();
            (name.first, name.last) = await names.GetName();
        }

        private static async Task<string> GetCategoryFromUSer() {
            await GetCategories();

            while (true) {
                printer.PrintLine("\nEnter a category (or 'cancel' to cancel):");

                string category = Console.ReadLine();

                if (category == "cancel") {
                    return null;
                }

                if (categories.Contains(category)) {
                    return category;
                } 

                printer.PrintLine("Category must be an item from the following list:");
                printer.PrintArray(categories, true);
            }
        }
    }
}
