using OpenQA.Selenium;
using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("WelcomePageTests")]
    public class WelcomePageTests : BaseTests
    {
        private CommonPO _commonPage;
        private WelcomePO _welcomePage;
        private SignInPO _signInPage;

        [TestInitialize]
        public void TestSetup()
        {
            _commonPage = new CommonPO(Driver);
            _welcomePage = new WelcomePO(Driver);
            _signInPage = new SignInPO(Driver);
            Driver.Navigate().GoToUrl(PetStoreUrl);
        }

        [TestMethod]
        public void VerifyCorrectUsernameDisplayedOnWelcomePage()
        {
            _commonPage.ClickSignInLink();
            _signInPage.SignInUser("mAQyo6677", "lqYFkDr0WD");
            string firstName = _welcomePage.GetFirstName();
            Assert.AreEqual("Welcome RwOnrA!", firstName);
        }

        [TestMethod]
        public void VerifyThatSignOutButtonIsWorking()
        {
            _commonPage.ClickSignInLink();
            _signInPage.SignInUser("mAQyo6677", "lqYFkDr0WD");
            string firstName = _welcomePage.GetFirstName();
            Assert.AreEqual("Welcome RwOnrA!", firstName);
            _welcomePage.SignOut();
            _commonPage.ClickSignInLink();
            Assert.IsTrue(Driver.Url.Equals("https://petstore.octoperf.com/actions/Account.action?signonForm="));
        }
    }
}
