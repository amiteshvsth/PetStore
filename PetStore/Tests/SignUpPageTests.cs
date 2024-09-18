using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;
using PetStore.utilities;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("SignUpPageTest")]
    public class SignUpPageTests : BaseTests
    {
        private SignUpPO signUpPage;
        private CommonPO commonPage;
        private SignInPO signInPage;
        private WelcomePO welcomePage;

        [TestInitialize]
        public void TestSetup()
        {
            commonPage = new CommonPO(Driver);
            signInPage = new SignInPO(Driver);
            signUpPage = new SignUpPO(Driver);
            welcomePage = new WelcomePO(Driver);
        }

        [TestMethod]
        public void VerifySignUpFunctionalityWithRandomData()
        {
            Driver.NavigateTo(PetStoreUrl);
            commonPage.ClickSignInLink();
            signInPage.ClickRegisterNowButton();
            var result = signUpPage.SignUpUser();
            commonPage.ClickSignInLink();
            signInPage.SignInUser(result.UserId, result.Password);
            Assert.IsNotNull(welcomePage.GetFirstName());
        }
    }
}
