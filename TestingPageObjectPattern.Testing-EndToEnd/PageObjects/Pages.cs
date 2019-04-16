using OpenQA.Selenium.Support.PageObjects;

namespace TestingPageObjectPattern.Testing_EndToEnd.PageObjects
{
    public static class Pages
    {
        //constraint on a parameter   
        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(CustomWebDriver.Driver, page);

            return page;
        }

        public static SiteHomePage SiteHome => GetPage<SiteHomePage>();
    }
}
