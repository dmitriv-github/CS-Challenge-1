using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class ConsolePrinter
    {
        private string printValue;

        public void print(string value)
        {
            this.printValue = value;
            Console.WriteLine(this.printValue);
        }

        public override string ToString()
        {
            Console.WriteLine(PrintValue);
            return null;
        }
    }
}
