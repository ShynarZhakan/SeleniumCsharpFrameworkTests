using EaFramework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace EaFramework.Driver
{
    public class DriverFixture : IDriverFixture, IDisposable
    {
        private readonly TestSettings _testSettings;
        public IWebDriver Driver { get; }
        public DriverFixture(TestSettings testSettings)
        {
            _testSettings = testSettings;
            Driver = GetDriverType(_testSettings.BrowserType);
            Driver.Navigate().GoToUrl(_testSettings.ApplicationUrl);
        }


        private IWebDriver GetDriverType(BrowserType browserType)
        {
            return browserType switch
            {
                BrowserType.Chrome => new ChromeDriver(),
                BrowserType.Firefox => new FirefoxDriver(),
                BrowserType.EdgeChromium => new EdgeDriver(),
                _ => new ChromeDriver(),
            };
        }

        public void Dispose()
        {
            Driver.Quit();
        }

        public enum BrowserType
        {
            Chrome,
            Firefox,
            Safari,
            EdgeChromium
        }
    }
}
