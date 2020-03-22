using System;

namespace ConsoleApp1
{
    public class ConsolePrinter
    {
        private string printValue;

        public void print(string value)
        {
            this.printValue = value;
            Console.WriteLine(value);
        }

        public override string ToString()
        {
            return $"{nameof(ConsolePrinter)}: {this.printValue}";
        }
    }
}
