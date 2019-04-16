using OpenQA.Selenium;
using TestingPageObjectPattern.Testing_EndToEnd.PageObjects;
using ExpectedCondition = OpenQA.Selenium.Support.UI.ExpectedConditions;

namespace TestingPageObjectPattern.Testing_EndToEnd.Extensions
{
    public static class PageObjectExtensions
    {
        public static void WaitToLoaderFinish(this PageObject page)
        {
            page.Wait.Until(ExpectedCondition.InvisibilityOfElementLocated(By.Id("preloader")));
        }
        
        public static void WaitForElementToBeClickable(this PageObject page, IWebElement element)
        {
            page.Wait.Until(ExpectedCondition.ElementToBeClickable(element));
        }
    }
}
