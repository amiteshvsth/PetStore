using PetStore.Pages.Common;
using PetStore.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using PetStore.Base;
using PetStore.utilities;

namespace PetStore.Tests
{

    [TestClass]
    [TestCategory("ThankyouPageTests")]
    public class ThankyouPageTests:BaseTests
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
                    string paymentPagefirstName = paymentPage.GetFirstName();
                    string paymentPagelastName = paymentPage.GetLastName();
                    string paymentPageaddress1 = paymentPage.GetAddress1();
                    string paymentPageaddress2 = paymentPage.GetAddress2();
                    string paymentPagecity = paymentPage.GetCity();
                    string paymentPagestate = paymentPage.GetState();
                    string paymentPagezip = paymentPage.GetZip();
                    string paymentPagecountry = paymentPage.GetCountry();
                    paymentPage.ClickOnContinueButton();
                    placeOrderPage.ClickOnConfirmButton();
                    string thankYouText = thankYouPage.GetThankYouText();
                    Assert.AreEqual(thankYouText, "Thank you, your order has been submitted.");

                    string thankYouPageBillingFirstName = thankYouPage.GetBillingFirstName();
                    string thankYouPageBillingLastName = thankYouPage.GetBillingLastName();
                    string thankYouPageBillingAddress1 = thankYouPage.GetBillingAddress1();
                    string thankYouPageBillingAddress2 = thankYouPage.GetBillingAddress2();
                    string thankYouPageBillingCity = thankYouPage.GetBillingCity();
                    string thankYouPageBillingState = thankYouPage.GetBillingState();
                    string thankYouPageBillingZip = thankYouPage.GetBillingZip();
                    string thankYouPageBillingCountry = thankYouPage.GetBillingCountry();

                    string thankYouPageShippingFirstName = thankYouPage.GetShippingFirstName();
                    string thankYouPageShippingLastName = thankYouPage.GetShippingLastName();
                    string thankYouPageShippingAddress1 = thankYouPage.GetShippingAddress1();
                    string thankYouPageShippingAddress2 = thankYouPage.GetShippingAddress2();
                    string thankYouPageShippingCity = thankYouPage.GetShippingCity();
                    string thankYouPageShippingState = thankYouPage.GetShippingState();
                    string thankYouPageShippingZip = thankYouPage.GetShippingZip();
                    string thankYouPageShippingCountry = thankYouPage.GetShippingCountry();

                    // Assert that thank you page billing and payment page values are equal
                    Assert.AreEqual(paymentPagefirstName, thankYouPageBillingFirstName, "Billing first names do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPagelastName, thankYouPageBillingLastName, "Billing last names do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPageaddress1, thankYouPageBillingAddress1, "Billing address1 values do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPageaddress2, thankYouPageBillingAddress2, "Billing address2 values do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPagecity, thankYouPageBillingCity, "Billing city values do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPagestate, thankYouPageBillingState, "Billing state values do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPagezip, thankYouPageBillingZip, "Billing zip codes do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPagecountry, thankYouPageBillingCountry, "Billing country values do not match between payment page and thank you page.");

                    // Assert that thank you page shipping and payment page values are equal
                    Assert.AreEqual(paymentPagefirstName, thankYouPageShippingFirstName, "Shipping first names do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPagelastName, thankYouPageShippingLastName, "Shipping last names do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPageaddress1, thankYouPageShippingAddress1, "Shipping address1 values do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPageaddress2, thankYouPageShippingAddress2, "Shipping address2 values do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPagecity, thankYouPageShippingCity, "Shipping city values do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPagestate, thankYouPageShippingState, "Shipping state values do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPagezip, thankYouPageShippingZip, "Shipping zip codes do not match between payment page and thank you page.");
                    Assert.AreEqual(paymentPagecountry, thankYouPageShippingCountry, "Shipping country values do not match between payment page and thank you page.");

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
        public void VerifyThatReturnToMainMenuLinkIsWorking(string category)
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
                    string paymentPagefirstName = paymentPage.GetFirstName();
                    string paymentPagelastName = paymentPage.GetLastName();
                    string paymentPageaddress1 = paymentPage.GetAddress1();
                    string paymentPageaddress2 = paymentPage.GetAddress2();
                    string paymentPagecity = paymentPage.GetCity();
                    string paymentPagestate = paymentPage.GetState();
                    string paymentPagezip = paymentPage.GetZip();
                    string paymentPagecountry = paymentPage.GetCountry();
                    paymentPage.ClickOnContinueButton();
                    placeOrderPage.ClickOnConfirmButton();
                    string thankYouText = thankYouPage.GetThankYouText();
                    Assert.AreEqual(thankYouText, "Thank you, your order has been submitted.");

                    thankYouPage.ClickReturntoMainMenuBtn();
                    Assert.IsTrue(Driver.Url.Equals("https://petstore.octoperf.com/actions/Catalog.action"));

                    Driver.Back();
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
