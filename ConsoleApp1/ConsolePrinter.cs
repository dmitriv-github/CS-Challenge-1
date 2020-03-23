using System;

namespace ConsoleApp1
{
    /// <summary> Utility class for console printing functions.</summary>
    public class ConsolePrinter
    {
        private string printValue;

        /// <summary>Overloaded version of PrintLine to print a blank line on the console.</summary>
        public void PrintLine() => Console.WriteLine();

        /// <summary> Prints the specified string value on its own line in the console. </summary>
        public void PrintLine(string value)
        {
            this.printValue = value;
            Console.WriteLine(value);
        }

        /// <summary> Helper function for printing string arrays. </summary>
        /// <param name="onOneLine">Whether the array items should be printed all on one line (comma-separated), or
        /// printed with a separate line for each item.</param>
        public void PrintArray(string[] items, bool onOneLine) {
            if (onOneLine) {
                Console.WriteLine("[{0}]", string.Join(", ", items));
            } else {
                Console.WriteLine('[');
                
                foreach(string item in items) {
                    Console.WriteLine("\t {0}", item);
                }

                Console.WriteLine(']');
            }
        }

        public override string ToString()
        {
            return $"{nameof(ConsolePrinter)}: {this.printValue}";
        }
    }
}
