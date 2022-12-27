using OpenQA.Selenium;
using Polly;
using POREG.Models;
using POREG.OtherServices;
using SeleniumUndetectedChromeDriver;

namespace POREG.WebDriverServices
{
    public class WebDriverServiceBase
    {
        private static Policy CreatePolicy(UndetectedChromeDriver driver, CancellationToken token)
        {
            var policy = Policy.Handle<Exception>(e => e.GetType() != typeof(OperationCanceledException))
                .Retry(3, onRetry: (exception, retryCount) =>
                {
                    token.ThrowIfCancellationRequested();
                    driver.GoToUrl(driver.Url);
                    Thread.Sleep(5000);
                });
            return policy;
        }

        private static void ResultHandle(PolicyResult? result)
        {
            if (result != null && result.FinalException != null)
            {
                throw result.FinalException;
            }
        }

        private static void CaptchaHandle(UndetectedChromeDriver driver, IWebElement button, string expectedCondition, CancellationToken token)
        {
            var policy = Policy.Handle<Exception>(e => e.GetType() != typeof(OperationCanceledException))
                .Retry(1, onRetry: (exception, retryCount) =>
                {
                    token.ThrowIfCancellationRequested();
                });
            var result = policy.ExecuteAndCapture(() =>
            {
                driver.CaptchaWait(token);
                Thread.Sleep(2000);
                button.ClickWithTry(token);
                driver.FindElementBySelector(expectedCondition, token);
                return;
            });
            ResultHandle(result);
        }

        public static void ChipChipHome(UndetectedChromeDriver driver, CancellationToken token)
        {
            var policy = CreatePolicy(driver, token);
            var result = policy.ExecuteAndCapture(() =>
            {
                driver.GoToUrl("https://www.chipchip.com/");
                var urls = driver.FindElementsBySelector("div.grid-footer-links a.footer-link", token);
                var getStartedUrl = urls.Where(url => url.Text.Equals("Get started", StringComparison.OrdinalIgnoreCase))
                    .First().GetAttribute("href");
                driver.GoToUrl(getStartedUrl);
                return;
            });
            ResultHandle(result);
        }

        public static void ChipChipRegister(UndetectedChromeDriver driver, POInfor info, CancellationToken token)
        {
            var policy = CreatePolicy(driver, token);
            var result = policy.ExecuteAndCapture(() =>
            {
                if (!driver.Url.Contains("register"))
                    return;

                Thread.Sleep(1000);
                var emailElement = driver.FindElementByName("email", token);
                emailElement.SendKeysWithClear(info.Email, token);

                Thread.Sleep(1000);
                var passwordElement = driver.FindElementByName("password", token);
                passwordElement.SendKeysWithClear(info.Password, token);

                var button = driver.FindElementBySelector(@"form.ui.form button[type=""submit""]", token);
                var conditon = ".SetupUserProfilePage";
                CaptchaHandle(driver, button, conditon, token);
                return;
            });
            ResultHandle(result);
        }

        public static void ChipChipSettingPage(UndetectedChromeDriver driver, CancellationToken token)
        {
            driver.GoToUrl("https://dashboard.chipchip.com/manager/settings/payment");
            var policy = CreatePolicy(driver, token);
            policy.ExecuteAndCapture(() =>
            {
                Thread.Sleep(1000);
                var acceptLicense = driver.FindElementBySelector("div.modal div.actions button.primary", token);
                driver.ClickByJS(acceptLicense, token);
                return;
            });

            var result = policy.ExecuteAndCapture(() =>
            {
                if (driver.Url.Contains("partners", StringComparison.OrdinalIgnoreCase))
                    return;

                Thread.Sleep(1000);
                var linkaccountElm = driver.FindElementBySelector("div.ui.form div.ui.basic.button", token);
                linkaccountElm.ClickWithTry(token);
                driver.FindElementBySelector("span.name > span.colored", token);
                return;
            });
            ResultHandle(result);
        }

        public static void PayoneerRegisterStartPage(UndetectedChromeDriver driver, CancellationToken token)
        {
            var policy = CreatePolicy(driver, token);
            var result = policy.ExecuteAndCapture(() =>
            {
                if (driver.Url.Contains("Partners/Default", StringComparison.OrdinalIgnoreCase))
                    return;

                Thread.Sleep(1000);
                var paymentMethodElm = driver.FindElementBySelector("span.name > span.colored", token);
                paymentMethodElm.ClickWithTry(token);

                Thread.Sleep(1000);
                var signUpBtnElm = driver.FindElementBySelector("#SignUpButton", token);
                signUpBtnElm.ClickWithTry(token);

                driver.FindElementBySelector("#txtFirstName", token);
                return;
            });
            ResultHandle(result);
        }

        public static void PayoneerRegisterPage1(UndetectedChromeDriver driver, POInfor info, CancellationToken token)
        {
            var policy = CreatePolicy(driver, token);
            var result = policy.ExecuteAndCapture(() =>
            {
                Thread.Sleep(1000);
                var firstNameElm = driver.FindElementBySelector("#txtFirstName", token);
                firstNameElm.SendKeysWithClear(info.GetFirstName(), token);

                Thread.Sleep(1000);
                var lastNameElm = driver.FindElementBySelector("#txtLastName", token);
                lastNameElm.SendKeysWithClear(info.GetLastName(), token);

                Thread.Sleep(1000);
                var emailElm = driver.FindElementBySelector("#txtEmail", token);
                emailElm.SendKeysWithClear(info.Email, token);

                Thread.Sleep(1000);
                var reEmailElm = driver.FindElementBySelector("#txtRetypeEmail", token);
                reEmailElm.SendKeysWithClear(info.Email, token);

                Thread.Sleep(1000);
                var birthDateElm = driver.FindElementBySelector("#datepicker5", token);
                birthDateElm.ClickWithTry(token);

                Thread.Sleep(1000);
                driver.FindElementBySelector("select.ui-datepicker-month", token)
                    .ClickWithTry(token);

                Thread.Sleep(1000);
                var monthBirth = int.Parse(info.GetMonthOfBirth());
                var selectMonthElms = driver.FindElementsBySelector("select.ui-datepicker-month > option", token);
                selectMonthElms.Where(elm => int.Parse(elm.GetAttribute("value")) + 1 == monthBirth)
                    .First().ClickWithTry(token);

                Thread.Sleep(1000);
                driver.FindElementBySelector("select.ui-datepicker-year", token)
                    .ClickWithTry(token);

                Thread.Sleep(1000);
                var yearBirth = int.Parse(info.GetYearOfBirth());
                var selectYearElms = driver.FindElementsBySelector("select.ui-datepicker-year > option", token);
                selectYearElms.Where(elm => int.Parse(elm.GetAttribute("value")) == yearBirth)
                    .First().ClickWithTry(token);

                Thread.Sleep(1000);
                var dateBirth = int.Parse(info.GetDayOfBirth());
                var selectDayElms = driver.FindElementsBySelector("#ui-datepicker-div > table > tbody > tr > td > a", token);
                selectDayElms.Where(elm => int.Parse(elm.Text) == dateBirth)
                    .First().ClickWithTry(token);

                var button = driver.FindElementBySelector("#PersonalDetailsButton", token);
                var condition = ".slider-item.activePage #txtAddress1";
                CaptchaHandle(driver, button, condition, token);
                return;
            });
            ResultHandle(result);
        }

        public static string PayoneerRegisterPage2(UndetectedChromeDriver driver, POInfor info, string simcodeKey, CancellationToken token)
        {
            var policy = CreatePolicy(driver, token);
            var result = policy.ExecuteAndCapture(() =>
            {
                Thread.Sleep(1000);
                var streetElm = driver.FindElementBySelector("#txtAddress1", token);
                streetElm.SendKeysWithClear(info.GetStreetAddress(), token);

                Thread.Sleep(1000);
                var cityElm = driver.FindElementBySelector("#txtCity", token);
                cityElm.SendKeysWithClear(info.GetCity(), token);

                Thread.Sleep(1000);
                var zipcodeElm = driver.FindElementBySelector("#txtZip", token);
                zipcodeElm.SendKeysWithClear(info.ZipCode, token);

                var simcode = SimCodeService.GetSim(simcodeKey, token);
                var sim = simcode.Item2;

                Thread.Sleep(1000);
                var phoneElm = driver.FindElementBySelector("#AccountPhoneNumber_num", token);
                phoneElm.SendKeysWithClear(sim, token);

                Thread.Sleep(1000);
                var sendcodeElm = driver.FindElementBySelector("#lnkSendCode", token);
                sendcodeElm.ClickWithTry(token);

                var code = SimCodeService.GetCode(simcodeKey, simcode.Item1, token);

                Thread.Sleep(1000);
                var codeElm = driver.FindElementBySelector("#txtVerificationCode", token);
                codeElm.SendKeysWithClear(code, token);

                var button = driver.FindElementBySelector("#ContactDetailsButton", token);
                button.ClickWithTry(token);

                driver.FindElementBySelector(".slider-item.activePage #tbPassword", token);
                return sim;
            });
            if (result.FinalException != null)
            {
                throw result.FinalException;
            }
            return result.Result;
        }

        public static void PayoneerRegisterPage3(UndetectedChromeDriver driver, POInfor info, CancellationToken token)
        {
            var policy = CreatePolicy(driver, token);
            var result = policy.ExecuteAndCapture(() =>
            {
                Thread.Sleep(1000);
                var passElm = driver.FindElementBySelector("#tbPassword", token);
                passElm.SendKeysWithClear(info.Password, token);

                Thread.Sleep(1000);
                var repassElm = driver.FindElementBySelector("#tbRetypePassword", token);
                repassElm.SendKeysWithClear(info.Password, token);

                Thread.Sleep(1000);
                driver.FindElementBySelector("#AccountDetails > div.slide-form > div > div.form-object.custom-style > div.field.field-select", token)
                    .ClickWithTry(token);

                Thread.Sleep(1000);
                var questionElms = driver.FindElementsBySelector("#ddlSecurityQuestions > option", token);
                questionElms.Where(item => !item.Text.Contains("Please Select"))
                    .First().ClickWithTry(token);

                Thread.Sleep(1000);
                var answerElm = driver.FindElementBySelector("#tbSecurityAnswer", token);
                answerElm.SendKeysWithClear(info.Password, token);

                Thread.Sleep(1000);
                driver.FindElementBySelector("#ctl00_identityDocumentCollection > div.id-type-fields > div.form-object > div.field.field-select", token)
                    .ClickWithTry(token);

                Thread.Sleep(1000);
                var countryElms = driver.FindElementsBySelector("#ddlIssueCountry_1 > option", token);
                countryElms.Where(elm => elm.GetAttribute("value").Equals("VN"))
                    .First().ClickWithTry(token);

                Thread.Sleep(1000);
                driver.FindElementBySelector("#ctl00_divIdTypeSelection > div > div.field.field-select", token)
                    .ClickWithTry(token);

                Thread.Sleep(1000);
                var identityElms = driver.FindElementsBySelector("#ddlIdentityDocTypes_1 > option", token);
                identityElms.Where(elm => elm.GetAttribute("value").Equals("3"))
                    .First().ClickWithTry(token);

                Thread.Sleep(1000);
                var idCardInputElm = driver.FindElementBySelector("#txtCollectForeignId", token);
                idCardInputElm.SendKeysWithClear(info.IDCard, token);

                Thread.Sleep(1000);
                var button = driver.FindElementBySelector("#AccountDetailsButton", token);
                var condition = ".slider-item.activePage #iframMR";
                CaptchaHandle(driver, button, condition, token);
                return;
            });
            ResultHandle(result);
        }

        public static void PayoneerRegisterPage4(UndetectedChromeDriver driver, POInfor info, CancellationToken token)
        {
            var policy = CreatePolicy(driver, token);
            var result = policy.ExecuteAndCapture(() =>
            {
                if (driver.Url.Contains("dashboard.chipchip"))
                    return;

                var iframe = driver.FindElementBySelector("#iframMR", token);
                driver.SwitchTo().Frame(iframe);

                Thread.Sleep(1000);
                driver.FindElementBySelector("#validationContainer > div:nth-child(5) > div.field.field-select", token)
                    .ClickWithTry(token);

                Thread.Sleep(1000);
                var countryElms = driver.FindElementsBySelector("#ddlCountries > option", token);
                countryElms.Where(elm => elm.GetAttribute("value").Equals("VN"))
                    .First().ClickWithTry(token);

                Thread.Sleep(1000);
                driver.FindElementBySelector("#divNewLayout > div.form-object.field-select.field-select-searchable > " +
                    "div.field.field-select > span.select2.select2-container.select2-container--default > span.selection > span", token)
                    .ClickWithTry(token);

                Thread.Sleep(1000);
                var bankBranchElms = driver.FindElementsBySelector("ul.select2-results__options > li.select2-results__option", token);
                bankBranchElms.Where(elm => elm.Text.Contains(info.GetBankBranchName()))
                    .First().ClickWithTry(token);

                Thread.Sleep(1000);
                var bankPlaceElm = driver.FindElementByName("iachfield44", token);
                bankPlaceElm.SendKeysWithClear($"CN {info.GetCity()}", token);

                Thread.Sleep(1000);
                var nameElm = driver.FindElementByName("iachfield2", token);
                nameElm.SendKeysWithClear(info.Name, token);

                Thread.Sleep(1000);
                var bankNumberElm = driver.FindElementByName("iachfield3", token);
                bankNumberElm.SendKeysWithClear(info.GetBankNumber(), token);

                Thread.Sleep(1000);
                var swiftElm = driver.FindElementByName("iachfield4", token);
                swiftElm.SendKeysWithClear(info.GetBankCode(), token);

                Thread.Sleep(1000);
                var agreeElm1 = driver.FindElementBySelector("#SignDocument_13_2", token);
                agreeElm1.ClickBySendKey(token);

                Thread.Sleep(1000);
                var agreeElm2 = driver.FindElementBySelector("#SignDocument_7", token);
                agreeElm2.ClickBySendKey(token);

                driver.FindElementBySelector("#btnNextPhantomOnly", token)
                    .ClickWithTry(token);

                driver.FindElementBySelector("div.ui.grid > div.twelve.wide.stretched.column", token);
                return;
            });
            ResultHandle(result);
        }

        public static void LoginPayoneer(UndetectedChromeDriver driver, POInfor info, CancellationToken token)
        {
            driver.GoToUrl("https://login.payoneer.com/");
            var policy = CreatePolicy(driver, token);
            var result = policy.ExecuteAndCapture(() =>
            {
                if (!driver.Url.Contains("login.payoneer.com"))
                {
                    return;
                }

                var emailElm = driver.FindElementBySelector("#username", token);
                emailElm.ClickWithTry(token);
                emailElm.SendKeysWithClear(info.Email, 10, token);

                Thread.Sleep(1000);
                var passwordElm = driver.FindElementByName("password", token);
                passwordElm.ClickWithTry(token);
                passwordElm.SendKeysWithClear(info.Password, 10, token);

                Thread.Sleep(1000);
                driver.FindElementBySelector("#login_button", token)
                    .ClickWithTry(token);

                Thread.Sleep(1000);
                if (driver.IsElementDisplayed(By.CssSelector("div.captcha-solver"), token))
                {
                    var button = driver.FindElementBySelector("#login_button", token);
                    var conditon = "div.challenge__form iframe";
                    CaptchaHandle(driver, button, conditon, token);
                }

                Thread.Sleep(1000);
                if (driver.IsElementDisplayed(By.CssSelector("div.challenge__form iframe"), token))
                {
                    var iframe = driver.FindElementBySelector("div.challenge__form iframe", token);
                    driver.SwitchTo().Frame(iframe);

                    Thread.Sleep(1000);
                    driver.FindElementBySelector("#skip-onboarding-button", token)
                        .ClickWithTry(token);
                }

                driver.FindElementBySelector("div.user-details > div.user-name", token);
                return;
            });
            ResultHandle(result);
        }

        public static void VerifyPayoneerEmail(UndetectedChromeDriver driver, POInfor info, CancellationToken token)
        {
            var policy = Policy.Handle<Exception>(e => e.GetType() != typeof(OperationCanceledException))
                .Retry(1, onRetry: (exception, retryCount) =>
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(5000);
                });

            var url = MailHandlerService.GetVerifyUrl(info.Email, info.Password, token);
            var result = policy.ExecuteAndCapture(() =>
            {
                driver.GoToUrl(url);
                if (driver.Url.Contains("www.payoneer.com") || driver.Url.Contains("myaccount.payoneer.com"))
                {
                    return;
                }
                driver.FindElementBySelector("div.validVerification__user-verify-icon", token);
            });
        }

        public static void SecuritySettingsPayoneer(UndetectedChromeDriver driver, POInfor info, CancellationToken token)
        {
            driver.GoToUrl("https://myaccount.payoneer.com/ma/settings/securitysettings");
            var policy = CreatePolicy(driver, token);
            var result = policy.ExecuteAndCapture(() =>
            {
                Thread.Sleep(1000);
                var button = driver.FindElementBySelector(@"#security-questions-wrapper div[data-tid=""edit-button""]", token);
                driver.ClickByJS(button, token);

                Thread.Sleep(1000);
                var question3Elm = driver.FindElementByName("Question3.SecurityAnswer", token);
                question3Elm.SendKeysWithClear(info.Password, token);

                Thread.Sleep(1000);
                var question2Elm = driver.FindElementByName("Question2.SecurityAnswer", token);
                question2Elm.SendKeysWithClear(info.Password, token);

                Thread.Sleep(1000);
                var question1Elm = driver.FindElementByName("Question1.SecurityAnswer", token);
                question1Elm.SendKeysWithClear(info.Password, token);

                Thread.Sleep(1000);
                driver.FindElementBySelector(@"form.security-questions-form button[type=""submit""]", token)
                    .ClickWithTry(token);

                Thread.Sleep(1000);
                var iframe = driver.FindElementBySelector(@"iframe[title=""auth-payoneer""]", token);
                driver.SwitchTo().Frame(iframe);

                var code = MailHandlerService.GetVerifyCode(info.Email, info.Password, token);

                Thread.Sleep(1000);
                var codeElm = driver.FindElementBySelector("#email-code-textbox", token);
                codeElm.SendKeysWithClear(code, token);

                driver.FindElementBySelector("#send-code-button", token)
                    .ClickWithTry(token);

                driver.SwitchTo().Window(driver.WindowHandles[0]);
                driver.FindElementBySelector(@"#security-questions-wrapper div[data-tid=""edit-button""]", token);
                return;
            });
            ResultHandle(result);
        }

        public static void SendKYCDocument(UndetectedChromeDriver driver, POInfor infor, CancellationToken token)
        {
            driver.GoToUrl("https://myaccount.payoneer.com/ma/settings/fdc/pending-requirements");
            var policy = CreatePolicy(driver, token);
            var result = policy.ExecuteAndCapture(() =>
            {
                Thread.Sleep(1000);
                var button = driver.FindElementBySelector(@"button[data-testid=""link--requirementTypeId-1""]", token);
                driver.ClickByJS(button, token);

                Thread.Sleep(1000);
                var optionElm = driver.FindElementBySelector("#radio-button_subRequirementTypeId_3", token);
                driver.ClickByJS(optionElm, token);

                Thread.Sleep(1000);
                var fileInputElm = driver.FindElementBySelector(@"div.fileSelectorV1 input[type=""file""]", token);

                string currentDir = Directory.GetCurrentDirectory();
                var files = Directory.GetFiles($@"{currentDir}\IDCards\{infor.Email}");
                var frontImg = files.First(item => item.Contains("front", StringComparison.OrdinalIgnoreCase));
                var backImg = files.First(item => item.Contains("back", StringComparison.OrdinalIgnoreCase));

                Thread.Sleep(1000);
                fileInputElm.SendKeys(frontImg);

                Thread.Sleep(1000);
                fileInputElm.SendKeys(backImg);

                Thread.Sleep(1000);
                driver.FindElementBySelector("form > div.sub-requirement-content__footer > button", token)
                    .ClickWithTry(token);

                driver.FindElementBySelector("div.wrapper > div.confirmation-wizard-success__icon-wrapper > svg", token);
                return;
            });
            ResultHandle(result);
        }
    }
}