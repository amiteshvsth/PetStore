using PetStore.Base;
using PetStore.Pages;
using PetStore.Pages.Common;
using PetStore.utilities;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("productCategoryPageTests")]
    public class ProductCategoryPageTests : BaseTests
    {
        private HomePO homePage;
        private ProductCategoryPO productCategoryPage;
        private ItemsPO itemsPage;

        [TestInitialize]
        public void ProductCategoryPageTest()
        {
            homePage = new HomePO(Driver);
            productCategoryPage = new ProductCategoryPO(Driver);
            itemsPage = new ItemsPO(Driver);
            Driver.NavigateTo(PetStoreUrl);
        }

        private void NavigateToCategory(string page)
        {
            homePage.ClickCategoryImage(page);
            Assert.IsTrue(Driver.Url.Contains(page), $"URL does not contain {page}");
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatReturnToMainMenuLinkIsWorkingForEachCategory(string page)
        {
            NavigateToCategory(page);
            productCategoryPage.ClickReturnToMainMenuButton();
            Assert.AreEqual("https://petstore.octoperf.com/actions/Catalog.action", Driver.Url);
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatAllSubCategoryLinksAreClickableForEachCategory(string page)
        {
            NavigateToCategory(page);

            var subCatNames = productCategoryPage.GetAllSubCategories();
            var subCatIds = productCategoryPage.GetAllProductIDs();

            foreach (var name in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(name);
                Assert.AreEqual(name, itemsPage.GetSubCategory());
                Driver.Back();
            }

            foreach (var id in subCatIds)
            {
                productCategoryPage.ClickOnSubCategoryByProductID(id);
                Assert.IsTrue(Driver.Url.EndsWith(id));
                Driver.Back();
            }

            for (int i = 1; i < subCatNames.Count; i++)
            {
                productCategoryPage.ClickOnSubCategoryByIndex(i);
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyProductCategoryNameIsNotNull(string page)
        {
            NavigateToCategory(page);
            var category = productCategoryPage.GetCategory();
            Assert.IsNotNull(category, "Category name should not be null");
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyAtLeastOneProductSubCategoryExists(string page)
        {
            NavigateToCategory(page);

            var subCategories = productCategoryPage.GetAllSubCategories();
            Assert.IsTrue(subCategories.Count > 0, "No product subcategories found");

            var subCategoryIds = productCategoryPage.GetAllProductIDs();
            Assert.IsTrue(subCategoryIds.Count > 0, "No product IDs found");
        }
    }
}
