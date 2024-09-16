using OpenQA.Selenium;
using PetStore.Base;
using PetStore.utilities;

namespace PetStore.Pages
{
    internal class CartPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By itemId = By.XPath("//td/input/parent::td/preceding-sibling::td[4]");
        private readonly By productId = By.XPath("//td/input/parent::td/preceding-sibling::td[3]");
        private readonly By itemName = By.XPath("//td/input/parent::td/preceding-sibling::td[2]");

        private static By QuantityByName(string itemName) => By.XPath($"//td[contains(text(), '{itemName}')]/following-sibling::td/input");
        private static By QuantityByIndex(int index) => By.XPath($"(//td[contains(text(),'$')]/preceding-sibling::td/input)[{index}]");
        private static By QuantityByItemId(string itemID) => By.XPath($"//a[contains(text(), '{itemID}')]/parent::td/following-sibling::td/input");

        private static By StatusByIndex(int index) => By.XPath($"(//td/input/parent::td/preceding-sibling::td[1])[{index}]");
        private static By StatusByName(string name) => By.XPath($"//td[contains(text(),'{name}')]/following-sibling::td");
        private static By StatusById(string id) => By.XPath($"(//td/a[text() = '{id}']/parent::td/following-sibling::td[3]");

        private static By ListPriceById(string id) => By.XPath($"//td/a[text() = '{id}']/parent::td/following-sibling::td[5]");
        private static By ListPriceByIndex(int index) => By.XPath($"(//td[6])[{index}]");
        private static By ListPriceByName(string name) => By.XPath($"//td[contains(text(),'{name}')]/following-sibling::td[3]");

        private static By TotalPriceById(string id) => By.XPath($"//td/a[text() = '{id}']/parent::td/following-sibling::td[6]");
        private static By TotalPriceByIndex(int index) => By.XPath($"(//td[7])[{index}]");
        private static By TotalPriceByName(string name) => By.XPath($"//td[contains(text(),'{name}')]/following-sibling::td[4]");

        private static By RemoveItemButtonByIndex(int index) => By.LinkText($"//a[contains(text(), 'Remove')][{index}]");
        private static By RemoveItemButtonByItemName(string name) => By.LinkText($"//td[contains(text(), '{name}')]/following-sibling::td/a");
        private static By RemoveItemButtonByItemId(string itemId) => By.LinkText($"//td/a[contains(text(), '{itemId}')]/parent::td/following-sibling::td/a");

        private readonly By updateCartButton = By.Name("updateCartQuantities");
        private readonly By proceedToCheckoutButton = By.LinkText("Proceed to Checkout");
        private readonly By subTotal = By.XPath("//tr/td[@colspan='7']");
        private readonly By returnToMainMenuButton = By.LinkText("Return to Main Menu");

        private static By ItemIdByLinkText(string text) => By.LinkText(text);
        private static By ItemIdByItemName(string name) => By.XPath($"//td[text()='{name}']/preceding-sibling::td/a");
        private static By ItemIdByIndex(int index) => By.XPath($"(//td/a[text()='Remove']/parent::td/preceding-sibling::td/a)[{index}]");

        //Get All Item Ids
        public List<string> GetAllItemId()
        {
            IReadOnlyCollection<IWebElement> elements = Driver.FindElements(itemId);
            List<string> items = [];
            foreach (IWebElement element in elements)
            {
                items.Add(element.Text);
            }
            return items;
        }

        // Click on Item Ids
        public void ClickItemIdByLinkText(string text)
        {
            Wait.UntilElementClickable(ItemIdByLinkText(text)).Click();
        }

        public void ClickItemIdByItemName(string name)
        {
            Wait.UntilElementClickable(ItemIdByItemName(name)).Click();
        }

        public void ClickItemIdByIndex(int index)
        {
            Wait.UntilElementClickable(ItemIdByIndex(index)).Click();
        }


        // Get All Product IDs
        public List<string> GetAllProductIds()
        {
            IReadOnlyCollection<IWebElement> elements = Driver.FindElements(productId);
            List<string> items = [];
            foreach (IWebElement element in elements)
            {
                items.Add(element.Text);
            }
            return items;
        }

        // Get All Item Names
        public List<string> GetAllItemNames()
        {
            IReadOnlyCollection<IWebElement> elements = Driver.FindElements(itemName);
            List<string> items = [];
            foreach (IWebElement element in elements)
            {
                items.Add(element.Text);
            }
            return items;
        }

        // Get SubTotal
        public string GetSubTotal()
        {
            string subTot = Wait.UntilElementVisible(subTotal).Text;
            return subTot;
        }


        // Get Status

        public string GetStatusOfItemByIndex(int index)
        {
            string status = Wait.UntilElementVisible(StatusByIndex(index)).Text;
            return status;
        }

        public string GetStatusOfItemByName(string name)
        {
            string status = Wait.UntilElementVisible(StatusByName(name)).Text;
            return status;
        }

        public string GetStatusOfItemById(string id)
        {
            string status = Wait.UntilElementVisible(StatusById(id)).Text;
            return status;
        }


        // Get Quantity

        public string GetQuantityByItemName(string itemName)
        {
            string quant = Wait.UntilElementExists(QuantityByName(itemName)).GetAttribute("value");
            return quant;
        }

        public string GetQuantityByItemIndex(int index)
        {
            string quant = Wait.UntilElementExists(QuantityByIndex(index)).GetAttribute("value");
            return quant;
        }

        public string GetQuantityByItemID(string itemID)
        {
            string quant = Wait.UntilElementExists(QuantityByItemId(itemID)).GetAttribute("value");
            return quant;
        }

        // Set Quantity
        public void SetQuantityByItemName(string itemName,string quantity) {
            Wait.UntilElementExists(QuantityByName(itemName)).EnterText(quantity);
        }

        public void SetQuantityByItemIndex(int index,string quantity)
        {
            Wait.UntilElementExists(QuantityByIndex(index)).EnterText(quantity);
        }

        public void SetQuantityByItemID(string itemID, string quantity)
        {
            Wait.UntilElementExists(QuantityByItemId(itemID)).EnterText(quantity);
        }

        // Get ListPrice
        public string GetListPriceById(string id)
        {
            string listPrice = Wait.UntilElementVisible(ListPriceById(id)).Text;
            return listPrice;
        }

        public string GetListPriceByIndex(int index)
        {
            string listPrice = Wait.UntilElementVisible(ListPriceByIndex(index)).Text;
            return listPrice;
        }

        public string GetListPriceByName(string name)
        {
            string listPrice = Wait.UntilElementVisible(ListPriceByName(name)).Text;
            return listPrice;
        }

        // Get TotalPrice
        public string GetTotalPriceById(string id)
        {
            string totalPrice = Wait.UntilElementVisible(TotalPriceById(id)).Text;
            return totalPrice;
        }

        public string GetTotalPriceByIndex(int index)
        {
            string totalPrice = Wait.UntilElementVisible(TotalPriceByIndex(index)).Text;
            return totalPrice;
        }

        public string GetTotalPriceByName(string name)
        {
            string totalPrice = Wait.UntilElementVisible(TotalPriceByName(name)).Text;
            return totalPrice;
        }

        // Remove Button

        public void ClickOnRemoveButtonByItemId(string itemId) { 
            Wait.UntilElementClickable(RemoveItemButtonByItemId(itemId)).Click();
        }

        public void ClickOnRemoveButtonByIndex(int index)
        {
            Wait.UntilElementClickable(RemoveItemButtonByIndex(index)).Click();
        }

        public void ClickOnRemoveButtonByItemName(string itemName)
        {
            Wait.UntilElementClickable(RemoveItemButtonByItemName(itemName)).Click();
        }

        // Update Cart Button
        public void ClickOnUpdateCartButton()
        {
            Wait.UntilElementClickable(updateCartButton).Click();
        }

        // Proceed to Checkout Button
        public void ClickProceedToCheckoutButton()
        {
            Wait.UntilElementClickable(proceedToCheckoutButton).Click();
        }

        // Return to Main Menu Button
        public void ClickReturnToMainMenuButton()
        {
            Wait.UntilElementClickable(returnToMainMenuButton).Click();
        }
    }
}
