using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestingPageObjectPattern.Testing_EndToEnd.PageObjects
{
    public class SiteHomePage
    {
        #region Elements

        [FindsBy(How = How.CssSelector, Using = "button.navbar-toggle.collapsed")]
        [CacheLookup]
        private IWebElement MenuToggleElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = "li#iniciar.iniciar-sesion a")]
        [CacheLookup]
        private IWebElement LinkIniciarSesionElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#registrar [href]")]
        [CacheLookup]
        private IWebElement LinkRegistrarseElement { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='selectedCase']//label[@title='Si Deseas Un Prestamo, Has Clic Aquí']")]
        [CacheLookup]
        private IWebElement BotonSolicitarPrestamoElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[class] [class=\"col-md-12 collapse\"]:nth-child(5) [type=\"email\"]")]
        [CacheLookup]
        private IWebElement TextCorreoElectronicoElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[class=\"col-md-12 \"] [type=\"text\"]")]
        [CacheLookup]
        private IWebElement TextUserNameElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[class=\"col-md-12 \"] .form-group:nth-of-type(3) [type=\"password\"]")]
        [CacheLookup]
        private IWebElement TextContrasenaElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[class] .form-group:nth-of-type(4) input")]
        [CacheLookup]
        private IWebElement TextContrasenaConfirmarElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = "[type=\"checkbox\"]")]
        [CacheLookup]
        private IWebElement CheckAceptarTerminosElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".recaptcha-checkbox-checkmark")]
        [CacheLookup]
        private IWebElement RecaptchaElement { get; set; }


        [FindsBy(How = How.CssSelector, Using = "div#modal-iniciar-sesion.modal-content input.form-control[type='text']")]
        [CacheLookup]
        private IWebElement EmailInputElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div#modal-iniciar-sesion.modal-content input.form-control[type='password']")]
        [CacheLookup]
        private IWebElement PasswordInputElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div#modal-iniciar-sesion.modal-content input.btn.btn-prolecta.btn-block[type='submit']")]
        [CacheLookup]
        private IWebElement SubmitButtonElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button.swal2-confirm.swal2-styled")]
        [CacheLookup]
        private IWebElement ButtonModalElement { get; set; }

        [FindsBy(How = How.XPath, Using = "/html//div[@id='main-wrapper']/footer[@class='footer']/div/div[1]//div[@class='row']/div[1]/ul[@class='menu-footer']//a[@href='/Home/Invierte#como-funciona-inv']")]
        [CacheLookup]
        private IWebElement FooterComoFuncionaElement { get; set; }

        [FindsBy(How = How.XPath, Using = "/html//div[@id='main-wrapper']/footer[@class='footer']/div/div//div[@class='row']/div/ul[@class='menu-footer']//a[@href='/Home/Invierte']")]
        [CacheLookup]
        private IWebElement FooterSimulaTuInversionElement { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".footer [class=\"col-md-3 col-sm-3 col-xs-12\"]:nth-of-type(1) [href=\"\\/Home\\/InformacionDeTasas\"]")]
        [CacheLookup]
        private IWebElement FooterInformacionTasasElement { get; set; }

        #endregion

        public void OpenLoginModal()
        {
            CustomWebDriver.WaitUntilElementIsClickeable(MenuToggleElement);
            MenuToggleElement.Click();
            CustomWebDriver.WaitUntilElementIsClickeable(LinkIniciarSesionElement);
            LinkIniciarSesionElement.Click();
        }

        public void OpenRegisterModal()
        {
            CustomWebDriver.WaitUntilElementIsClickeable(MenuToggleElement);
            MenuToggleElement.Click();
            CustomWebDriver.WaitUntilElementIsClickeable(LinkRegistrarseElement);
            LinkRegistrarseElement.Click();
        }

        public void InputBadPassword()
        {
            EmailInputElement.SendKeys("darigazz" + "\u0040" + "gmail.com");
            PasswordInputElement.SendKeys("Finreal0");
            SubmitButtonElement.Submit();
        }

        public void CloseLoginModal()
        {
            ButtonModalElement.Click();
        }

        public void InputCorrectPassword()
        {
            EmailInputElement.Clear();
            EmailInputElement.SendKeys("darigazz" + "\u0040" + "gmail.com");
            PasswordInputElement.Clear();
            PasswordInputElement.SendKeys("Finreal0=");
            SubmitButtonElement.Submit();
        }

        public void PageCapturarSolicitud()
        {
            CustomWebDriver.WaitUntilElementIsDisplayed(By.Id("Preloader"), 1);
        }


        public void FooterLinkComoFunciona()
        {
            CustomWebDriver.ScrollToElement(FooterComoFuncionaElement);
            FooterComoFuncionaElement.Click();
            CustomWebDriver.WaitUntilPreloaderFinish();
        }

        public void FooterLinkSimulaInversion()
        {
            CustomWebDriver.ScrollToElement(FooterSimulaTuInversionElement);
            FooterSimulaTuInversionElement.Click();
            CustomWebDriver.WaitUntilPreloaderFinish();
        }

        public void FooterLinkInformacionTasas()
        {
            CustomWebDriver.ScrollToElement(FooterInformacionTasasElement);
            FooterInformacionTasasElement.Click();
            CustomWebDriver.WaitUntilPreloaderFinish();
        }
    }
}
