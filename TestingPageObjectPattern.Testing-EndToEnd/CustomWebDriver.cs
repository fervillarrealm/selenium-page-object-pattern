using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Threading;
using TestingPageObjectPattern.Common.Enums;


namespace TestingPageObjectPattern.Testing_EndToEnd
{
    public static class CustomWebDriver
    {
        private static readonly string _baseUrl = "https://dzone.com/";
        private static IWebDriver _webDriver;
        public static WebDriverWait Wait;
        public static Actions Actions;
        public static ISearchContext Driver { get { return _webDriver; } }
        public static string Title { get { return _webDriver.Title; } }


        internal static IWebDriver GetDriver(Drivers driver)
        {
            switch (driver)
            {
                case Drivers.Firefox:
                    var optFireFox = new FirefoxOptions { AcceptInsecureCertificates = true };
                    _webDriver = new FirefoxDriver(optFireFox);
                    break;
                case Drivers.Ie:
                    _webDriver = new InternetExplorerDriver();
                    break;
                case Drivers.Chrome:
                    var optChrome = new ChromeOptions { AcceptInsecureCertificates = true };
                    _webDriver = new ChromeDriver(optChrome);
                    break;
                case Drivers.Safari:
                    _webDriver = new SafariDriver();
                    break;
                default:
                    _webDriver = new ChromeDriver();
                    break;
            }

            return _webDriver;
        }


        public static void MoveToElement(IWebElement element)
        {
            Actions.MoveToElement(element).Click().Build().Perform();
        }

        public static void ScrollToElement(IWebElement element)
        {
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void WaitUntilPreloaderFinish()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("preloader")));
        }

        public static void WaitUntilElementIsClickeable(IWebElement element)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public static IWebElement ClickOnInvisibleElement(By by)
        {
            IWebElement element = _webDriver.FindElement(by);
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].hidden = false;", element);
            element.Click();
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].hidden = true;", element);
            return element;
        }

        internal static bool WaitUntilElementIsDisplayed(By element, int timeoutInSeconds)
        {
            for (var i = 0; i < timeoutInSeconds; i++)
            {
                if (ElementIsDisplayed(element))
                {
                    return true;
                }
                Thread.Sleep(1000);
            }
            return false;
        }

        internal static IWebElement FindElement(By by)
        {
            return _webDriver.FindElement(by);
        }

        public static bool ElementIsDisplayed(By element)
        {
            var present = false;
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            try
            {
                present = _webDriver.FindElement(element).Displayed;
            }
            catch (NoSuchElementException)
            {
            }
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return present;
        }

        public static void Initialize(Drivers browser)
        {
            _webDriver = GetDriver(browser);
            _webDriver.Manage().Window.Maximize();
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            Actions = new Actions(_webDriver);
            Goto("");

            //	IE Fix
            if (_webDriver.GetType() == typeof(InternetExplorerDriver) && _webDriver.Title.Contains("certificado"))
                _webDriver.Navigate().GoToUrl("javascript:document.getElementById('overridelink').click()");
        }

        public static void Close()
        {
            _webDriver.Close();
        }

        public static void Goto(string url, bool useBaseUrl = true)
        {
            if (useBaseUrl)
                _webDriver.Navigate().GoToUrl(string.Format("{0}/{1}", _baseUrl, url));
            else
                _webDriver.Navigate().GoToUrl(url);
        }

        public static void Quit()
        {
            _webDriver.Quit();
        }

        public static void IsAt(string url)
        {
            var currentUrl = _webDriver.Url;
            Assert.AreEqual(currentUrl, url, "The current URL does not match the provided URL");
        }


        public static void TakeScreenshot(string testName, string errMsg)
        {
            try
            {
                var ss = ((ITakesScreenshot)_webDriver).GetScreenshot();
                var filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\_Temp\\" + testName + "_" + DateTime.Now.ToString(@"MMM-ddd-d-HH.mm") + ".png";

                byte[] imageBytes = Convert.FromBase64String(ss.ToString());

                using (BinaryWriter bw = new BinaryWriter(new FileStream(filePath, FileMode.Append,
                    FileAccess.Write)))
                {
                    bw.Write(imageBytes);
                    bw.Close();
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

    }
}
