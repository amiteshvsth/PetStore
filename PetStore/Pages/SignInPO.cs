using OpenQA.Selenium;
using PetStore.Base;
using PetStore.utilities;

namespace PetStore.Pages
{
    internal class SignInPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By registerNowButton = By.LinkText("Register Now!");
        private readonly By userNameField = By.Name("username");
        private readonly By passwordField = By.Name("password");
        private readonly By signInButton = By.Name("signon");
        public readonly By errorMessage = By.XPath("//li[contains(text(),'Invalid username or password')]");


        public void SignInUser(string username, string password)
        {
            EnterUserName(username);
            EnterPassword(password);
            ClickSignInButton();
        }

        public void ClickRegisterNowButton()
        {
            Driver.FindElement(registerNowButton).Click();
        }

        public void EnterUserName(string username)
        {
            Wait.UntilElementExists(userNameField).EnterText(username);
        }
        public void EnterPassword(string password)
        {
            Wait.UntilElementExists(passwordField).EnterText(password);
        }

        public void ClickSignInButton()
        {
            Wait.UntilElementClickable(signInButton).Click();
        }
    }
}
