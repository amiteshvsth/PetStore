using OpenQA.Selenium;
using PetStore.Pages.Common;
using PetStore.utilities;

namespace PetStore.Base
{
    internal class BasePO(IWebDriver driver)
    {
        protected IWebDriver Driver = driver;
        protected WaitHelpers Wait = new(driver);
        protected ILogger Log;
    }
}
