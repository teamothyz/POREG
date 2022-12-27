using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Polly;
using SeleniumUndetectedChromeDriver;
using System.Collections.ObjectModel;

namespace POREG.WebDriverServices
{
    public static class WebDriverExtensions
    {
        private static Policy CreatePolicy(CancellationToken token)
        {
            var policy = Policy.Handle<Exception>(e => e.GetType() != typeof(OperationCanceledException))
                .Retry(5, onRetry: (exception, retryCount) =>
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(5000);
                });
            return policy;
        }

        public static IWebElement FindElementByName(this UndetectedChromeDriver driver, string name, CancellationToken token)
        {
            var by = By.Name(name);
            var element = driver.FindElementWithWait(by, token);
            return element;
        }

        public static IWebElement FindElementBySelector(this UndetectedChromeDriver driver, string cssSelector, CancellationToken token)
        {
            var by = By.CssSelector(cssSelector);
            var element = driver.FindElementWithWait(by, token);
            return element;
        }

        public static ReadOnlyCollection<IWebElement> FindElementsBySelector(this UndetectedChromeDriver driver, string cssSelector, CancellationToken token)
        {
            var by = By.CssSelector(cssSelector);
            var element = driver.FindElementsWithWait(by, token);
            return element;
        }

        public static IWebElement FindElementWithWait(this UndetectedChromeDriver driver, By by, CancellationToken token)
        {
            var policy = CreatePolicy(token);
            var result = policy.ExecuteAndCapture(() =>
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                var element = wait.Until(e => e.FindElement(by));
                return element;
            });
            if (result.FinalException != null)
            {
                throw result.FinalException;
            }
            return result.Result;
        }

        public static ReadOnlyCollection<IWebElement> FindElementsWithWait(this UndetectedChromeDriver driver, By by, CancellationToken token)
        {
            var policy = CreatePolicy(token);
            var result = policy.ExecuteAndCapture(() =>
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(e => e.FindElement(by));
                var element = wait.Until(e => e.FindElements(by));
                return element;
            });
            if (result.FinalException != null)
            {
                throw result.FinalException;
            }
            return result.Result;
        }

        public static bool IsElementDisplayed(this UndetectedChromeDriver driver, By by, CancellationToken token)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(e => e.FindElement(by));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void SendKeysWithClear(this IWebElement element, string keys, int randomTimes, CancellationToken token)
        {
            var policy = CreatePolicy(token);
            var result = policy.ExecuteAndCapture(() =>
            {
                element.Clear();
                var random = new Random();
                foreach (char key in keys)
                {
                    element.SendKeys(key.ToString());
                    Thread.Sleep(random.Next(1, 10) * randomTimes);
                }
                if (element.ValidateInput(keys))
                    return;
                else
                    throw new Exception("Input not match.");
            });
            if (result != null && result.FinalException != null)
            {
                throw result.FinalException;
            }
        }

        public static void SendKeysWithClear(this IWebElement element, string keys, CancellationToken token)
        {
            var policy = CreatePolicy(token);
            var result = policy.ExecuteAndCapture(() =>
            {
                element.Clear();
                element.SendKeys(keys);

                if (element.ValidateInput(keys))
                    return;
                else
                    throw new Exception("Input not match.");
            });
            if (result != null && result.FinalException != null)
            {
                throw result.FinalException;
            }
        }

        public static void ClickWithTry(this IWebElement element, CancellationToken token)
        {
            var policy = CreatePolicy(token);
            var result = policy.ExecuteAndCapture(() =>
            {
                element.Click();
                return;
            });
            if (result != null && result.FinalException != null)
            {
                throw result.FinalException;
            }
        }

        public static void Refresh(this UndetectedChromeDriver driver)
        {
            driver.GoToUrl(driver.Url);
        }

        public static void CaptchaWait(this UndetectedChromeDriver driver, CancellationToken token)
        {
            var policy = Policy.Handle<Exception>(e => e.GetType() != typeof(OperationCanceledException))
                .Retry(30, onRetry: (exception, retryCount) =>
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                });

            var result = policy.ExecuteAndCapture(() =>
            {
                var element = driver.FindElements(By.CssSelector("div.captcha-solver"));
                var status = element.First().GetAttribute("data-state");

                if (status.Equals("solved"))
                    return;
                else if (status.Equals("error"))
                    throw new Exception("Captcha resolver error.");
                else
                    throw new Exception("Captcha not resolved yet.");
            });

            if (result != null && result.FinalException != null)
            {
                throw result.FinalException;
            }
        }

        public static void ClickBySendKey(this IWebElement element, CancellationToken token)
        {
            var policy = CreatePolicy(token);
            var result = policy.ExecuteAndCapture(() =>
            {
                element.SendKeys(OpenQA.Selenium.Keys.Space);
                return;
            });
            if (result != null && result.FinalException != null)
            {
                throw result.FinalException;
            }
        }

        public static void ClickByJS(this IWebDriver driver, IWebElement element, CancellationToken token)
        {
            var policy = CreatePolicy(token);
            var js = (IJavaScriptExecutor)driver;
            var result = policy.ExecuteAndCapture(() =>
            {
                js.ExecuteScript("arguments[0].click();", element);
                return;
            });
            if (result != null && result.FinalException != null)
            {
                throw result.FinalException;
            }
        }

        private static bool ValidateInput(this IWebElement element, string value)
        {
            try
            {
                string retrievedText = element.GetAttribute("value");
                return retrievedText.Equals(value);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
