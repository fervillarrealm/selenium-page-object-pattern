using NUnit.Framework;
using TestingPageObjectPattern.Common.Enums;
using TestingPageObjectPattern.Testing_EndToEnd.Extensions;
using StringAssert = Microsoft.VisualStudio.TestTools.UnitTesting.StringAssert;

namespace TestingPageObjectPattern.Testing_EndToEnd.Controllers
{
    [TestFixture]
    public class HomeControllerTest : TestBase
    {
        [Test(Author = "Fernando Villarreal", Description = "Prueba de inicio de sesión")]
        [System.ComponentModel.Category("Selenium"), TestCase(Drivers.Chrome), TestCase(Drivers.Firefox), TestCase(Drivers.Ie)]
        public void IniciarSesion(Drivers browser)
        {
            CustomWebDriver.Initialize(browser);
            CustomWebDriver.WaitUntilPreloaderFinish();

            //Pages.SiteHome.OpenLoginModal();
            //Pages.SiteHome.InputBadPassword();
            //Pages.SiteHome.CloseLoginModal();
            //Pages.SiteHome.InputCorrectPassword();
            //Pages.SiteHome.PageCapturarSolicitud();

            StringAssert.Contains(CustomWebDriver.Title, "DZone");
        }
    }
}
