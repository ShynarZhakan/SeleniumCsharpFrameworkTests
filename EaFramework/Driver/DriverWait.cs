using EaFramework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EaFramework.Driver
{

    public class DriverWait : IDriverWait
    {
        private readonly TestSettings _testSettings;
        private readonly IDriverFixture _driverFixture;
        private readonly Lazy<WebDriverWait> _waitDriverWait; //Lazy initialization
        public DriverWait(IDriverFixture driverFixture, TestSettings testSettings)
        {
            _testSettings = testSettings;
            _driverFixture = driverFixture;
            _waitDriverWait = new Lazy<WebDriverWait>(GetWaitDriver);
        }

        public IWebElement FindElement(By elementLocator)
        {
            return _waitDriverWait.Value.Until(_ => _driverFixture.Driver.FindElement(elementLocator));
        }

        public IEnumerable<IWebElement> FindElements(By elementLocator)
        {
            return _waitDriverWait.Value.Until(_ => _driverFixture.Driver.FindElements(elementLocator));
        }

        private WebDriverWait GetWaitDriver()
        {
            return new(_driverFixture.Driver, timeout: TimeSpan.FromSeconds(_testSettings.TimeoutInternal ?? 30))
            {
                PollingInterval = TimeSpan.FromSeconds(_testSettings.TimeoutInternal ?? 1)
            };
        }
    }
}
