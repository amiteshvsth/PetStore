using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("WelcomePageTests")]
    public class WelcomePageTests : BaseTests
    {
        private CommonPO commonPage;
        private WelcomePO welcomePage;
        private SignInPO signInPage;

        [TestInitialize]
        public void TestSetup()
        {
            commonPage = new CommonPO(Driver);
            welcomePage = new WelcomePO(Driver);
            signInPage = new SignInPO(Driver);
            Driver.Navigate().GoToUrl(PetStoreUrl);
            commonPage.ClickSignInLink();
            signInPage.SignInUser("mAQyo6677", "lqYFkDr0WD");
        }

        [TestMethod]
        public void VerifyCorrectUsernameDisplayedOnWelcomePage()
        {
            string firstName = welcomePage.GetFirstName();
            Assert.AreEqual("Welcome RwOnrA!", firstName);
        }

        [TestMethod]
        public void VerifyThatSignOutButtonIsWorking()
        {
            welcomePage.SignOut();
            commonPage.ClickSignInLink();
            Assert.IsTrue(Driver.Url.Equals("https://petstore.octoperf.com/actions/Account.action?signonForm="));
        }
    }
}
