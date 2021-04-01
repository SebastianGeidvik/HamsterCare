using BackEnd;
using System;
using System.Linq;

namespace FrontEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            var readFile = new ReadFromFile();
            readFile.ImportFile();
        }
    }
}