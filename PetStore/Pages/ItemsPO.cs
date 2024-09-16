using OpenQA.Selenium;
using PetStore.Base;

namespace PetStore.Pages
{
    internal class ItemsPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By returnToCategoryPage = By.PartialLinkText("Return to");
        private readonly By itemID = By.XPath("//td[contains(text(),'$')]/preceding-sibling::td/a");
        public readonly By itemName = By.XPath("//tr/td[3]");
        private readonly By subCategory = By.XPath("//h2");


        private static By ProductIdByItemId(string id) => By.XPath($"//td/a[text() = '{id}']/parent::td/following-sibling::td[1]");
        private static By ProductIdByItemName(string name) => By.XPath($"//td[text()='{name}']/preceding-sibling::td[1]");
        private static By ProductIdByItemIndex(int idx) => By.XPath($"(//td[contains(text(),'$')]/preceding-sibling::td[2])[{idx}]");
        private static By AddToCartButtonByItemName(string itemName) => By.XPath($"//td[contains(text(), '{itemName}')]/following-sibling::td/a");
        private static By AddToCartButtonByItemID(string itemID) => By.XPath($"//td/a[text()='{itemID}']/parent::td/following-sibling::td/a");
        private static By AddToCartButtonByItemIndex(int index) => By.XPath($"(//a[contains(text(), 'Add to Cart')])[{index}]");

        private static By ItemNameByItemID(string itemId) => By.XPath($"//td/a[text()='{itemId}']/parent::td/following-sibling::td[2]");
        private static By ItemNameByIndex(int index) => By.XPath($"(//td/a/parent::td/following-sibling::td[2])[{index}]");
        private static By ItemLinkByItemID(string linkText) => By.XPath($"//td/a[contains(text(), '{linkText}')]");
        private static By ItemLinkByItemName(string catName) => By.XPath($"//td[contains(text(), '{catName}')]/preceding-sibling::td/a");
        private static By ItemLinkByIndex(int index) => By.XPath($"(//td/a)[{index}]");

        private static By ItemPriceByName(string itemName) => By.XPath($"//td[contains(text(),'{itemName}')]/following-sibling::td[1]");
        private static By ItemPriceById(string itemID) => By.XPath($"//td/a[text()='{itemID}']/parent::td/following-sibling::td[3]");
        private static By ItemPriceByIndex(int itemIndex) => By.XPath($"(//tr/td[4])[{itemIndex}]");


        // Get Sub Category
        public string GetSubCategory()
        {
            string subcat = Wait.UntilElementExists(subCategory).Text;
            return subcat;
        }

        // Get Product Id
        public string GetProductIdByItemId(string id)
        {
            string productId = Wait.UntilElementExists(ProductIdByItemId(id)).Text;
            return productId;
        }

        public string GetProductIdByItemName(string name)
        {
            string productId = Wait.UntilElementExists(ProductIdByItemName(name)).Text;
            return productId;
        }

        public string GetProductIdByItemIndex(int idx)
        {
            string productId = Wait.UntilElementExists(ProductIdByItemIndex(idx)).Text;
            return productId;
        }

        public string GetItemNameByItemId(string itemId)
        {
            // Locate the element using the provided itemId and retrieve its text
            string itemName = Wait.UntilElementExists(ItemNameByItemID(itemId)).Text;
            return itemName;
        }

        public string GetItemNameByIndex(int index)
        {
            // Locate the element using the provided index and retrieve its text
            string itemName = Wait.UntilElementExists(ItemNameByIndex(index)).Text;
            return itemName;
        }


        //Click on Add To Cart
        public void ClickOnAddToCartButtonByItemName(string itemName)
        {
            Wait.UntilElementClickable(AddToCartButtonByItemName(itemName)).Click();
        }

        public void ClickOnAddToCartButtonByItemID(string itemID)
        {
            Wait.UntilElementClickable(AddToCartButtonByItemID(itemID)).Click();
        }

        public void ClickOnAddToCartButtonByItemIndex(int index)
        {
            Wait.UntilElementClickable(AddToCartButtonByItemIndex(index)).Click();
        }

        public void ClickOnItemByItemID(string linkText)
        {
            Wait.UntilElementClickable(ItemLinkByItemID(linkText)).Click();
        }

        public void ClickOnItemByItemName(string catName)
        {
            Wait.UntilElementClickable(ItemLinkByItemName(catName)).Click();
        }

        public void ClickOnItemByIndex(int index)
        {
            Wait.UntilElementClickable(ItemLinkByIndex(index)).Click();
        }

        public string GetItemPriceByName(string itemName)
        {
            return Wait.UntilElementVisible(ItemPriceByName(itemName)).Text;
        }

        public string GetItemPriceById(string itemID)
        {
            return Wait.UntilElementVisible(ItemPriceById(itemID)).Text;
        }

        public string GetItemPriceByIndex(int itemIndex)
        {
            return Wait.UntilElementVisible(ItemPriceByIndex(itemIndex)).Text;
        }


        // Return To SubCategory Page
        public void ClickOnReturnToCategoryPage()
        {
            Wait.UntilElementClickable(returnToCategoryPage).Click();
        }

        //Gets All Item IDs
        public List<string> GetAllItemIDs()
        {
            IReadOnlyCollection<IWebElement> itemIDs = Driver.FindElements(itemID);
            List<string> itemIDsList = [];
            foreach (IWebElement id in itemIDs)
            {
                itemIDsList.Add(id.Text);
                Console.WriteLine(id.Text);
            }
            return itemIDsList;
        }

        // Gets All Item Names
        public List<string> GetAllItemNames()
        {
            IReadOnlyCollection<IWebElement> itemNames = Driver.FindElements(itemName);
            List<string> itemNamesList = [];
            foreach (IWebElement name in itemNames)
            {
                itemNamesList.Add(name.Text);
                Console.WriteLine(name.Text);
            }
            return itemNamesList;
        }

    }
}
