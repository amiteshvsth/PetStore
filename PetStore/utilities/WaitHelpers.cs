using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace PetStore.utilities
{

    class WaitHelpers
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public WaitHelpers(IWebDriver driver, int timeOut = 60)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
        }

        /// <summary>
        ///  Waits until an element can be found with the supplied locator. The element is not necessarily displayed.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>The located element</returns>
        public IWebElement UntilElementExists(By locator)
        {
            IWebElement element = wait.Until((webDriver) => { return webDriver.FindElement(locator); });

            return element;
        }

        /// <summary>
        /// Waits until the element can be located with the supplied locator, and that the element is displayed and enabled.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>The located element</returns>
        public IWebElement UntilElementClickable(By locator)
        {
            IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            return element;
        }

        /// <summary>
        /// Waits until the element visible.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>The located element</returns>
        public IWebElement UntilElementVisible(By locator)
        {
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return element;
        }


        /// <summary>
        /// Waits until the element invisible.
        /// </summary>
        /// <param name="locator"></param>
        public void UntilElementInVisible(By locator)
        {
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }


        /// <summary>
        /// Locate all the elements that match the locator and return them in a list
        /// </summary>
        /// <param name="locator"></param>
        /// <returns>IList of elements that were located</returns>
        public IList<IWebElement> UntilAllElementsLocated(By locator)
        {
            IList<IWebElement> elements = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));

            return elements;
        }

        /// <summary>
        /// This waits for a specific amount of time. This should be used only if necessary.
        /// </summary>
        /// <param name="milliseconds"></param>
        public void HardWait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }


        public IWebElement WaitIncaseElementClickable(By locator, int duration = 30)
        {
            var localWait = new WebDriverWait(driver, TimeSpan.FromSeconds(duration));

            try
            {
                return localWait.Until(ExpectedConditions.ElementToBeClickable(locator));
            }
            catch
            {
                return null;
            }
        }

        public bool IsElementPresent(By locator, int duration = 5)
        {
            return WaitIncaseElementClickable(locator, duration)?.Displayed ?? false;
        }
    }
}
