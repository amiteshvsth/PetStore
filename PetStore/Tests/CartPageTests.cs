using OpenQA.Selenium;
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
        SignInPO signInPage;
        CommonPO commonPage;
        ProductCategoryPO productCategoryPage;
        ItemsPO itemsPage;
        ItemDetailsPO itemDetailsPage;
        CartPO cartPage;

        private static string RemoveDollarSign(string input) => input?.Replace("$", string.Empty) ?? string.Empty;

        [TestInitialize]
        public void CartPage()
        {
            signInPage = new SignInPO(Driver);
            productCategoryPage = new ProductCategoryPO(Driver);
            itemsPage = new ItemsPO(Driver);
            itemDetailsPage = new ItemDetailsPO(Driver);
            commonPage = new CommonPO(Driver);
            cartPage = new CartPO(Driver);

            Driver.NavigateTo(PetStoreUrl);
            commonPage.ClickSignInLink();
            signInPage.SignInUser("mAQyo6677", "lqYFkDr0WD");
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
        public void VerifyThatProductIdRemainsSameAtTheCartPage(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemIds = itemsPage.GetAllItemIDs();
                string prodIdAtItemsPage, prodIdAtCartPage;
                foreach (var itemId in itemIds)
                {
                    prodIdAtItemsPage = itemsPage.GetProductIdByItemId(itemId);
                    itemsPage.ClickOnItemByItemID(itemId);
                    Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));
                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    List<string> items = cartPage.GetAllItemId();

                    prodIdAtCartPage = cartPage.GetProductIdByItemId(itemId);
                    //List<string> prodIds = cartPage.GetAllProductIds();
                    Assert.IsTrue(items.Contains(itemId));
                    Assert.IsTrue(prodIdAtCartPage.Equals(prodIdAtItemsPage));
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatListPriceRemainsSameAtTheCartPage(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemIds = itemsPage.GetAllItemIDs();
                string itemPriceAtItemsPage, itemPriceAtCartPage;
                foreach (var itemId in itemIds)
                {
                    itemsPage.ClickOnItemByItemID(itemId);
                    Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));
                    itemPriceAtItemsPage = itemDetailsPage.GetItemPrice();

                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    List<string> items = cartPage.GetAllItemId();

                    itemPriceAtCartPage = cartPage.GetListPriceById(itemId);
                    //List<string> prodIds = cartPage.GetAllProductIds();
                    Assert.IsTrue(items.Contains(itemId));
                    Assert.IsTrue(itemPriceAtCartPage.Equals(itemPriceAtItemsPage));
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatTotalPriceIsCorrect(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemIds = itemsPage.GetAllItemIDs();
                double itemPriceAtCartPage;
                double itemQuantityAtCartPage;
                double totalPriceAtCartPage;
                foreach (var itemId in itemIds)
                {
                    itemsPage.ClickOnItemByItemID(itemId);
                    Assert.IsTrue(itemDetailsPage.GetItemId().Equals(itemId));

                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    List<string> items = cartPage.GetAllItemId();
                    Assert.IsTrue(items.Contains(itemId));
                    string itemPrice = RemoveDollarSign(cartPage.GetTotalPriceById(itemId));
                    itemPriceAtCartPage = double.Parse(itemPrice);
                    itemQuantityAtCartPage = double.Parse(cartPage.GetQuantityByItemID(itemId));
                    string totalPrice = RemoveDollarSign(cartPage.GetTotalPriceById(itemId));
                    totalPriceAtCartPage = double.Parse(totalPrice);
                    Assert.AreEqual(itemPriceAtCartPage * itemQuantityAtCartPage, totalPriceAtCartPage);
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatUpdateCartIsUpdatingThePriceAccordingly(string category)
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
                    cartPage.SetQuantityByItemID(itemId,"23");
                    cartPage.ClickOnUpdateCartButton();
                    double priceAtCartPage = double.Parse(RemoveDollarSign(cartPage.GetListPriceById(itemId)));
                    double quantityAtCartPage = double.Parse(cartPage.GetQuantityByItemID(itemId));
                    double total = Math.Round(priceAtCartPage*quantityAtCartPage,2);
                    double totalAtCartPage = double.Parse(RemoveDollarSign(cartPage.GetTotalPriceById(itemId)));
                    Assert.AreEqual(total, totalAtCartPage);
                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatItemNameRemainsSameAtTheCartPage(string category)
        {
            commonPage.NavigateToCategory(category);
            List<string> subCatNames = productCategoryPage.GetAllSubCategories();
            foreach (var item in subCatNames)
            {
                productCategoryPage.ClickOnSubCategoryByProductName(item);
                Assert.IsTrue(itemsPage.GetSubCategory().Equals(item));
                List<string> itemIds = itemsPage.GetAllItemIDs();
                string itemNameAtItemsPage, itemNameAtCartPage;
                foreach (var itemId in itemIds)
                {
                    itemsPage.ClickOnItemByItemID(itemId);
                    itemNameAtItemsPage = itemDetailsPage.GetItemName();
                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    List<string> items = cartPage.GetAllItemNames();

                    itemNameAtCartPage = cartPage.GetItemNameByID(itemId);
                    Assert.IsTrue(itemNameAtItemsPage.Equals(itemNameAtCartPage));
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatTheSubTotalIsCorrect(string category)
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
                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    double subtotal = cartPage.GetSubTotal();
                    Assert.AreEqual(subtotal, cartPage.GetSubTotalByCalculation());
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatTheItemIsRemovedAfterClickingRemoveButton(string category)
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
                    itemDetailsPage.ClickOnAddToCartDetailsPage();
                    cartPage.ClickOnRemoveButtonByItemId(itemId);
                    try
                    {
                        // Attempt to find the element. If found, fail the test.
                        var element = Driver.FindElement(cartPage.ItemIdByLinkText(itemId));
                        Assert.Fail($"Element with itemId {itemId} exists on the page, but it should not.");
                    }
                    catch (NoSuchElementException)
                    {
                        // No elements found, which is the expected outcome
                        Assert.IsTrue(true);
                    }
                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllCategories), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatProceedToCheckoutButtonIsWorkingFine(string category)
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
                    cartPage.SetQuantityByItemID(itemId, "23");
                    cartPage.ClickOnUpdateCartButton();
                    cartPage.ClickProceedToCheckoutButton();
                    Assert.IsTrue(Driver.Url.Contains("newOrderForm="));
                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                    Driver.Back();
                }
                Driver.Back();
            }
        }
    }
}
