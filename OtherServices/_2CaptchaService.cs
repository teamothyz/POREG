using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POREG.OtherServices
{
    public class _2CaptchaService
    {
        private static readonly string KeyFileName = "_2captchakey.txt";
        private static readonly string KeyFolderName = "Keys";
        private static readonly string ConfigTemplateFileName = "ChromeDriver/2captcha/common/configTemplate.js";
        private static readonly string ConfigFileName = "ChromeDriver/2captcha/common/config.js";

        public static string GetKey()
        {
            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                using var reader = new StreamReader($"{currentDirectory}/{KeyFolderName}/{KeyFileName}");
                while (reader.Peek() != -1)
                {
                    var line = reader.ReadLine();
                    if (line != null && !line.Trim().Equals(""))
                    {
                        return line;
                    }
                }
                reader.Close();
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static void SaveKey(string apikey)
        {
            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var folderKey = $"{currentDirectory}/{KeyFolderName}";
                if (!Directory.Exists(folderKey))
                {
                    Directory.CreateDirectory(folderKey);
                }
                using var writer = new StreamWriter($"{folderKey}/{KeyFileName}", append: false);
                writer.Write(apikey);
                writer.Flush();
                writer.Close();
            }
            catch (Exception) { }
        }

        public static void ReWriteConfigFile(string apikey)
        {
            var template = GetTemplateContent();
            template = template.Replace("@@@KEY@@@", apikey);
            var currentDirectory = Directory.GetCurrentDirectory();
            using var writer = new StreamWriter($"{currentDirectory}/{ConfigFileName}", append: false);
            writer.Write(template);
            writer.Flush();
            writer.Close();
        }

        private static string GetTemplateContent()
        {
            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                using var reader = new StreamReader($"{currentDirectory}/{ConfigTemplateFileName}");
                while (reader.Peek() != -1)
                {
                    return reader.ReadToEnd();
                }
                reader.Close();
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
