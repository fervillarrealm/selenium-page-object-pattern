using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;

namespace TestingPageObjectPattern.Testing_EndToEnd.Extensions
{
    public static class WebDriverExtensions
    {
        public static void Screenshot(this IWebDriver driver)
        {
            var ss = ((ITakesScreenshot)driver).GetScreenshot();
            var filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\_Temp\\" + DateTime.Now.ToString(@"MMM-ddd-d-HH.mm") + ".png";

            byte[] imageBytes = Convert.FromBase64String(ss.ToString());

            using (var bw = new BinaryWriter(new FileStream(filePath, FileMode.Append,
                FileAccess.Write)))
            {
                bw.Write(imageBytes);
                bw.Close();
            }
        }

        public static bool HasElement(this IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            return true;
        }


        public static void WaitForPageToLoad(this IWebDriver driver)
        {
            var timeout = new TimeSpan(0, 0, 30);
            var wait = new WebDriverWait(driver, timeout);

            IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
            if (javascript == null)
                throw new ArgumentException("driver", "Driver must support javascript execution");

            wait.Until((d) =>
            {
                try
                {
                    string readyState = javascript.ExecuteScript(
                        "if (document.readyState) return document.readyState;").ToString();
                    return readyState.ToLower() == "complete";
                }
                catch (InvalidOperationException e)
                {
                    //Window is no longer available
                    return e.Message.ToLower().Contains("unable to get browser");
                }
                catch (WebDriverException e)
                {
                    //Browser is no longer available
                    return e.Message.ToLower().Contains("unable to connect");
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public static bool WaitUntilElementIsPresent(this IWebDriver driver, By by, int timeout = 10)
        {
            for (var i = 0; i < timeout; i++)
            {
                if (driver.ElementIsPresent(by)) return true;
                Thread.Sleep(1000);
            }
            return false;
        }

        public static bool ElementIsPresent(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => (drv.FindElements(by).Count > 0) ? drv.FindElements(by) : null);
            }
            return driver.FindElements(by);
        }
    }
}
