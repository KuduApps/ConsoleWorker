using System;
using System.IO;

namespace ConsoleWorker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            File.AppendAllText(Environment.GetEnvironmentVariable("WORKER_ROOT") + "\\..\\..\\..\\LogFiles\\verification.txt", "Verified!!!\n");
            Console.WriteLine("Verification file written");
        }
    }
}
