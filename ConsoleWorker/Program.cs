using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;

namespace ConsoleWorker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string appSettingsFilePath = Environment.GetEnvironmentVariable("WEBROOT_PATH") + "\\..\\..\\LogFiles\\appSettings.txt";
            File.WriteAllText(appSettingsFilePath, GetFileContent());

            string verificationFilePath = Environment.GetEnvironmentVariable("WEBROOT_PATH") + "\\..\\..\\LogFiles\\verification.txt";
            using (var fs = File.Open(verificationFilePath, FileMode.Append, FileAccess.Write, FileShare.Read | FileShare.Delete))
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

        private static string GetFileContent()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(ConfigurationManager.AppSettings["testSetting1"]);
            stringBuilder.AppendLine(ConfigurationManager.AppSettings["testSetting2"]);
            stringBuilder.AppendLine(ConfigurationManager.AppSettings["testSettingExtra"]);

            foreach (ConnectionStringSettings c in ConfigurationManager.ConnectionStrings)
            {
                stringBuilder.AppendLine(c.Name + " " + c.ConnectionString + " " + c.ProviderName);
            }

            return stringBuilder.ToString();
        }
    }
}
