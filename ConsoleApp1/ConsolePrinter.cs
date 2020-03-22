using System;

namespace ConsoleApp1
{
    public class ConsolePrinter
    {
        private string printValue;

        public void PrintLine() 
        {
            Console.WriteLine();
        }

        public void PrintLine(string value)
        {
            this.printValue = value;
            Console.WriteLine(value);
        }

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
