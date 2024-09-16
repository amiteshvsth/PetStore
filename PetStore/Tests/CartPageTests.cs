using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;
using PetStore.utilities;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("CartPageTests")]
    public class CartPageTests : BaseTests
    {
        HomePO homePage;
        CommonPO commonPage;
        ProductCategoryPO productCategoryPage;
        ItemsPO itemsPage;
        ItemDetailsPO itemDetailsPage;
        CartPO cartPage;

        [TestInitialize]
        public void CartPage()
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
        public void VerifyThatItemAddedToCartIsDisplayed(string category)
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
        public void VerifyThatQuantityIsIncreasedAfterAddingTheSameItemToCart(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                int itemQuantity, newItemQuantity;
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemIds = itemsPage.GetAllItemIDs();
                foreach (var itemId in itemIds)
                {
                    itemsPage.ClickOnItemByItemID(itemId);
                    Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));
                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    List<string> items = cartPage.GetAllItemId();
                    itemQuantity = int.Parse(cartPage.GetQuantityByItemID(itemId));
                    Assert.IsTrue(items.Contains(itemId));
                    cartPage.ClickItemIdByLinkText(itemId);
                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    newItemQuantity = int.Parse(cartPage.GetQuantityByItemID(itemId));
                    Assert.AreEqual(newItemQuantity, itemQuantity + 1);
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
        public void VerifyThat()
        {

        }
    }
}
