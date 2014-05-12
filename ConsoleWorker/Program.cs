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
            string instanceId = GetInstanceId();
            string appSettingsFilePath = Environment.GetEnvironmentVariable("WEBROOT_PATH") + "\\..\\..\\LogFiles\\appSettings.txt." + instanceId;
            File.WriteAllText(appSettingsFilePath, GetFileContent());

            string verificationFilePath = Environment.GetEnvironmentVariable("WEBROOT_PATH") + "\\..\\..\\LogFiles\\verification.txt." + instanceId;
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

        private static string GetInstanceId()
        {
            string instanceId = Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID") ?? Environment.MachineName;
            return instanceId.Substring(0, instanceId.Length > 6 ? 6 : instanceId.Length);
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
