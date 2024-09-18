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

            foreach (var subCategoryName in subCatNames)
            {
                VerifySubCategoryDetails(subCategoryName);
            }
        }

        private void VerifySubCategoryDetails(string subCategoryName)
        {
            string subCategoryUrl = Driver.Url;
            productCategoryPage.ClickOnSubCategoryByProductName(subCategoryName);
            Assert.IsTrue(itemsPage.GetSubCategory().Equals(subCategoryName));

            List<string> itemIds = itemsPage.GetAllItemIDs();

            foreach (var itemId in itemIds)
            {
                VerifyItemDetails(itemId);
            }

            Driver.NavigateTo(subCategoryUrl);
        }

        private void VerifyItemDetails(string itemId)
        {
            string itemsPageUrl = Driver.Url;
            itemsPage.ClickOnItemByItemID(itemId);
            Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));

            itemDetailsPage.ClickOnAddToCartDetailsPage();
            cartPage.ClickProceedToCheckoutButton();
            Assert.IsTrue(Driver.Url.Contains("newOrderForm="));

            var paymentDetails = GetPaymentDetails();
            paymentPage.ClickOnContinueButton();

            var billingDetails = GetBillingDetails();
            var shippingDetails = GetShippingDetails();

            AssertDetailsMatch(paymentDetails, billingDetails, "billing");
            AssertDetailsMatch(paymentDetails, shippingDetails, "shipping");

            Driver.NavigateTo(itemsPageUrl);
        }

        private dynamic GetPaymentDetails()
        {
            return new
            {
                FirstName = paymentPage.GetFirstName(),
                LastName = paymentPage.GetLastName(),
                Address1 = paymentPage.GetAddress1(),
                Address2 = paymentPage.GetAddress2(),
                City = paymentPage.GetCity(),
                State = paymentPage.GetState(),
                Zip = paymentPage.GetZip(),
                Country = paymentPage.GetCountry()
            };
        }

        private dynamic GetBillingDetails()
        {
            return new
            {
                FirstName = placeOrderPage.GetBillingFirstName(),
                LastName = placeOrderPage.GetBillingLastName(),
                Address1 = placeOrderPage.GetBillingAddress1(),
                Address2 = placeOrderPage.GetBillingAddress2(),
                City = placeOrderPage.GetBillingCity(),
                State = placeOrderPage.GetBillingState(),
                Zip = placeOrderPage.GetBillingZip(),
                Country = placeOrderPage.GetBillingCountry()
            };
        }

        private dynamic GetShippingDetails()
        {
            return new
            {
                FirstName = placeOrderPage.GetShippingFirstName(),
                LastName = placeOrderPage.GetShippingLastName(),
                Address1 = placeOrderPage.GetShippingAddress1(),
                Address2 = placeOrderPage.GetShippingAddress2(),
                City = placeOrderPage.GetShippingCity(),
                State = placeOrderPage.GetShippingState(),
                Zip = placeOrderPage.GetShippingZip(),
                Country = placeOrderPage.GetShippingCountry()
            };
        }

        private void AssertDetailsMatch(dynamic expected, dynamic actual, string label)
        {
            Assert.AreEqual(expected.FirstName, actual.FirstName, $"First names do not match between payment page and {label} page.");
            Assert.AreEqual(expected.LastName, actual.LastName, $"Last names do not match between payment page and {label} page.");
            Assert.AreEqual(expected.Address1, actual.Address1, $"Address1 values do not match between payment page and {label} page.");
            Assert.AreEqual(expected.Address2, actual.Address2, $"Address2 values do not match between payment page and {label} page.");
            Assert.AreEqual(expected.City, actual.City, $"City values do not match between payment page and {label} page.");
            Assert.AreEqual(expected.State, actual.State, $"State values do not match between payment page and {label} page.");
            Assert.AreEqual(expected.Zip, actual.Zip, $"Zip codes do not match between payment page and {label} page.");
            Assert.AreEqual(expected.Country, actual.Country, $"Country values do not match between payment page and {label} page.");
        }




        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatConfirmButtonIsClickable(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            string subCategoryPageUrl, itemPageUrl;
            foreach (var item in subCatNames)
            {
                subCategoryPageUrl = Driver.Url;
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemIds = itemsPage.GetAllItemIDs();
                foreach (var itemId in itemIds)
                {
                    itemPageUrl = Driver.Url;
                    itemsPage.ClickOnItemByItemID(itemId);
                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    cartPage.ClickProceedToCheckoutButton();
                    paymentPage.ClickOnContinueButton();
                    placeOrderPage.ClickOnConfirmButton();
                    string thankYouText = thankYouPage.GetThankYouText();
                    Assert.AreEqual(thankYouText, "Thank you, your order has been submitted.");

                    Driver.NavigateTo(itemPageUrl);
                }
                Driver.NavigateTo(subCategoryPageUrl);
            }
        }
    }
}
