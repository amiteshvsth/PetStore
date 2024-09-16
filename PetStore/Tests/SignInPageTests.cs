using Newtonsoft.Json;
using OpenQA.Selenium;
using PetStore.Base;
using PetStore.DataObject;
using PetStore.Pages;
using PetStore.Pages.Common;
using PetStore.utilities;
using System.IO;
using System.Linq;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("SignInPageTest")]
    public class SignInPageTests : BaseTests
    {
        private SignInPO signInPage;
        private CommonPO commonPage;
        private WelcomePO welcomePage;
        private SignInList signInData;

        private const string SignInDataPath = "C:\\Users\\amitesh\\source\\repos\\PetStore\\PetStore\\DataFactory\\SignInData.json";
        private const string InvalidSignInDataPath = "C:\\Users\\amitesh\\source\\repos\\PetStore\\PetStore\\DataFactory\\InvalidSignInData.json";

        [TestInitialize]
        public void TestSetup()
        {
            commonPage = new CommonPO(Driver);
            signInPage = new SignInPO(Driver);
            welcomePage = new WelcomePO(Driver);
            signInData = LoadSignInData(SignInDataPath);
        }

        private SignInList LoadSignInData(string path)
        {
            return JsonConvert.DeserializeObject<SignInList>(File.ReadAllText(path));
        }

        private void NavigateToSignInPage()
        {
            Driver.NavigateTo(PetStoreUrl);
            commonPage.ClickSignInLink();
        }

        [TestMethod]
        public void VerifySignInFunctionalityForMultipleUsersWithValidData()
        {
            foreach (var user in signInData.SignInDataModels)
            {
                NavigateToSignInPage();
                signInPage.SignInUser(user.UserId, user.Password);
                welcomePage.SignOut();
            }
        }

        [TestMethod]
        [DataRow("mAQyo6677")] // Invalid Password
        [DataRow("HEzmosdfs5344")] // Invalid Username
        [DataRow("7mCn75890")] // Empty Password
        [DataRow("")] // Empty Username
        [DataRow(" ")] // Space as Username
        public void VerifyThatErrorIsDisplayedForInvalidData(string userId)
        {
            var invalidSignInData = LoadSignInData(InvalidSignInDataPath);
            var userDataModel = invalidSignInData.SignInDataModels.FirstOrDefault(user => user.UserId == userId);

            if (userDataModel == null)
            {
                Assert.Fail("User data model not found for the given userId.");
                return;
            }

            NavigateToSignInPage();
            signInPage.SignInUser(userId, userDataModel.Password);
            IWebElement errorElement = Driver.FindElement(signInPage.errorMessage);
            Assert.IsNotNull(errorElement, "The expected error message is not displayed.");
        }
    }
}
