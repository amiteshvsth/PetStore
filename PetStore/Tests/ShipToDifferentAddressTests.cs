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
                var subCatPageUrl = Driver.Url;
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.AreEqual(item, itemsPage.GetSubCategory());

                List<string> itemIds = itemsPage.GetAllItemIDs();
                foreach (var itemId in itemIds)
                {
                    var itemPageUrl = Driver.Url;
                    itemsPage.ClickOnItemByItemID(itemId);
                    Assert.AreEqual(itemId, itemDetailsPage.GetItemId());

                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    cartPage.ClickProceedToCheckoutButton();
                    Assert.IsTrue(Driver.Url.Contains("newOrderForm="));

                    paymentPage.ClickOnShipToDifferentAddress();
                    paymentPage.ClickOnContinueButton();
                    Assert.AreEqual("Shipping Address", shipToDifferentAddressPage.GetHeading());

                    var shipToDifferentAddressDetails = new
                    {
                        FirstName = shipToDifferentAddressPage.GetFirstName(),
                        LastName = shipToDifferentAddressPage.GetLastName(),
                        Address1 = shipToDifferentAddressPage.GetAddress1(),
                        Address2 = shipToDifferentAddressPage.GetAddress2(),
                        City = shipToDifferentAddressPage.GetCity(),
                        State = shipToDifferentAddressPage.GetState(),
                        Zip = shipToDifferentAddressPage.GetZip(),
                        Country = shipToDifferentAddressPage.GetCountry()
                    };

                    commonPage.ClickMyAccountLink();
                    var accountPageDetails = new
                    {
                        FirstName = accountPage.GetFirstName(),
                        LastName = accountPage.GetLastName(),
                        Address1 = accountPage.GetAddress1(),
                        Address2 = accountPage.GetAddress2(),
                        City = accountPage.GetCity(),
                        State = accountPage.GetState(),
                        Zip = accountPage.GetZip(),
                        Country = accountPage.GetCountry()
                    };

                    Assert.AreEqual(accountPageDetails.FirstName, shipToDifferentAddressDetails.FirstName, "First names do not match.");
                    Assert.AreEqual(accountPageDetails.LastName, shipToDifferentAddressDetails.LastName, "Last names do not match.");
                    Assert.AreEqual(accountPageDetails.Address1, shipToDifferentAddressDetails.Address1, "Address1 values do not match.");
                    Assert.AreEqual(accountPageDetails.Address2, shipToDifferentAddressDetails.Address2, "Address2 values do not match.");
                    Assert.AreEqual(accountPageDetails.City, shipToDifferentAddressDetails.City, "City values do not match.");
                    Assert.AreEqual(accountPageDetails.State, shipToDifferentAddressDetails.State, "State values do not match.");
                    Assert.AreEqual(accountPageDetails.Zip, shipToDifferentAddressDetails.Zip, "Zip codes do not match.");
                    Assert.AreEqual(accountPageDetails.Country, shipToDifferentAddressDetails.Country, "Country values do not match.");

                    Driver.NavigateTo(itemPageUrl);
                }
                Driver.NavigateTo(subCatPageUrl);
            }
        }


        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatDataCanBeEnteredInAllTheFields(string category)
        {
            commonPage.NavigateToCategory(category);
            var subCatNames = productCategoryPage.GetAllSubCategories();

            foreach (var subCategory in subCatNames)
            {
                var subCatPageUrl = Driver.Url;
                productCategoryPage.ClickOnSubCategoryByProductName(subCategory);
                Assert.AreEqual(subCategory, itemsPage.GetSubCategory());

                var itemIds = itemsPage.GetAllItemIDs();
                foreach (var itemId in itemIds)
                {
                    var itemPageUrl = Driver.Url;
                    itemsPage.ClickOnItemByItemID(itemId);
                    Assert.AreEqual(itemId, itemDetailsPage.GetItemId());

                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    cartPage.ClickProceedToCheckoutButton();
                    Assert.IsTrue(Driver.Url.Contains("newOrderForm="));

                    paymentPage.ClickOnShipToDifferentAddress();
                    paymentPage.ClickOnContinueButton();
                    Assert.AreEqual("Shipping Address", shipToDifferentAddressPage.GetHeading());

                    // Define expected values
                    var expectedAddressDetails = new
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Address1 = "123 Main St",
                        Address2 = "Apt 4B",
                        City = "New York",
                        State = "NY",
                        Zip = "10001",
                        Country = "USA"
                    };

                    // Enter address details
                    shipToDifferentAddressPage.EnterFirstName(expectedAddressDetails.FirstName);
                    shipToDifferentAddressPage.EnterLastName(expectedAddressDetails.LastName);
                    shipToDifferentAddressPage.EnterAddress1(expectedAddressDetails.Address1);
                    shipToDifferentAddressPage.EnterAddress2(expectedAddressDetails.Address2);
                    shipToDifferentAddressPage.EnterCity(expectedAddressDetails.City);
                    shipToDifferentAddressPage.EnterState(expectedAddressDetails.State);
                    shipToDifferentAddressPage.EnterZip(expectedAddressDetails.Zip);
                    shipToDifferentAddressPage.EnterCountry(expectedAddressDetails.Country);

                    // Retrieve actual address details and compare
                    var actualAddressDetails = new
                    {
                        FirstName = shipToDifferentAddressPage.GetFirstName(),
                        LastName = shipToDifferentAddressPage.GetLastName(),
                        Address1 = shipToDifferentAddressPage.GetAddress1(),
                        Address2 = shipToDifferentAddressPage.GetAddress2(),
                        City = shipToDifferentAddressPage.GetCity(),
                        State = shipToDifferentAddressPage.GetState(),
                        Zip = shipToDifferentAddressPage.GetZip(),
                        Country = shipToDifferentAddressPage.GetCountry()
                    };

                    Assert.AreEqual(expectedAddressDetails.FirstName, actualAddressDetails.FirstName, "First names do not match.");
                    Assert.AreEqual(expectedAddressDetails.LastName, actualAddressDetails.LastName, "Last names do not match.");
                    Assert.AreEqual(expectedAddressDetails.Address1, actualAddressDetails.Address1, "Address1 values do not match.");
                    Assert.AreEqual(expectedAddressDetails.Address2, actualAddressDetails.Address2, "Address2 values do not match.");
                    Assert.AreEqual(expectedAddressDetails.City, actualAddressDetails.City, "City values do not match.");
                    Assert.AreEqual(expectedAddressDetails.State, actualAddressDetails.State, "State values do not match.");
                    Assert.AreEqual(expectedAddressDetails.Zip, actualAddressDetails.Zip, "Zip codes do not match.");
                    Assert.AreEqual(expectedAddressDetails.Country, actualAddressDetails.Country, "Country values do not match.");

                    Driver.NavigateTo(itemPageUrl);
                }
                Driver.NavigateTo(subCatPageUrl);
            }
        }


        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatContinueButtonIsWorking(string category)
        {
            commonPage.NavigateToCategory(category);
            var subCatNames = productCategoryPage.GetAllSubCategories();

            foreach (var subCategory in subCatNames)
            {
                var subCatPageUrl = Driver.Url;
                productCategoryPage.ClickOnSubCategoryByProductName(subCategory);
                Assert.AreEqual(subCategory, itemsPage.GetSubCategory());

                var itemIds = itemsPage.GetAllItemIDs();
                foreach (var itemId in itemIds)
                {
                    var itemPageUrl = Driver.Url;
                    itemsPage.ClickOnItemByItemID(itemId);
                    Assert.AreEqual(itemId, itemDetailsPage.GetItemId());

                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    cartPage.ClickProceedToCheckoutButton();
                    Assert.IsTrue(Driver.Url.Contains("newOrderForm="));

                    paymentPage.ClickOnShipToDifferentAddress();
                    paymentPage.ClickOnContinueButton();
                    Assert.AreEqual("Shipping Address", shipToDifferentAddressPage.GetHeading());

                    shipToDifferentAddressPage.ClickOnContinueButton();
                    var orderText = placeOrderPage.GetOrderText();
                    Assert.IsTrue(orderText.Contains("Please confirm the information below and then press continue..."));

                    // Navigate back to the item and subcategory pages
                    Driver.NavigateTo(itemPageUrl);
                }
                Driver.NavigateTo(subCatPageUrl);
            }
        }

    }
}
