using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;
using PetStore.utilities;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("PaymentPageTests")]
    public class PaymentPageTests : BaseTests
    {
        private SignInPO signInPage;
        private CommonPO commonPage;
        private ProductCategoryPO productCategoryPage;
        private ItemsPO itemsPage;
        private ItemDetailsPO itemDetailsPage;
        private CartPO cartPage;
        private PaymentPO paymentPage;
        private MyAccountPO accountPage;
        private ShipToDifferentAddressPO shipToDifferentAddressPage;
        private PlaceOrderPO placeOrderPage;

        [TestInitialize]
        public void PaymentPageTest()
        {
            InitializePageObjects();

            Driver.NavigateTo(PetStoreUrl);
            commonPage.ClickSignInLink();
            signInPage.SignInUser("mAQyo6677", "lqYFkDr0WD");
        }

        private void InitializePageObjects()
        {
            signInPage = new SignInPO(Driver);
            productCategoryPage = new ProductCategoryPO(Driver);
            itemsPage = new ItemsPO(Driver);
            itemDetailsPage = new ItemDetailsPO(Driver);
            commonPage = new CommonPO(Driver);
            cartPage = new CartPO(Driver);
            paymentPage = new PaymentPO(Driver);
            accountPage = new MyAccountPO(Driver);
            shipToDifferentAddressPage = new ShipToDifferentAddressPO(Driver);
            placeOrderPage = new PlaceOrderPO(Driver);
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatBillingAddressIsCorrect(string category)
        {
            ProcessItemsInCategory(category, VerifyBillingAddress);
        }

        private void VerifyBillingAddress(string itemId, string itemPageUrl)
        {
            AddItemToCartAndProceedToCheckout();

            var paymentPageDetails = GetAddressDetailsFromPaymentPage();
            commonPage.ClickMyAccountLink();
            var accountPageDetails = GetAddressDetailsFromAccountPage();

            VerifyAddressDetails(paymentPageDetails, accountPageDetails);

            Driver.NavigateTo(itemPageUrl);
        }

        private (string firstName, string lastName, string address1, string address2, string city, string state, string zip, string country)
        GetAddressDetailsFromPaymentPage() => (
            paymentPage.GetFirstName(),
            paymentPage.GetLastName(),
            paymentPage.GetAddress1(),
            paymentPage.GetAddress2(),
            paymentPage.GetCity(),
            paymentPage.GetState(),
            paymentPage.GetZip(),
            paymentPage.GetCountry()
        );

        private (string firstName, string lastName, string address1, string address2, string city, string state, string zip, string country)
        GetAddressDetailsFromAccountPage() => (
            accountPage.GetFirstName(),
            accountPage.GetLastName(),
            accountPage.GetAddress1(),
            accountPage.GetAddress2(),
            accountPage.GetCity(),
            accountPage.GetState(),
            accountPage.GetZip(),
            accountPage.GetCountry()
        );

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatShipToDifferentAddressIsClickable(string category)
        {
            ProcessItemsInCategory(category, VerifyShipToDifferentAddress);
        }

        private void VerifyShipToDifferentAddress(string itemId, string itemPageUrl)
        {
            AddItemToCartAndProceedToCheckout();

            paymentPage.ClickOnShipToDifferentAddress();
            paymentPage.ClickOnContinueButton();
            Assert.IsTrue(shipToDifferentAddressPage.GetHeading().Equals("Shipping Address"));

            Driver.NavigateTo(itemPageUrl);
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatContinueButtonIsClickable(string category)
        {
            ProcessItemsInCategory(category, VerifyContinueButton);
        }

        private void VerifyContinueButton(string itemId, string itemPageUrl)
        {
            AddItemToCartAndProceedToCheckout();

            paymentPage.ClickOnContinueButton();
            Assert.IsTrue(placeOrderPage.GetOrderText().Contains("Please confirm the information below and then press continue..."));

            Driver.NavigateTo(itemPageUrl);
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatDefaultInformationCanBeChanged(string category)
        {
            ProcessItemsInCategory(category, ChangeAndVerifyPaymentDetails);
        }

        private void ChangeAndVerifyPaymentDetails(string itemId, string itemPageUrl)
        {
            AddItemToCartAndProceedToCheckout();

            paymentPage.SelectCardType("MasterCard");
            paymentPage.EnterCardNumber("00008838239");
            paymentPage.EnterExpiryDate("10/05");
            paymentPage.EnterFirstName("Amitesh");
            paymentPage.EnterLastName("Rawal");
            paymentPage.EnterAddress1("nikol");
            paymentPage.EnterAddress2("jignect");
            paymentPage.EnterCity("odhav");
            paymentPage.EnterState("gujarat");
            paymentPage.EnterZip("398080");
            paymentPage.EnterCountry("India");

            AssertPaymentDetails("MasterCard", "00008838239", "10/05", "Amitesh", "Rawal", "nikol", "jignect", "odhav", "gujarat", "398080", "India");

            Driver.NavigateTo(itemPageUrl);
        }

        private void AssertPaymentDetails(
            string expectedCardType, string expectedCardNumber, string expectedExpiryDate,
            string expectedFirstName, string expectedLastName, string expectedAddress1, string expectedAddress2,
            string expectedCity, string expectedState, string expectedZip, string expectedCountry)
        {
            Assert.AreEqual(paymentPage.GetCardType(), expectedCardType);
            Assert.AreEqual(paymentPage.GetCardNumber(), expectedCardNumber);
            Assert.AreEqual(paymentPage.GetExpiryDate(), expectedExpiryDate);
            Assert.AreEqual(paymentPage.GetFirstName(), expectedFirstName);
            Assert.AreEqual(paymentPage.GetLastName(), expectedLastName);
            Assert.AreEqual(paymentPage.GetAddress1(), expectedAddress1);
            Assert.AreEqual(paymentPage.GetAddress2(), expectedAddress2);
            Assert.AreEqual(paymentPage.GetCity(), expectedCity);
            Assert.AreEqual(paymentPage.GetState(), expectedState);
            Assert.AreEqual(paymentPage.GetZip(), expectedZip);
            Assert.AreEqual(paymentPage.GetCountry(), expectedCountry);
        }

        private void AddItemToCartAndProceedToCheckout()
        {
            itemDetailsPage.ClickOnAddToCartDetailsPage();
            cartPage.ClickProceedToCheckoutButton();
            Assert.IsTrue(Driver.Url.Contains("newOrderForm="));
        }

        private void ProcessItemsInCategory(string category, Action<string, string> actionPerItem)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();

            foreach (var subCategory in subCatNames)
            {
                string subCategoryPageUrl = Driver.Url;
                productCategoryPage.ClickOnSubCategoryByProductName(subCategory);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(subCategory));

                List<string> itemIds = itemsPage.GetAllItemIDs();
                foreach (var itemId in itemIds)
                {
                    string itemPageUrl = Driver.Url;
                    itemsPage.ClickOnItemByItemID(itemId);
                    Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));

                    actionPerItem(itemId, itemPageUrl);
                }

                Driver.NavigateTo(subCategoryPageUrl);
            }
        }

        private static void VerifyAddressDetails(
            (string firstName, string lastName, string address1, string address2, string city, string state, string zip, string country) paymentPageDetails,
            (string firstName, string lastName, string address1, string address2, string city, string state, string zip, string country) accountPageDetails)
        {
            Assert.AreEqual(accountPageDetails.firstName, paymentPageDetails.firstName, "First names do not match.");
            Assert.AreEqual(accountPageDetails.lastName, paymentPageDetails.lastName, "Last names do not match.");
            Assert.AreEqual(accountPageDetails.address1, paymentPageDetails.address1, "Address1 values do not match.");
            Assert.AreEqual(accountPageDetails.address2, paymentPageDetails.address2, "Address2 values do not match.");
            Assert.AreEqual(accountPageDetails.city, paymentPageDetails.city, "City values do not match.");
            Assert.AreEqual(accountPageDetails.state, paymentPageDetails.state, "State values do not match.");
            Assert.AreEqual(accountPageDetails.zip, paymentPageDetails.zip, "Zip codes do not match.");
            Assert.AreEqual(accountPageDetails.country, paymentPageDetails.country, "Country values do not match.");
        }
    }
}
