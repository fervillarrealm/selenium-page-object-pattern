using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestingPageObjectPattern.Testing_EndToEnd.PageObjects
{
    public abstract class PageObject
    {
        public WebDriverWait Wait;
        public IAlert Alert;
    }
}
