using OpenQA.Selenium;
using System;

namespace TestingPageObjectPattern.Testing_EndToEnd.Extensions
{
    public class ExpectedConditions
    {
        public static Func<IWebDriver, bool> ElementIsVisible(IWebElement element)
        {
            return (driver) =>
            {
                try
                {
                    return element.Displayed;
                }
                catch (Exception)
                {
                    return false;
                }
            };
        }

        public static Func<IWebDriver, bool> ElementIsNotVisible(IWebElement element)
        {
            return (driver) =>
            {
                try
                {
                    return !element.Displayed;
                }
                catch (Exception)
                {
                    return false;
                }
            };
        }
    }
}
