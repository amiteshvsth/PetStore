using OpenQA.Selenium;
using PetStore.Base;
using PetStore.utilities;

namespace PetStore.Pages
{
    internal class WelcomePO(IWebDriver driver) : BasePO(driver)
    {

        private readonly By signOutLink = By.LinkText("Sign Out");
        private readonly By myAccountLink = By.LinkText("My Account");
        private readonly By firstName = By.Id("WelcomeContent");

        public void GoToMyAccountPage()
        {
            Wait.UntilElementClickable(myAccountLink).Click();
        }

        public bool SignOut()
        {
            Wait.UntilElementClickable(signOutLink).Click();
            return true;
        }

        public string GetFirstName()
        {
           return Wait.UntilElementVisible(firstName).GetText();
        }
    }
}
