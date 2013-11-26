using System;
using System.IO;
using System.Threading;

namespace ConsoleWorker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = Path.Combine(Environment.GetEnvironmentVariable("WEBROOT_PATH"), "\\..\\..\\LogFiles\\verification.txt");
            using (var fs = File.Open(path, FileMode.Append, FileAccess.Write, FileShare.Read | FileShare.Delete))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine("Verified!!!");
                    sw.Flush();

                    Console.WriteLine("Verification file written");

                    // Keep file locked until process ends
                    while (true)
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}
