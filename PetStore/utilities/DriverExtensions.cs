using OpenQA.Selenium;
using System.Linq;

namespace PetStore.utilities
{
    public static class DriverExtensions
    {
        public static void NavigateTo(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static void RefreshPage(this IWebDriver driver)
        {
            try
            {
                driver.Navigate().Refresh();
            }
            catch //If there is a WebDriverException, attempt refreshing page again.
            {
                driver.Navigate().Refresh();
            }
        }

        public static void Back(this IWebDriver driver)
        {
            driver.Navigate().Back();
        }

        public static string GetCurrentUrl(this IWebDriver driver)
        {

            return driver.Url;

        }

        public static string GetPageTitle(this IWebDriver driver)
        {
            return driver.Title.Trim();
        }

        public static void TakeScreenshot(this IWebDriver driver, string filepath)
        {
            var ssdriver = driver as ITakesScreenshot;
            var screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(filepath+".Png");
        }

        public static void SwitchToIframe(this IWebDriver driver, IWebElement element)
        {
            driver.SwitchTo().Frame(element);
        }

        public static void SwitchToDefaultIframe(this IWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }

        public static void AcceptAlert(this IWebDriver driver)
        {
            driver.SwitchTo().Alert().Accept();
        }

        public static IWebDriver SelectWindowByTitle(this IWebDriver driver, string title)
        {
            foreach (var item in driver.WindowHandles.Where(item => driver.SwitchTo().Window(item).Title.Equals(title)))
            {
                driver.SwitchTo().Window(item);
                break;
            }

            return driver;
        }

        public static void SwitchToLastWindow(this IWebDriver driver)
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        public static void SwitchToFirstWindow(this IWebDriver driver)
        {
            driver.SwitchTo().Window(driver.WindowHandles.First());
        }

        public static void SelectWindowByIndex(this IWebDriver driver, int index)
        {
            var windows = driver.WindowHandles;
            driver.SwitchTo().Window(windows[index]);
        }

        public static IList<string> GetTextFromAllElements(this IWebDriver driver, By locator)
        {
            IList<IWebElement> elements = driver.FindElements(locator);

            return elements.Select(e => e.GetText()).ToList();

        }
    }
}
