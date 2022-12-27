using OpenQA.Selenium.Chrome;
using POREG.WebDriverServices;
using SeleniumUndetectedChromeDriver;
using POREG.OtherServices;

namespace POREG.Models
{
    public class WebDriverWorker
    {
        public UndetectedChromeDriver Driver { get; set; }

        public WebDriverWorker(ref POInfor info, int index, string tinsoftKey, CancellationToken token)
        {
            try
            {
                var currentDir = Directory.GetCurrentDirectory();
                var options = new ChromeOptions();

                var captchaExtension = $@"{currentDir}/ChromeDriver/2captcha";
                options.AddArgument($@"--load-extension={captchaExtension}");

                var proxy = TinsoftProxyService.GetNewIP(tinsoftKey, token);
                options.AddArgument($@"--proxy-server=http://{proxy}");

                var width = Screen.PrimaryScreen.Bounds.Width / 5;
                var height = (Screen.PrimaryScreen.Bounds.Height / 2) + 100;
                options.AddArgument($"--window-size={width},{height}");

                Driver = UndetectedChromeDriver.Create(
                    driverExecutablePath: $@"{currentDir}/ChromeDriver/chromedriver.exe",
                    hideCommandPromptWindow: true,
                    commandTimeout: TimeSpan.FromSeconds(60),
                    options: options);

                Driver.Manage().Window.Position = new Point(GetPositionX(index), GetPositionY(index));
            }
            catch (Exception)
            {
                Close();
                info.Status = "Canceled";
            }
        }

        public void Run(ref POInfor info, string simcodeKey, CancellationToken token)
        {
            try
            {
                using (token.Register(Close))
                {
                    Thread.Sleep(3000);
                    WebDriverServiceBase.ChipChipHome(Driver, token);
                    WebDriverServiceBase.ChipChipRegister(Driver, info, token);
                    WebDriverServiceBase.ChipChipSettingPage(Driver, token);

                    WebDriverServiceBase.PayoneerRegisterStartPage(Driver, token);

                    WebDriverServiceBase.PayoneerRegisterPage1(Driver, info, token);
                    info.Phone = WebDriverServiceBase.PayoneerRegisterPage2(Driver, info, simcodeKey, token);
                    WebDriverServiceBase.PayoneerRegisterPage3(Driver, info, token);
                    WebDriverServiceBase.PayoneerRegisterPage4(Driver, info, token);

                    WebDriverServiceBase.LoginPayoneer(Driver, info, token);
                    WebDriverServiceBase.VerifyPayoneerEmail(Driver, info, token);
                    WebDriverServiceBase.SecuritySettingsPayoneer(Driver, info, token);
                    WebDriverServiceBase.SendKYCDocument(Driver, info, token);

                    info.Status = "Success";
                    ExportDataService.ExportData(info);
                }
            }
            catch (OperationCanceledException)
            {
                info.Status = "Canceled";
                ExportDataService.ExportErrorData(info);
            }
            catch (Exception)
            {
                info.Status = "Failed";
                ExportDataService.ExportErrorData(info);
            }
            Close();
        }

        private int GetPositionX(int index)
        {
            var width = Screen.PrimaryScreen.Bounds.Width / 5;
            index = index >= 5 ? index - 5 : index;
            return width * index;
        }

        private int GetPositionY(int index)
        {
            if (index < 5)
            {
                return 0;
            }
            return Screen.PrimaryScreen.Bounds.Height / 2;
        }

        private void Close()
        {
            try
            {
                Driver?.Close();
            }
            catch (Exception) { }
        }
    }
}
