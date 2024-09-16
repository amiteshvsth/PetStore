using OpenQA.Selenium;
using PetStore.Base;

namespace PetStore.Pages
{

    internal class ProductCategoryPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By returnToMainMenuButton = By.XPath("//a[contains(text(),'Return to Main Menu')]");
        private readonly By subCategory = By.XPath("//td/following-sibling::td");
        private readonly By productID = By.XPath("//td/a");
        private readonly By category = By.XPath("//h2");
        private static By ProductNameById(string id) => By.XPath($"//td/a[text()='{id}']/parent::td/following-sibling::td[1]");
        private static By ProductNameByIndex(int index) => By.XPath($"(//td/a/parent::td/following-sibling::td[1])[{index}]");
        private static By SubCategoryLinkByProductID(string linkText) => By.XPath($"//td/a[contains(text(), '{linkText}')]");
        private static By SubCategoryLinkByProductName(string catName) => By.XPath($"//td[contains(text(), '{catName}')]/preceding-sibling::td/a");
        private static By SubCategoryLinkByIndex(int index) => By.XPath($"(//td/a)[{index}]");


        public string GetCategory()
        {
            string catgry = Wait.UntilElementExists(category).Text;
            return catgry;
        }

        public void ClickReturnToMainMenuButton()
        {
            Wait.UntilElementClickable(returnToMainMenuButton).Click();
        }

        public List<string> GetAllSubCategories()
        {
            IReadOnlyCollection<IWebElement> categories = Driver.FindElements(subCategory);
            List<string> categoriesList = [];
            foreach (IWebElement catgry in categories)
            {
                categoriesList.Add(catgry.Text);
                Console.WriteLine(catgry.Text);
            }
            return categoriesList;
        }

        public List<string> GetAllProductIDs()
        {
            IReadOnlyCollection<IWebElement> productIDs = Driver.FindElements(productID);
            List<string> productIDsList = [];
            foreach (IWebElement id in productIDs)
            {
                productIDsList.Add(id.Text);
                Console.WriteLine(id.Text);
            }
            return productIDsList;
        }

        public string GetProductIdByName(string name)
        {
            var subCategoryLinkElement = Wait.UntilElementClickable(SubCategoryLinkByProductName(name));
            return subCategoryLinkElement.Text;
        }

        public string GetProductIdByIndex(int idx)
        {
            var subCategoryLinkElement = Wait.UntilElementClickable(SubCategoryLinkByIndex(idx));
            return subCategoryLinkElement.Text;
        }

        public string GetProductNameById(string id)
        {
            var prodName = Wait.UntilElementVisible(ProductNameById(id));
            return prodName.Text;
        }

        public string GetProductNameByIndex(int idx)
        {
            var prodName = Wait.UntilElementVisible(ProductNameByIndex(idx));
            return prodName.Text;
        }
        public void ClickOnSubCategoryByProductID(string linkText)
        {
            var subCategoryLinkElement = Wait.UntilElementClickable(SubCategoryLinkByProductID(linkText));
            subCategoryLinkElement.Click();
        }

        public void ClickOnSubCategoryByProductName(string catName)
        {
            var subCategoryLinkElement = Wait.UntilElementClickable(SubCategoryLinkByProductName(catName));
            subCategoryLinkElement.Click();
        }

        public void ClickOnSubCategoryByIndex(int index)
        {
            var subCategoryLinkElement = Wait.UntilElementClickable(SubCategoryLinkByIndex(index));
            subCategoryLinkElement.Click();
        }
    }
}
