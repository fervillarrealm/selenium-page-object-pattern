using NUnit.Framework;

namespace TestingPageObjectPattern.Testing_EndToEnd.Extensions
{
    [TestFixture]
    public class TestBase
    {
        public const string RootUrl = "https://dzone.com/";

        [TearDown]
        public static void TearDown()
        {
            CustomWebDriver.Close();
        }
    }
}
