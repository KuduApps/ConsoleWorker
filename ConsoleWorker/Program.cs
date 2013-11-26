using System;
using System.IO;
using System.Text;
using System.Threading;

namespace ConsoleWorker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = Environment.GetEnvironmentVariable("WEBROOT_PATH") + "\\..\\..\\LogFiles\\verification.txt";
            using (var fs = File.Open(path, FileMode.Append, FileAccess.Write, FileShare.Read | FileShare.Delete))
            {
                byte[] bytes = Encoding.UTF8.GetBytes("Verified!!!" + Environment.NewLine);
                fs.Write(bytes, 0, bytes.Length);
                fs.Flush(true);

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
