using OpenQA.Selenium;
using PetStore.Base;

namespace PetStore.Pages
{
    internal class PlaceOrderPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By confirmBtn = By.LinkText("Confirm");
        private readonly By returnToMainMenu = By.LinkText("Return to Main Menu");
        private readonly By timeOfPurchase = By.XPath("(//font/b)[2]");
        private readonly By billingFirstName = By.XPath("(//td[text()='First name:'])[1]/following-sibling::td");
        private readonly By billingLastName = By.XPath("(//td[text()='Last name:'])[1]/following-sibling::td");
        private readonly By billingAddress1 = By.XPath("(//td[text()='Address 1:'])[1]/following-sibling::td");
        private readonly By billingAddress2 = By.XPath("(//td[text()='Address 2:'])[1]/following-sibling::td");
        private readonly By billingCity = By.XPath("(//td[text()='City:'])[1]/following-sibling::td");
        private readonly By billingState = By.XPath("(//td[text()='State:'])[1]/following-sibling::td");
        private readonly By billingZip = By.XPath("(//td[text()='Zip:'])[1]/following-sibling::td");
        private readonly By billingCountry = By.XPath("(//td[text()='Country:'])[1]/following-sibling::td");

        private readonly By shippingFirstName = By.XPath("(//td[text()='First name:'])[2]/following-sibling::td");
        private readonly By shippingLastName = By.XPath("(//td[text()='Last name:'])[2]/following-sibling::td");
        private readonly By shippingAddress1 = By.XPath("(//td[text()='Address 1:'])[2]/following-sibling::td");
        private readonly By shippingAddress2 = By.XPath("(//td[text()='Address 2:'])[2]/following-sibling::td");
        private readonly By shippingCity = By.XPath("(//td[text()='City:'])[2]/following-sibling::td");
        private readonly By shippingState = By.XPath("(//td[text()='State:'])[2]/following-sibling::td");
        private readonly By shippingZip = By.XPath("(//td[text()='Zip:'])[2]/following-sibling::td");
        private readonly By shippingCountry = By.XPath("(//td[text()='Country:'])[2]/following-sibling::td");

        public string GetBillingFirstName()
        {
            string billFirstName = Wait.UntilElementVisible(billingFirstName).Text;
            return billFirstName;
        }
        public string GetBillingLastName()
        {
            string billLastName = Wait.UntilElementVisible(billingLastName).Text;
            return billLastName;
        }

        public string GetBillingAddress1()
        {
            string billAddress1 = Wait.UntilElementVisible(billingAddress1).Text;
            return billAddress1;
        }

        public string GetBillingAddress2()
        {
            string billAddress2 = Wait.UntilElementVisible(billingAddress2).Text;
            return billAddress2;
        }

        public string GetBillingCity()
        {
            string billCity = Wait.UntilElementVisible(billingCity).Text;
            return billCity;
        }

        public string GetBillingState()
        {
            string billState = Wait.UntilElementVisible(billingState).Text;
            return billState;
        }

        public string GetBillingZip()
        {
            string billZip = Wait.UntilElementVisible(billingZip).Text;
            return billZip;
        }

        public string GetBillingCountry()
        {
            string billCountry = Wait.UntilElementVisible(billingCountry).Text;
            return billCountry;
        }
        public string GetTimeOfPurchase()
        {
            string top = Wait.UntilElementVisible(timeOfPurchase).Text;
            return top;
        }

        public string GetShippingFirstName()
        {
            string shipFirstName = Wait.UntilElementVisible(shippingFirstName).Text;
            return shipFirstName;
        }

        public string GetShippingLastName()
        {
            string shipLastName = Wait.UntilElementVisible(shippingLastName).Text;
            return shipLastName;
        }

        public string GetShippingAddress1()
        {
            string shipAddress1 = Wait.UntilElementVisible(shippingAddress1).Text;
            return shipAddress1;
        }

        public string GetShippingAddress2()
        {
            string shipAddress2 = Wait.UntilElementVisible(shippingAddress2).Text;
            return shipAddress2;
        }

        public string GetShippingCity()
        {
            string shipCity = Wait.UntilElementVisible(shippingCity).Text;
            return shipCity;
        }

        public string GetShippingState()
        {
            string shipState = Wait.UntilElementVisible(shippingState).Text;
            return shipState;
        }

        public string GetShippingZip()
        {
            string shipZip = Wait.UntilElementVisible(shippingZip).Text;
            return shipZip;
        }

        public string GetShippingCountry()
        {
            string shipCountry = Wait.UntilElementVisible(shippingCountry).Text;
            return shipCountry;
        }
        public void ClickOnConfirmButton()
        {
            Wait.UntilElementClickable(confirmBtn).Click();
        }

        public void ClickOnReturnToMainMenuButton()
        {
            Wait.UntilElementClickable(returnToMainMenu).Click();
        }
    }
}
