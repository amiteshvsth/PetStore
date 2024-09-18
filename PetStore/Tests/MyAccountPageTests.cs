using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;
using PetStore.utilities;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("MyAccountPageTests")]
    public class MyAccountPageTests : BaseTests
    {
        SignInPO signInPage;
        CommonPO commonPage;
        MyAccountPO myAccountPage;

        [TestInitialize]
        public void MyAccountPage()
        {
            signInPage = new SignInPO(Driver);
            commonPage = new CommonPO(Driver);
            myAccountPage = new MyAccountPO(Driver);
            Driver.NavigateTo(PetStoreUrl);
            commonPage.ClickSignInLink();
            signInPage.SignInUser("mAQyo6677", "lqYFkDr0WD");
            commonPage.ClickMyAccountLink();
        }

        [TestMethod]
        public void VerifyThatUserIdIsCorrect()
        {

            string userId = myAccountPage.GetUserId();
            Assert.IsTrue(userId.Equals("mAQyo6677"));
        }

        [TestMethod]
        public void VerifyThatFirstNameIsCorrect()
        {
            string firstName = myAccountPage.GetFirstName();
            Assert.IsTrue(firstName.Contains("RwOnrA"));
        }

        [TestMethod]
        public void VerifyThatWeCanEditDataInMyAccountsPage()
        {
            myAccountPage.EnterFirstName("Amitesh");
            myAccountPage.EnterLastName("Rawal");
            myAccountPage.EnterEmail("amitesh@gmail.com");
            myAccountPage.EnterPhone("9352473062");
            myAccountPage.EnterAddress1("nikol");
            myAccountPage.EnterAddress2("balaji");
            myAccountPage.EnterCity("ahmedabad");
            myAccountPage.EnterState("gujarat");
            myAccountPage.EnterZip("307026");
            myAccountPage.SelectCountry("India");
            myAccountPage.SelectLanguagePreference("english");
            myAccountPage.SelectFavouriteCategory("DOGS");
            bool isEnableMyListChecked = myAccountPage.IsEnableMyListChecked();
            myAccountPage.SetEnableMyList(isEnableMyListChecked);
            // Determine the action to take based on the checkbox state
            bool isEnableMyBannerChecked = myAccountPage.IsEnableMyBannerChecked();
            myAccountPage.SetEnableMyBanner(isEnableMyBannerChecked);

            myAccountPage.SetEnableMyList(true);
            myAccountPage.SetEnableMyBanner(false);
            myAccountPage.ClickSubmitButton();

            string myAccountPageFirstName = myAccountPage.GetFirstName();
            string myAccountPageLastName = myAccountPage.GetLastName();
            string myAccountPageEmail = myAccountPage.GetEmail();
            string myAccountPagePhone = myAccountPage.GetPhone();
            string myAccountPageAddress1 = myAccountPage.GetAddress1();
            string myAccountPageAddress2 = myAccountPage.GetAddress2();
            string myAccountPageCity = myAccountPage.GetCity();
            string myAccountPageState = myAccountPage.GetState();
            string myAccountPageZip = myAccountPage.GetZip();
            string myAccountPageCountry = myAccountPage.GetCountry();
            string myAccountPageLanguagePreference = myAccountPage.GetLanguagePreference();
            string myAccountPageFavouriteCategory = myAccountPage.GetFavouriteCategory();
            bool myAccountPageEnableMyList = myAccountPage.IsEnableMyListChecked();
            bool myAccountPageEnableMyBanner = myAccountPage.IsEnableMyBannerChecked();

            // Assert that the values entered match the values retrieved
            Assert.AreEqual("Amitesh", myAccountPageFirstName, "First names do not match.");
            Assert.AreEqual("Rawal", myAccountPageLastName, "Last names do not match.");
            Assert.AreEqual("amitesh@gmail.com", myAccountPageEmail, "Email addresses do not match.");
            Assert.AreEqual("9352473062", myAccountPagePhone, "Phone numbers do not match.");
            Assert.AreEqual("nikol", myAccountPageAddress1, "Address1 values do not match.");
            Assert.AreEqual("balaji", myAccountPageAddress2, "Address2 values do not match.");
            Assert.AreEqual("ahmedabad", myAccountPageCity, "Cities do not match.");
            Assert.AreEqual("gujarat", myAccountPageState, "States do not match.");
            Assert.AreEqual("307026", myAccountPageZip, "Zip codes do not match.");
            Assert.AreEqual("India", myAccountPageCountry, "Countries do not match.");
            Assert.AreEqual("english", myAccountPageLanguagePreference, "Language preferences do not match.");
            Assert.AreEqual("DOGS", myAccountPageFavouriteCategory, "Favourite categories do not match.");
            Assert.AreEqual(true, myAccountPageEnableMyList, "My List checkbox state does not match.");
            Assert.AreEqual(false, myAccountPageEnableMyBanner, "My Banner checkbox state does not match.");


        }

        [TestMethod]
        public void VerifythatMyOrdersLinkIsClickable()
        {
            myAccountPage.ClickMyOrdersLink();
            Assert.IsTrue(Driver.Url.Equals("https://petstore.octoperf.com/actions/Order.action?listOrders="));
        }
    }
}
