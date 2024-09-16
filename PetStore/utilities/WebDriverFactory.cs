using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace PetStore.utilities
{
    public class WebDriverFactory
    {
        private IWebDriver _driver;

        public IWebDriver InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.SetPreference("browser.download.dir", new FileUtil().GetDownloadPath());
                    firefoxOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    _driver = new FirefoxDriver(firefoxOptions);
                    break;

                case "chrome":
                    _driver = new ChromeDriver();
                    break;

                case "safari":
                    _driver = new SafariDriver();
                    break;
            }

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(180);
            _driver.Manage().Window.Maximize();
            return _driver;
        }

        public void CloseBrowser()
        {
            if (_driver != null)
            {
                try
                {
                    _driver.Close();
                    _driver.Quit();
                }
                catch
                {
                    _driver.Quit();
                }

            }
        }
    }
}
