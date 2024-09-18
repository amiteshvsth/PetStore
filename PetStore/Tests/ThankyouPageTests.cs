using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;
using PetStore.utilities;

namespace PetStore.Tests
{

    [TestClass]
    [TestCategory("ThankyouPageTests")]
    public class ThankyouPageTests : BaseTests
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
        public void ThankYouPage()
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
        public void VerifyThatAllDetailsAreCorrect(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();

            foreach (var subCat in subCatNames)
            {
                string subCategoryUrl = Driver.Url;
                productCategoryPage.ClickOnSubCategoryByProductName(subCat);

                Assert.IsTrue(itemsPage.GetSubCategory().Equals(subCat));

                List<string> itemIds = itemsPage.GetAllItemIDs();
                foreach (var itemId in itemIds)
                {
                    string itemPageUrl = Driver.Url;
                    itemsPage.ClickOnItemByItemID(itemId);

                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    cartPage.ClickProceedToCheckoutButton();

                    var paymentDetails = new
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

                    paymentPage.ClickOnContinueButton();
                    placeOrderPage.ClickOnConfirmButton();

                    Assert.AreEqual("Thank you, your order has been submitted.", thankYouPage.GetThankYouText(), "Thank you message mismatch");

                    var thankYouBillingDetails = new
                    {
                        FirstName = thankYouPage.GetBillingFirstName(),
                        LastName = thankYouPage.GetBillingLastName(),
                        Address1 = thankYouPage.GetBillingAddress1(),
                        Address2 = thankYouPage.GetBillingAddress2(),
                        City = thankYouPage.GetBillingCity(),
                        State = thankYouPage.GetBillingState(),
                        Zip = thankYouPage.GetBillingZip(),
                        Country = thankYouPage.GetBillingCountry()
                    };

                    var thankYouShippingDetails = new
                    {
                        FirstName = thankYouPage.GetShippingFirstName(),
                        LastName = thankYouPage.GetShippingLastName(),
                        Address1 = thankYouPage.GetShippingAddress1(),
                        Address2 = thankYouPage.GetShippingAddress2(),
                        City = thankYouPage.GetShippingCity(),
                        State = thankYouPage.GetShippingState(),
                        Zip = thankYouPage.GetShippingZip(),
                        Country = thankYouPage.GetShippingCountry()
                    };

                    // Assert that payment page details match billing details on the thank you page
                    AssertDetailsMatch(paymentDetails, thankYouBillingDetails, "Billing");

                    // Assert that payment page details match shipping details on the thank you page
                    AssertDetailsMatch(paymentDetails, thankYouShippingDetails, "Shipping");

                    Driver.NavigateTo(itemPageUrl);
                }
                Driver.NavigateTo(subCategoryUrl);
            }
        }

        // Helper method for asserting that payment details match billing or shipping details
        private static void AssertDetailsMatch(dynamic paymentDetails, dynamic thankYouDetails, string type)
        {
            Assert.AreEqual(paymentDetails.FirstName, thankYouDetails.FirstName, $"{type} first names do not match between payment page and thank you page.");
            Assert.AreEqual(paymentDetails.LastName, thankYouDetails.LastName, $"{type} last names do not match between payment page and thank you page.");
            Assert.AreEqual(paymentDetails.Address1, thankYouDetails.Address1, $"{type} address1 values do not match between payment page and thank you page.");
            Assert.AreEqual(paymentDetails.Address2, thankYouDetails.Address2, $"{type} address2 values do not match between payment page and thank you page.");
            Assert.AreEqual(paymentDetails.City, thankYouDetails.City, $"{type} city values do not match between payment page and thank you page.");
            Assert.AreEqual(paymentDetails.State, thankYouDetails.State, $"{type} state values do not match between payment page and thank you page.");
            Assert.AreEqual(paymentDetails.Zip, thankYouDetails.Zip, $"{type} zip codes do not match between payment page and thank you page.");
            Assert.AreEqual(paymentDetails.Country, thankYouDetails.Country, $"{type} country values do not match between payment page and thank you page.");
        }



        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatReturnToMainMenuLinkIsWorking(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            string subCategoryUrl;
            foreach (var item in subCatNames)
            {
                subCategoryUrl = Driver.Url;
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemIds = itemsPage.GetAllItemIDs();
                string itemPageUrl;
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

                    thankYouPage.ClickReturntoMainMenuBtn();
                    Assert.IsTrue(Driver.Url.Equals("https://petstore.octoperf.com/actions/Catalog.action"));

                    Driver.NavigateTo(itemPageUrl);
                }
                Driver.NavigateTo(subCategoryUrl);
            }
        }
    }
}