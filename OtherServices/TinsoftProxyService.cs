using Newtonsoft.Json;
using Polly;

namespace POREG.OtherServices
{
    public class TinsoftProxyService
    {
        private static Policy CreatePolicy(CancellationToken token)
        {
            var policy = Policy.Handle<Exception>(e => e.GetType() != typeof(OperationCanceledException))
                .Retry(20, onRetry: (exception, retryCount) =>
                {
                    token.ThrowIfCancellationRequested();
                });
            return policy;
        }

        public static string GetNewIP(string key, CancellationToken token)
        {
            var client = new HttpClient();
            var policy = CreatePolicy(token);
            var result = policy.ExecuteAndCapture(() =>
            {
                var wait = CheckNextChange(key, token);
                wait = wait >= 6 ? wait : 6;
                Thread.Sleep(TimeSpan.FromSeconds(wait));

                var url = $"http://proxy.tinsoftsv.com/api/changeProxy.php?key={key}";
                var response = client.GetAsync(url, token).Result;
                dynamic? content = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                if (content == null || !bool.Parse(content?.success.ToString()))
                {
                    throw new Exception("[TinsoftProxy] Error while getting new IP.");
                }
                return content?.proxy?.ToString();
            });
            if (result.FinalException != null)
            {
                throw result.FinalException;
            }
            return result.Result?.ToString();
        }

        public static int CheckNextChange(string key, CancellationToken token)
        {
            var client = new HttpClient();
            var policy = CreatePolicy(token);
            var result = policy.ExecuteAndCapture(() =>
            {
                var url = $"https://proxy.tinsoftsv.com/api/getProxy.php?key={key}";
                var response = client.GetAsync(url, token).Result;
                dynamic? content = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                if (content == null)
                {
                    throw new Exception("[TinsoftProxy] Error while checking IP.");
                }
                else if (bool.Parse(content?.success.ToString()))
                {
                    return content?.next_change?.ToString();
                }
                return 0;
            });
            if (result.FinalException != null)
            {
                throw result.FinalException;
            }
            return int.Parse(result.Result?.ToString());
        }
    }
}