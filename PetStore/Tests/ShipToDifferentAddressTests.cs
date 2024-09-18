using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;
using PetStore.utilities;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("ShipToDifferentAddressTests")]
    public class ShipToDifferentAddressTests : BaseTests
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
        public void ShiptoDifferentAddress()
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
        public void VerifyThatAllDetailsAreSameAsMyAccountDetails(string category)
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
                    string shipToDifferentAddressPageFirstName = shipToDifferentAddressPage.GetFirstName();
                    string shipToDifferentAddressPageLastName = shipToDifferentAddressPage.GetLastName();
                    string shipToDifferentAddressPageAddress1 = shipToDifferentAddressPage.GetAddress1();
                    string shipToDifferentAddressPageAddress2 = shipToDifferentAddressPage.GetAddress2();
                    string shipToDifferentAddressPageCity = shipToDifferentAddressPage.GetCity();
                    string shipToDifferentAddressPageState = shipToDifferentAddressPage.GetState();
                    string shipToDifferentAddressPageZip = shipToDifferentAddressPage.GetZip();
                    string shipToDifferentAddressPageCountry = shipToDifferentAddressPage.GetCountry();

                    commonPage.ClickMyAccountLink();
                    string accountPageFirstName = accountPage.GetFirstName();
                    string accountPageLastName = accountPage.GetLastName();
                    string accountPageAddress1 = accountPage.GetAddress1();
                    string accountPageAddress2 = accountPage.GetAddress2();
                    string accountPageCity = accountPage.GetCity();
                    string accountPageState = accountPage.GetState();
                    string accountPageZip = accountPage.GetZip();
                    string accountPageCountry = accountPage.GetCountry();

                    Assert.AreEqual(accountPageFirstName, shipToDifferentAddressPageFirstName, "First names do not match.");
                    Assert.AreEqual(accountPageLastName, shipToDifferentAddressPageLastName, "Last names do not match.");
                    Assert.AreEqual(accountPageAddress1, shipToDifferentAddressPageAddress1, "Address1 values do not match.");
                    Assert.AreEqual(accountPageAddress2, shipToDifferentAddressPageAddress2, "Address2 values do not match.");
                    Assert.AreEqual(accountPageCity, shipToDifferentAddressPageCity, "City values do not match.");
                    Assert.AreEqual(accountPageState, shipToDifferentAddressPageState, "State values do not match.");
                    Assert.AreEqual(accountPageZip, shipToDifferentAddressPageZip, "Zip codes do not match.");
                    Assert.AreEqual(accountPageCountry, shipToDifferentAddressPageCountry, "Country values do not match.");

                    Driver.Back();
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
        public void VerifyThatDataCanBeEnteredInAllTheFields(string category)
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
                    shipToDifferentAddressPage.EnterFirstName("John");
                    shipToDifferentAddressPage.EnterLastName("Doe");
                    shipToDifferentAddressPage.EnterAddress1("123 Main St");
                    shipToDifferentAddressPage.EnterAddress2("Apt 4B");
                    shipToDifferentAddressPage.EnterCity("New York");
                    shipToDifferentAddressPage.EnterState("NY");
                    shipToDifferentAddressPage.EnterZip("10001");
                    shipToDifferentAddressPage.EnterCountry("USA");


                    string shipToDifferentAddressPageFirstName = shipToDifferentAddressPage.GetFirstName();
                    string shipToDifferentAddressPageLastName = shipToDifferentAddressPage.GetLastName();
                    string shipToDifferentAddressPageAddress1 = shipToDifferentAddressPage.GetAddress1();
                    string shipToDifferentAddressPageAddress2 = shipToDifferentAddressPage.GetAddress2();
                    string shipToDifferentAddressPageCity = shipToDifferentAddressPage.GetCity();
                    string shipToDifferentAddressPageState = shipToDifferentAddressPage.GetState();
                    string shipToDifferentAddressPageZip = shipToDifferentAddressPage.GetZip();
                    string shipToDifferentAddressPageCountry = shipToDifferentAddressPage.GetCountry();

                    Assert.AreEqual("John", shipToDifferentAddressPageFirstName, "First names do not match.");
                    Assert.AreEqual("Doe", shipToDifferentAddressPageLastName, "Last names do not match.");
                    Assert.AreEqual("123 Main St", shipToDifferentAddressPageAddress1, "Address1 values do not match.");
                    Assert.AreEqual("Apt 4B", shipToDifferentAddressPageAddress2, "Address2 values do not match.");
                    Assert.AreEqual("New York", shipToDifferentAddressPageCity, "City values do not match.");
                    Assert.AreEqual("NY", shipToDifferentAddressPageState, "State values do not match.");
                    Assert.AreEqual("10001", shipToDifferentAddressPageZip, "Zip codes do not match.");
                    Assert.AreEqual("USA", shipToDifferentAddressPageCountry, "Country values do not match.");

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
        public void VerifyThatContinueButtonIsWorking(string category)
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
                    shipToDifferentAddressPage.ClickOnContinueButton();
                    string orderText = placeOrderPage.GetOrderText();
                    Assert.IsTrue(orderText.Contains("Please confirm the information below and then press continue..."));

                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }
    }
}
