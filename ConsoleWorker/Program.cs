using System;
using System.IO;

namespace ConsoleWorker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            File.WriteAllText("verification.txt", "Verified!!!");
            Console.WriteLine("Verification file written");
        }
    }
}
