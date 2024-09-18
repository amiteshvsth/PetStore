using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;
using PetStore.utilities;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("ItemsPageTests")]
    public class ItemsPageTests : BaseTests
    {
        HomePO homePage;
        CommonPO commonPage;
        ProductCategoryPO productCategoryPage;
        ItemsPO itemsPage;
        ItemDetailsPO itemDetailsPage;
        CartPO cartPage;

        [TestInitialize]
        public void ItemsTest()
        {
            homePage = new HomePO(Driver);
            productCategoryPage = new ProductCategoryPO(Driver);
            itemsPage = new ItemsPO(Driver);
            itemDetailsPage = new ItemDetailsPO(Driver);
            commonPage = new CommonPO(Driver);
            cartPage = new CartPO(Driver);

            Driver.NavigateTo(PetStoreUrl);
        }

        

        private void VerifyReturnToCategory(string category)
        {
            Assert.IsTrue(productCategoryPage.GetCategory().ToUpper().Equals(category));
        }

        private void ClickSubCategoryAndVerify(string subCategoryName)
        {
            productCategoryPage.ClickOnSubCategoryByProductName(subCategoryName);
            Assert.IsTrue(itemsPage.GetSubCategory().Equals(subCategoryName));
        }

        private void VerifyItemActions(List<string> itemIds, List<string> itemNames, string subCategory)
        {
            foreach (var itemId in itemIds)
            {
                itemsPage.ClickOnItemByItemID(itemId);
                Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));
                Driver.Back();
                Assert.IsTrue(subCategory.Equals(itemsPage.GetSubCategory()));
            }

            foreach (var itemName in itemNames)
            {
                commonPage.RemoveTabsAndNewLineCharacters(itemsPage.itemName);
                itemsPage.ClickOnItemByItemName(itemName);
                Assert.IsTrue(itemDetailsPage.GetItemName().Equals(itemName));
                Driver.Back();
                Assert.IsTrue(subCategory.Equals(itemsPage.GetSubCategory()));
            }

            for (int i = 1; i <= itemIds.Count; i++)
            {
                itemsPage.ClickOnItemByIndex(i);
                Driver.Back();
                Assert.IsTrue(subCategory.Equals(itemsPage.GetSubCategory()));
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatReturnToCategoryPageLinkIsWorkingForEachSubCategory(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            List<string> subCatIds = productCategoryPage.GetAllProductIDs();

            foreach (var subCat in subCatNames)
            {
                ClickSubCategoryAndVerify(subCat);
                itemsPage.ClickOnReturnToCategoryPage();
                VerifyReturnToCategory(category);
            }

            foreach (var subCatId in subCatIds)
            {
                productCategoryPage.ClickOnSubCategoryByProductID(subCatId);
                itemsPage.ClickOnReturnToCategoryPage();
                VerifyReturnToCategory(category);
            }

            for (int i = 1; i < subCatNames.Count; i++)
            {
                productCategoryPage.ClickOnSubCategoryByIndex(i);
                itemsPage.ClickOnReturnToCategoryPage();
                VerifyReturnToCategory(category);
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatAllItemsLinksAreClickable(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();

            foreach (var subCat in subCatNames)
            {
                ClickSubCategoryAndVerify(subCat);
                List<string> itemIds = itemsPage.GetAllItemIDs();
                List<string> itemNames = itemsPage.GetAllItemNames();
                VerifyItemActions(itemIds, itemNames, subCat);
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatAtLeastOneItemIsAvailablePerCategory(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();

            foreach (var subCat in subCatNames)
            {
                ClickSubCategoryAndVerify(subCat);
                Assert.IsTrue(itemsPage.GetAllItemIDs().Count > 0);
                Assert.IsTrue(itemsPage.GetAllItemNames().Count > 0);
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatListItemsPriceIsNotZero(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();

            foreach (var subCat in subCatNames)
            {
                ClickSubCategoryAndVerify(subCat);
                VerifyItemPrices(itemsPage.GetAllItemIDs(), itemsPage.GetAllItemNames());
                Driver.Back();
            }
        }

        private void VerifyItemPrices(List<string> itemIds, List<string> itemNames)
        {
            foreach (var itemId in itemIds)
            {
                string itemPrice = itemsPage.GetItemPriceById(itemId);
                Assert.IsFalse(itemPrice.Equals("$0.00"));
            }

            foreach (var itemName in itemNames)
            {
                commonPage.RemoveTabsAndNewLineCharacters(itemsPage.itemName);
                string itemPrice = itemsPage.GetItemPriceByName(itemName);
                Assert.IsFalse(itemPrice.Equals("$0.00"));
            }

            for (int i = 1; i <= itemNames.Count; i++)
            {
                string itemPrice = itemsPage.GetItemPriceByIndex(i);
                Assert.IsFalse(itemPrice.Equals("$0.00"));
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatTheProductIdIsSameForAllItemsOfThatSubCategory(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatIds = productCategoryPage.GetAllProductIDs();

            foreach (var subCatId in subCatIds)
            {
                productCategoryPage.ClickOnSubCategoryByProductID(subCatId);
                VerifyProductIds(itemsPage.GetAllItemIDs(), itemsPage.GetAllItemNames(), subCatId);
                Driver.Back();
            }
        }

        private void VerifyProductIds(List<string> itemIds, List<string> itemNames, string expectedProductId)
        {
            foreach (var itemId in itemIds)
            {
                string prodID = itemsPage.GetProductIdByItemId(itemId);
                Assert.IsTrue(prodID.Equals(expectedProductId));
            }

            foreach (var itemName in itemNames)
            {
                commonPage.RemoveTabsAndNewLineCharacters(itemsPage.itemName);
                string prodID = itemsPage.GetProductIdByItemName(itemName);
                Assert.IsTrue(prodID.Equals(expectedProductId));
            }

            for (int i = 1; i <= itemNames.Count; i++)
            {
                string prodID = itemsPage.GetProductIdByItemIndex(i);
                Assert.IsTrue(prodID.Equals(expectedProductId));
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatAddToCartButtonIsWorkingForAllItems(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatIds = productCategoryPage.GetAllProductIDs();

            foreach (var subCatId in subCatIds)
            {
                productCategoryPage.ClickOnSubCategoryByProductID(subCatId);
                VerifyAddToCartActions(itemsPage.GetAllItemIDs(), itemsPage.GetAllItemNames());
                Driver.Back();
            }
        }

        private void VerifyAddToCartActions(List<string> itemIds, List<string> itemNames)
        {
            foreach (var itemId in itemIds)
            {
                itemsPage.ClickOnAddToCartButtonByItemID(itemId);
                Assert.IsTrue(Driver.Url.EndsWith(itemId));
                Driver.Back();
            }

            foreach (var itemName in itemNames)
            {
                commonPage.RemoveTabsAndNewLineCharacters(itemsPage.itemName);
                itemsPage.ClickOnAddToCartButtonByItemName(itemName);
                List<string> itemsInCart = cartPage.GetAllItemNames();
                Assert.IsTrue(itemsInCart.Contains(itemName));
                Driver.Back();
            }

            for (int i = 1; i <= itemNames.Count; i++)
            {
                itemsPage.ClickOnAddToCartButtonByItemIndex(i);
                List<string> itemsInCart = cartPage.GetAllItemNames();
                Assert.IsTrue(itemsInCart.Contains(itemNames[i - 1]));
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatSubCategoryNameIsNotNull(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatIds = productCategoryPage.GetAllProductIDs();
            string categoryName;
            foreach (var item in subCatIds)
            {
                categoryName = productCategoryPage.GetProductNameById(item);
                productCategoryPage.ClickOnSubCategoryByProductID(item);
                Assert.IsTrue(Driver.Url.EndsWith(item));
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(categoryName));
                Driver.Back();
            }
        }
    }
}
