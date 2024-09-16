using OpenQA.Selenium;
using PetStore.Base;

namespace PetStore.Pages
{
    internal class ItemDetailsPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By addToCartButton = By.LinkText("Add to Cart");
        private readonly By returnToItemsPage = By.PartialLinkText("Return to");
        private readonly By itemId = By.XPath("(//td/b)[1]");
        private readonly By itemName = By.XPath("//td/b/font");
        private readonly By itemSubCategory = By.XPath("(//tr/td)[4]");
        private readonly By itemPrice = By.XPath("(//tr/td)[6]");
        private readonly By itemDescription = By.XPath("//td/img/parent::td");
        private readonly By itemStatus = By.XPath("//tbody/tr[5]/td");

        public void ClickOnAddToCartDetailsPage()
        {
            Wait.UntilElementClickable(addToCartButton).Click();
        }

        public void ClickOnReturnToItemsPage()
        {
            Wait.UntilElementClickable(returnToItemsPage).Click();
        }

        public string GetItemId()
        {
            string itemid = Wait.UntilElementClickable(itemId).Text;
            return itemid;
        }

        public string GetItemName()
        {
            string itemname = Wait.UntilElementClickable(itemName).Text;
            return itemname;
        }

        public string GetItemSubCategory()
        {
            string itemSubcat = Wait.UntilElementClickable(itemSubCategory).Text;
            return itemSubcat;
        }

        public string GetItemPrice()
        {
            string itemprice = Wait.UntilElementClickable(itemPrice).Text;
            return itemprice;
        }

        public string GetItemDescription()
        {
            string itemDesc = Wait.UntilElementClickable(itemDescription).Text;
            return itemDesc;
        }

        public string GetItemStatus()
        {
            string itemStat = Wait.UntilElementClickable(itemStatus).Text;
            return itemStat;
        }

        public void GetItemDetails()
        {
            Console.WriteLine(GetItemId());
            Console.WriteLine(GetItemName());
            Console.WriteLine(GetItemDescription());
            Console.WriteLine(GetItemPrice());
            Console.WriteLine(GetItemSubCategory());
            Console.WriteLine(GetItemStatus());
        }
    }
}
