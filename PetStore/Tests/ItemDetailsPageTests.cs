using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;
using PetStore.utilities;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("ItemDetailsPageTests")]
    public class ItemDetailsPageTests : BaseTests
    {
        HomePO homePage;
        CommonPO commonPage;
        ProductCategoryPO productCategoryPage;
        ItemsPO itemsPage;
        ItemDetailsPO itemDetailsPage;
        CartPO cartPage;

        [TestInitialize]
        public void ItemDetailsPage()
        {
            homePage = new HomePO(Driver);
            productCategoryPage = new ProductCategoryPO(Driver);
            itemsPage = new ItemsPO(Driver);
            itemDetailsPage = new ItemDetailsPO(Driver);
            commonPage = new CommonPO(Driver);
            cartPage = new CartPO(Driver);

            Driver.NavigateTo(PetStoreUrl);
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatAddToCartFunctionalityIsWorking(string category)
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
                    Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));
                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    List<string> items = cartPage.GetAllItemId();
                    Assert.IsTrue(items.Contains(itemId));
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatReturnToItemsPageButtonIsWorking(string category)
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
                    Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));
                    itemDetailsPage.ClickOnReturnToItemsPage();
                    Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatItemIdIsNotNull(string category)
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
                    Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatItemNameIsNotNull(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemNames = itemsPage.GetAllItemNames();
                foreach (var itemName in itemNames)
                {
                    commonPage.RemoveTabsAndNewLineCharacters(itemsPage.itemName);
                    itemsPage.ClickOnItemByItemName(itemName);
                    Assert.IsTrue(itemDetailsPage.GetItemName().Equals(itemName));
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatItemSubCategoryIsNotNull(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemNames = itemsPage.GetAllItemNames();
                foreach (var itemName in itemNames)
                {
                    commonPage.RemoveTabsAndNewLineCharacters(itemsPage.itemName);
                    itemsPage.ClickOnItemByItemName(itemName);
                    Assert.IsTrue(itemDetailsPage.GetItemSubCategory().Equals(item));
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatItemDescriptionIsNotNull(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemNames = itemsPage.GetAllItemNames();
                foreach (var itemName in itemNames)
                {
                    commonPage.RemoveTabsAndNewLineCharacters(itemsPage.itemName);
                    itemsPage.ClickOnItemByItemName(itemName);
                    Assert.IsNotNull(itemDetailsPage.GetItemDescription());
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatItemPriceIsNotNull(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemNames = itemsPage.GetAllItemNames();
                foreach (var itemName in itemNames)
                {
                    commonPage.RemoveTabsAndNewLineCharacters(itemsPage.itemName);
                    itemsPage.ClickOnItemByItemName(itemName);
                    Assert.IsNotNull(itemDetailsPage.GetItemPrice());
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatItemStatusIsNotNull(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemNames = itemsPage.GetAllItemNames();
                foreach (var itemName in itemNames)
                {
                    commonPage.RemoveTabsAndNewLineCharacters(itemsPage.itemName);
                    itemsPage.ClickOnItemByItemName(itemName);
                    Assert.IsNotNull(itemDetailsPage.GetItemStatus());
                    Driver.Back();
                }
                Driver.Back();
            }
        }
    }
}