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
        HomePO homePage;
        ProductCategoryPO productCategoryPage;
        ItemsPO itemsPage;
        [TestInitialize]
        public void ProductCategoryTest() {
            homePage = new HomePO(Driver);
            productCategoryPage = new ProductCategoryPO(Driver);
            itemsPage = new ItemsPO(Driver);
            Driver.NavigateTo(PetStoreUrl);
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatReturnToMainMenuLinkIsWorkingForEachCategory(string page)
        {
            homePage.ClickCategoryImage(page);
            Assert.IsTrue(Driver.Url.Contains(page));
            productCategoryPage.ClickReturnToMainMenuButton();
            Assert.IsTrue(Driver.Url.Equals("https://petstore.octoperf.com/actions/Catalog.action"));
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatAllSubCategoryLinksAreClickableForEachCategory(string page)
        {
            homePage.ClickCategoryImage(page);
            Assert.IsTrue(Driver.Url.Contains(page));
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            List<string> subCatIds = productCategoryPage.GetAllProductIDs();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                Driver.Back();
            }
            foreach (var item in subCatIds)
            {
                productCategoryPage.ClickOnSubCategoryByProductID(item);
                Assert.IsTrue(Driver.Url.EndsWith(item));
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
            homePage.ClickCategoryImage(page);
            Assert.IsTrue(Driver.Url.Contains(page));
            string category = productCategoryPage.GetCategory();
            Assert.IsNotNull(category);
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyAtLeastOneProductSubCategoryExists(string page)
        {
            homePage.ClickCategoryImage(page);
            Assert.IsTrue(Driver.Url.Contains(page));
            List<string> subCategory = productCategoryPage.GetAllSubCategories();
            Assert.IsTrue(subCategory.Count > 0);
            List<string> subCategory1 = productCategoryPage.GetAllProductIDs();
            Assert.IsTrue(subCategory1.Count > 0);
        }

    }
}
