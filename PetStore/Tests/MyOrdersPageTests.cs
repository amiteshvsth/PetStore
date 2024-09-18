using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;
using PetStore.utilities;
using System.Globalization;

namespace PetStore.Tests
{
    [TestClass]
    public class MyOrdersPageTests : BaseTests
    {
        SignInPO signInPage;
        CommonPO commonPage;
        MyAccountPO myAccountPage;
        MyOrdersPO myOrdersPage;
        [TestInitialize]
        public void MyOrdersPage()
        {
            signInPage = new SignInPO(Driver);
            commonPage = new CommonPO(Driver);
            myAccountPage = new MyAccountPO(Driver);
            myOrdersPage = new MyOrdersPO(Driver);
            Driver.NavigateTo(PetStoreUrl);
            commonPage.ClickSignInLink();
            signInPage.SignInUser("mAQyo6677", "lqYFkDr0WD");
            commonPage.ClickMyAccountLink();
            myAccountPage.ClickMyOrdersLink();
        }

        [TestMethod]
        public void VerifyThatOrderIdIsClickable()
        {
            List<string> orderIds = myOrdersPage.GetAllOrderIds();
            foreach (var orderId in orderIds)
            {
                myOrdersPage.ClickOrderIdByText(orderId);
                Assert.IsTrue(Driver.Url.Contains(orderId));
                Driver.Back();
            }
        }

        [TestMethod]
        public void VerifyThatTotalPriceIsNotZero()
        {
            List<string> orderIds = myOrdersPage.GetAllOrderIds();
            foreach (var orderId in orderIds)
            {
                string orderTotal = myOrdersPage.GetOrderTotalById(orderId);
                Assert.AreNotEqual(orderTotal, "$0.00");
            }
        }

        [TestMethod]
        public void VerifyThatDateIsInCorrectFormat()
        {
            List<string> orderIds = myOrdersPage.GetAllOrderIds();
            foreach (var orderId in orderIds)
            {
                string orderTime = myOrdersPage.GetOrderTimeById(orderId);
                string format = "yyyy/MM/dd HH:mm:ss";

                bool isValidFormat = DateTime.TryParseExact(orderTime, format,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _);

                Assert.IsTrue(isValidFormat, "The date format is invalid.");
            }
        }
    }
}
