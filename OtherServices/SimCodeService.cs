using Newtonsoft.Json;
using Polly;
using POREG.Models;

namespace POREG.OtherServices
{
    public class SimCodeService
    {
        private static readonly string KeyFileName = "chothuesimcodekey.txt";
        private static readonly string KeyFolderName = "Keys";

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

        private static Policy CreatePolicy(CancellationToken token)
        {
            var policy = Policy.Handle<Exception>(e => e.GetType() != typeof(OperationCanceledException))
                .Retry(20, onRetry: (exception, retryCount) =>
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(5000);
                });
            return policy;
        }

        public static Tuple<string, string> GetSim(string apikey, CancellationToken token)
        {
            var client = new HttpClient();
            var policy = CreatePolicy(token);
            var result = policy.ExecuteAndCapture(() =>
            {
                var url = $"https://chothuesimcode.com/api?act=number&apik={apikey}&appId=1089";
                var result = client.GetStringAsync(url, token).Result;
                var content = JsonConvert.DeserializeObject<SimCodeResponse>(result);
                if (content == null || content.ResponseCode != 0)
                {
                    throw new Exception("[SimCodeService] Get Sim Request Failed.");
                }
                if (content.Result?.App.ToString().Equals("Payoneer"))
                {
                    var id = content.Result?.Id.ToString();
                    var number = content.Result?.Number.ToString();
                    return new Tuple<string, string>(id, number);
                }
                else
                {
                    throw new Exception("[SimCodeService] Wrong Type Of Sim.");
                }
            });
            if (result.FinalException != null)
            {
                throw result.FinalException;
            }
            return result.Result;
        }

        public static string GetCode(string apikey, string id, CancellationToken token)
        {
            var client = new HttpClient();
            var policy = CreatePolicy(token);
            var result = policy.ExecuteAndCapture(() =>
            {
                var url = $"https://chothuesimcode.com/api?act=code&apik={apikey}&id={id}";
                var result = client.GetStringAsync(url, token).Result;
                var content = JsonConvert.DeserializeObject<SimCodeResponse>(result);
                if (content == null || content.ResponseCode != 0)
                {
                    throw new Exception("[SimCodeService] Get Code Request Failed.");
                }
                var code = content.Result?.Code.ToString();
                if (content.Result?.SMS.ToString().Contains("Payoneer") && code != null)
                {
                    return code;
                }
                else
                {
                    throw new Exception("[SimCodeService] Wrong Type Of Code.");
                }
            });
            if (result != null && result.FinalException != null)
            {
                var url = $"https://chothuesimcode.com/api?act=expired&apik={apikey}&id={id}";
                client.GetAsync(url).Wait();
                throw result.FinalException;
            }
            return result.Result?.ToString();
        }
    }
}
