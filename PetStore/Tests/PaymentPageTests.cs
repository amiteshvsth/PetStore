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
        SignInPO signInPage;
        CommonPO commonPage;
        ProductCategoryPO productCategoryPage;
        ItemsPO itemsPage;
        ItemDetailsPO itemDetailsPage;
        CartPO cartPage;
        PaymentPO paymentPage;
        MyAccountPO accountPage;
        ShipToDifferentAddressPO shipToDifferentAddressPage;
        PlaceOrderPO placeOrderPage;
        [TestInitialize]
        public void PaymentPageTest()
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

            Driver.NavigateTo(PetStoreUrl);
            commonPage.ClickSignInLink();
            signInPage.SignInUser("mAQyo6677", "lqYFkDr0WD");
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatBillingAddressIsCorrect(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemIds = itemsPage.GetAllItemIDs();
                foreach (var itemId in itemIds)
                {
                    itemsPage.ClickOnItemByItemID(itemId);
                    Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));

                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    cartPage.ClickProceedToCheckoutButton();
                    Assert.IsTrue(Driver.Url.Contains("newOrderForm="));
                    string paymentPagefirstName = paymentPage.GetFirstName();
                    string paymentPagelastName = paymentPage.GetLastName();
                    string paymentPageaddress1 = paymentPage.GetAddress1();
                    string paymentPageaddress2 = paymentPage.GetAddress2();
                    string paymentPagecity = paymentPage.GetCity();
                    string paymentPagestate = paymentPage.GetState();
                    string paymentPagezip = paymentPage.GetZip();
                    string paymentPagecountry = paymentPage.GetCountry();
                    commonPage.ClickMyAccountLink();
                    string accountPageFirstName = accountPage.GetFirstName();
                    string accountPageLastName = accountPage.GetLastName();
                    string accountPageAddress1 = accountPage.GetAddress1();
                    string accountPageAddress2 = accountPage.GetAddress2();
                    string accountPageCity = accountPage.GetCity();
                    string accountPageState = accountPage.GetState();
                    string accountPageZip = accountPage.GetZip();
                    string accountPageCountry = accountPage.GetCountry();

                    Assert.AreEqual(accountPageFirstName, paymentPagefirstName, "First names do not match.");
                    Assert.AreEqual(accountPageLastName, paymentPagelastName, "Last names do not match.");
                    Assert.AreEqual(accountPageAddress1, paymentPageaddress1, "Address1 values do not match.");
                    Assert.AreEqual(accountPageAddress2, paymentPageaddress2, "Address2 values do not match.");
                    Assert.AreEqual(accountPageCity, paymentPagecity, "City values do not match.");
                    Assert.AreEqual(accountPageState, paymentPagestate, "State values do not match.");
                    Assert.AreEqual(accountPageZip, paymentPagezip, "Zip codes do not match.");
                    Assert.AreEqual(accountPageCountry, paymentPagecountry, "Country values do not match.");


                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatShipToDifferentAddressIsClickable(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemIds = itemsPage.GetAllItemIDs();
                foreach (var itemId in itemIds)
                {
                    itemsPage.ClickOnItemByItemID(itemId);
                    Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));

                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    cartPage.ClickProceedToCheckoutButton();
                    Assert.IsTrue(Driver.Url.Contains("newOrderForm="));
                    paymentPage.ClickOnShipToDifferentAddress();
                    paymentPage.ClickOnContinueButton();
                    Assert.IsTrue(shipToDifferentAddressPage.GetHeading().Equals("Shipping Address"));

                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatContinueButtonIsClickable(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemIds = itemsPage.GetAllItemIDs();
                foreach (var itemId in itemIds)
                {
                    itemsPage.ClickOnItemByItemID(itemId);
                    Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));

                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    cartPage.ClickProceedToCheckoutButton();
                    Assert.IsTrue(Driver.Url.Contains("newOrderForm="));
                    paymentPage.ClickOnContinueButton();
                    string orderText = placeOrderPage.GetOrderText();
                    Assert.IsTrue(orderText.Contains("Please confirm the information below and then press continue..."));

                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatDefaultInformationCanBeChanged(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemIds = itemsPage.GetAllItemIDs();
                foreach (var itemId in itemIds)
                {
                    itemsPage.ClickOnItemByItemID(itemId);
                    Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));

                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    cartPage.ClickProceedToCheckoutButton();
                    Assert.IsTrue(Driver.Url.Contains("newOrderForm="));
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

                    Assert.AreEqual(paymentPage.GetCardType(), "MasterCard");
                    Assert.AreEqual(paymentPage.GetCardNumber(), "00008838239");
                    Assert.AreEqual(paymentPage.GetExpiryDate(), "10/05");
                    Assert.AreEqual(paymentPage.GetFirstName(), "Amitesh");
                    Assert.AreEqual(paymentPage.GetLastName(), "Rawal");
                    Assert.AreEqual(paymentPage.GetAddress1(), "nikol");
                    Assert.AreEqual(paymentPage.GetAddress2(), "jignect");
                    Assert.AreEqual(paymentPage.GetCity(), "odhav");
                    Assert.AreEqual(paymentPage.GetState(), "gujarat");
                    Assert.AreEqual(paymentPage.GetZip(), "398080");
                    Assert.AreEqual(paymentPage.GetCountry(), "India");

                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }
    }
}
