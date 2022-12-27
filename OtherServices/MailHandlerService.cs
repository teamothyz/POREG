using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using Polly;

namespace POREG.OtherServices
{
    public class MailHandlerService
    {
        private static Policy CreatePolicy(CancellationToken token)
        {
            var policy = Policy.Handle<Exception>(e => e.GetType() != typeof(OperationCanceledException))
                .Retry(12, onRetry: (exception, retryCount) =>
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(5000);
                });
            return policy;
        }

        private static IMailFolder ReadMail(string username, string password, CancellationToken token)
        {
            var client = new ImapClient();
            client.Connect("imap-mail.outlook.com", 993, SecureSocketOptions.SslOnConnect, token);
            client.Authenticate(username, password, token);
            client.Inbox.Open(FolderAccess.ReadWrite, token);
            return client.Inbox;
        }

        public static string GetVerifyUrl(string username, string password, CancellationToken token)
        {
            var policy = CreatePolicy(token);
            var result = policy.ExecuteAndCapture(() =>
            {
                var folder = ReadMail(username, password, token);
                var payoneerUids = folder.Search(SearchQuery.FromContains("payoneer.com"), token);
                var inboxes = payoneerUids.Select(uid => folder.GetMessage(uid, token))
                    .OrderByDescending(inbox => inbox.Date);
                foreach (var inbox in inboxes)
                {
                    var startIndex = inbox.HtmlBody.IndexOf("https://link.payoneer.com/Token");
                    if (startIndex > -1 && !inbox.HtmlBody.Contains("payoneer.custhelp.com/app/answers"))
                    {
                        var tempString = inbox.HtmlBody[startIndex..];
                        var endIndex = tempString.IndexOf("\"");
                        var url = tempString[..endIndex];
                        return url;
                    }
                }
                throw new Exception("Email verification not found.");
            });
            if (result.FinalException != null)
            {
                return "https://myaccount.payoneer.com/";
            }
            return result.Result;
        }

        public static string GetVerifyCode(string username, string password, CancellationToken token)
        {
            var policy = CreatePolicy(token);
            var result = policy.ExecuteAndCapture(() =>
            {
                var folder = ReadMail(username, password, token);
                var payoneerUids = folder.Search(SearchQuery.FromContains("payoneer.com"), token);
                var unreadUids = folder.Search(SearchQuery.NotSeen, token);
                var validUids = payoneerUids.Intersect(unreadUids);
                folder.AddFlags(unreadUids, MessageFlags.Seen, true, token);
                var inboxes = validUids.Select(uid => folder.GetMessage(uid, token))
                    .OrderByDescending(inbox => inbox.Date);
                foreach (var inbox in inboxes)
                {
                    var endIndex = inbox.HtmlBody.IndexOf(@"</b></p></td>");
                    if (inbox.Subject.Contains("Payoneer") && endIndex > -1)
                    {
                        var tempString = inbox.HtmlBody[..endIndex];
                        var startIndex = tempString.IndexOf(@"<b>");
                        var code = tempString[startIndex..].Replace("<b>", "");
                        return code;
                    }
                }
                throw new Exception("Email verification not found.");
            });
            if (result.FinalException != null)
            {
                throw result.FinalException;
            }
            return result.Result;
        }
    }
}