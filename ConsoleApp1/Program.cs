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

            printer.PrintLine("Press c to get categories");
            printer.PrintLine("Press r to get random jokes");
            printer.PrintLine("Press x at any time to exit");

            do {
                GetEnteredKey();

                if (key == ConsoleKey.C)
                {   
                    printer.PrintLine();
                    printer.PrintArray(await GetCategories(), true);
                }
                
                if (key == ConsoleKey.R)
                {
                    printer.PrintLine();
                    printer.PrintLine("Want to use a random name? y/n");
                    GetEnteredKey();

                    if (key == ConsoleKey.Y) {
                        await GetRandomName();
                    }

                    printer.PrintLine();
                    printer.PrintLine("Want to specify a category? y/n");
                    GetEnteredKey();

                    string category = key == ConsoleKey.Y ? await GetCategoryFromInput() : null;

                    printer.PrintLine();
                    printer.PrintLine("How many jokes do you want? (1-9)");
                    
                    int n = Int32.Parse(Console.ReadLine());
                    await GetRandomJokes(category, n);
                    printer.PrintArray(results, false);
                    (name.last, name.first) = (null, null);
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

        private static async Task<string> GetCategoryFromInput() {
            await GetCategories();

            while (true) {
                printer.PrintLine();
                printer.PrintLine("Enter a category (or 'cancel' to cancel):");

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
