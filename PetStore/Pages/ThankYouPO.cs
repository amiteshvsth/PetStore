using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PetStore.Base;

namespace PetStore.Pages
{
    internal class ThankYouPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By returnToMainMenu = By.LinkText("Return to Main Menu");
        private readonly By cardType = By.XPath("//td[text()='Card Type:']/following-sibling::td");
        private readonly By cardNumber = By.XPath("//td[text()='Card Number:']/following-sibling::td");
        private readonly By expiryDate = By.XPath("//td[text()='Expiry Date (MM/YYYY):']/following-sibling::td");
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


        private readonly By itemId = By.XPath("(//table)[2]/tbody/tr/td[1]");
        private readonly By itemName = By.XPath("(//table)[2]/tbody/tr/td[2]");

        private static By ItemIdByName(string name) => By.XPath($"//td/a[text()='{name}']");
        private static By ItemIdByIndex(int idx) => By.XPath($"((//table)[2]/tbody/tr/td[1])[{idx}]");
        private readonly By orderIdAndTime = By.XPath("//th[contains(text(),'Order')]");
        private readonly By totalPrice = By.XPath("//th[contains(text(), 'Total:')]");

        private static By QuantityByItemName(string item) => By.XPath($"//td[text()='{item}']/following-sibling::td[1]");
        private static By QuantityByItemId(string id) => By.XPath($"//td/a[text()='{id}']/parent::td/following-sibling::td[2]");
        private static By QuantityByItemIndex(int index) => By.XPath($"((//table)[2]/tbody/tr/td[3])[{index}]");

        private static By ListPriceByItemName(string item) => By.XPath($"//td[text()='{item}']/following-sibling::td[2]");
        private static By ListPriceByItemId(string id) => By.XPath($"//td/a[text()='{id}']/parent::td/following-sibling::td[3]");
        private static By ListPriceByItemIndex(int index) => By.XPath($"((//table)[2]/tbody/tr/td[4])[{index}]");
        private static By TotalPriceByItemIndex(int index) => By.XPath($"((//table)[2]/tbody/tr/td[5])[{index}]");
        private static By TotalPriceByItemId(string id) => By.XPath($"//td/a[text()='{id}']/parent::td/following-sibling::td[4]");
        private static By TotalPriceByItemName(string item) => By.XPath($"//td[text()='{item}']/following-sibling::td[3]");

        public void ClickItemIdByName(string name)
        {
            Wait.UntilElementExists(ItemIdByName(name)).Click();
        }

        public string GetItemIdByIndex(int idx)
        {
            string itemName = Wait.UntilElementExists(ItemIdByIndex(idx)).Text;
            return itemName;
        }
        public void ClickItemIdByIndex(int idx)
        {
            Wait.UntilElementExists(ItemIdByIndex(idx)).Click();
        }

        // Method to get the text of Quantity by Item Name
        public string GetQuantityByItemName(string item)
        {
            string quantityText = Wait.UntilElementVisible(QuantityByItemName(item)).Text;
            return quantityText;
        }

        // Method to get the text of Quantity by Item Id
        public string GetQuantityByItemId(string id)
        {
            string quantityText = Wait.UntilElementVisible(QuantityByItemId(id)).Text;
            return quantityText;
        }

        // Method to get the text of Quantity by Item Index
        public string GetQuantityByItemIndex(int index)
        {
            string quantityText = Wait.UntilElementVisible(QuantityByItemIndex(index)).Text;
            return quantityText;
        }


        // Method to get the text of List Price by Item Name
        public string GetListPriceByItemName(string item)
        {
            string listPriceText = Wait.UntilElementVisible(ListPriceByItemName(item)).Text;
            return listPriceText;
        }

        // Method to get the text of List Price by Item Id
        public string GetListPriceByItemId(string id)
        {
            string listPriceText = Wait.UntilElementVisible(ListPriceByItemId(id)).Text;
            return listPriceText;
        }

        // Method to get the text of List Price by Item Index
        public string GetListPriceByItemIndex(int index)
        {
            string listPriceText = Wait.UntilElementVisible(ListPriceByItemIndex(index)).Text;
            return listPriceText;
        }

        // Method to get the text of Total Price by Item Index
        public string GetTotalPriceByItemIndex(int index)
        {
            string totalPriceText = Wait.UntilElementVisible(TotalPriceByItemIndex(index)).Text;
            return totalPriceText;
        }

        // Method to get the text of Total Price by Item Id
        public string GetTotalPriceByItemId(string id)
        {
            string totalPriceText = Wait.UntilElementVisible(TotalPriceByItemId(id)).Text;
            return totalPriceText;
        }

        // Method to get the text of Total Price by Item Name
        public string GetTotalPriceByItemName(string item)
        {
            string totalPriceText = Wait.UntilElementVisible(TotalPriceByItemName(item)).Text;
            return totalPriceText;
        }

        public string GetTotalPrice()
        {
            string totPrice = Wait.UntilElementVisible(totalPrice).Text;
            return totPrice;
        }
        public List<string> GetAllItemIds()
        {
            List<string> items = [];
            IReadOnlyCollection<IWebElement> element = Driver.FindElements(itemId);
            foreach (IWebElement elementItem in element)
            {
                items.Add(elementItem.Text);
            }
            return items;
        }

        public List<string> GetAllItemNames()
        {
            List<string> items = [];
            IReadOnlyCollection<IWebElement> element = Driver.FindElements(itemName);
            foreach (IWebElement elementItem in element)
            {
                items.Add(elementItem.Text);
            }
            return items;
        }
        public string GetOrderIdandTime()
        {
            string orderIdAndTym = Wait.UntilElementVisible(orderIdAndTime).Text;
            return orderIdAndTym;
        }

        public string GetCardType()
        {
            string cardTyp = Wait.UntilElementVisible(cardType).Text;
            return cardTyp;
        }

        public string GetCardNumber()
        {
            string cardNum = Wait.UntilElementVisible(cardNumber).Text;
            return cardNum;
        }

        public string GetExpiryDate()
        {
            string expiry = Wait.UntilElementVisible(expiryDate).Text;
            return expiry;
        }

        public void ClickReturntoMainMenuBtn()
        {
            Wait.UntilElementClickable(returnToMainMenu).Click();
        }

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
    }
}
