using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;
using PetStore.utilities;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("PlaceOrderTests")]
    public class PlaceOrderTests : BaseTests
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
        ThankYouPO thankYouPage;

        [TestInitialize]
        public void PlaceOrder()
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
            thankYouPage = new ThankYouPO(Driver);

            Driver.NavigateTo(PetStoreUrl);
            commonPage.ClickSignInLink();
            signInPage.SignInUser("mAQyo6677", "lqYFkDr0WD");
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatAllDetailsAreSameAsFilledPreviously(string category)
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
                    paymentPage.ClickOnContinueButton();

                    string placeOrderPageBillingFirstName = placeOrderPage.GetBillingFirstName();
                    string placeOrderPageBillingLastName = placeOrderPage.GetBillingLastName();
                    string placeOrderPageBillingAddress1 = placeOrderPage.GetBillingAddress1();
                    string placeOrderPageBillingAddress2 = placeOrderPage.GetBillingAddress2();
                    string placeOrderPageBillingCity = placeOrderPage.GetBillingCity();
                    string placeOrderPageBillingState = placeOrderPage.GetBillingState();
                    string placeOrderPageBillingZip = placeOrderPage.GetBillingZip();
                    string placeOrderPageBillingCountry = placeOrderPage.GetBillingCountry();

                    string placeOrderPageShippingFirstName = placeOrderPage.GetShippingFirstName();
                    string placeOrderPageShippingLastName = placeOrderPage.GetShippingLastName();
                    string placeOrderPageShippingAddress1 = placeOrderPage.GetShippingAddress1();
                    string placeOrderPageShippingAddress2 = placeOrderPage.GetShippingAddress2();
                    string placeOrderPageShippingCity = placeOrderPage.GetShippingCity();
                    string placeOrderPageShippingState = placeOrderPage.GetShippingState();
                    string placeOrderPageShippingZip = placeOrderPage.GetShippingZip();
                    string placeOrderPageShippingCountry = placeOrderPage.GetShippingCountry();

                    // Assert that payment page and billing page values are equal
                    Assert.AreEqual(paymentPagefirstName, placeOrderPageBillingFirstName, "First names do not match between payment page and billing page.");
                    Assert.AreEqual(paymentPagelastName, placeOrderPageBillingLastName, "Last names do not match between payment page and billing page.");
                    Assert.AreEqual(paymentPageaddress1, placeOrderPageBillingAddress1, "Address1 values do not match between payment page and billing page.");
                    Assert.AreEqual(paymentPageaddress2, placeOrderPageBillingAddress2, "Address2 values do not match between payment page and billing page.");
                    Assert.AreEqual(paymentPagecity, placeOrderPageBillingCity, "City values do not match between payment page and billing page.");
                    Assert.AreEqual(paymentPagestate, placeOrderPageBillingState, "State values do not match between payment page and billing page.");
                    Assert.AreEqual(paymentPagezip, placeOrderPageBillingZip, "Zip codes do not match between payment page and billing page.");
                    Assert.AreEqual(paymentPagecountry, placeOrderPageBillingCountry, "Country values do not match between payment page and billing page.");

                    // Assert that payment page and shipping page values are equal
                    Assert.AreEqual(paymentPagefirstName, placeOrderPageShippingFirstName, "First names do not match between payment page and shipping page.");
                    Assert.AreEqual(paymentPagelastName, placeOrderPageShippingLastName, "Last names do not match between payment page and shipping page.");
                    Assert.AreEqual(paymentPageaddress1, placeOrderPageShippingAddress1, "Address1 values do not match between payment page and shipping page.");
                    Assert.AreEqual(paymentPageaddress2, placeOrderPageShippingAddress2, "Address2 values do not match between payment page and shipping page.");
                    Assert.AreEqual(paymentPagecity, placeOrderPageShippingCity, "City values do not match between payment page and shipping page.");
                    Assert.AreEqual(paymentPagestate, placeOrderPageShippingState, "State values do not match between payment page and shipping page.");
                    Assert.AreEqual(paymentPagezip, placeOrderPageShippingZip, "Zip codes do not match between payment page and shipping page.");
                    Assert.AreEqual(paymentPagecountry, placeOrderPageShippingCountry, "Country values do not match between payment page and shipping page.");

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
        public void VerifyThatConfirmButtonIsClickable(string category)
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
                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    cartPage.ClickProceedToCheckoutButton();
                    paymentPage.ClickOnContinueButton();
                    placeOrderPage.ClickOnConfirmButton();
                    string thankYouText = thankYouPage.GetThankYouText();
                    Assert.AreEqual(thankYouText, "Thank you, your order has been submitted.");

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
