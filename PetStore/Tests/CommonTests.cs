using OpenQA.Selenium;
using PetStore.Base;
using PetStore.Pages.Common;
using PetStore.utilities;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("CommonTest")]
    public class CommonTests : BaseTests
    {
        CommonPO commonPage;
        [TestInitialize]
        public void CommonTestSetup()
        {
            commonPage = new CommonPO(Driver);
            Driver.NavigateTo(PetStoreUrl);
        }

        [TestMethod]
        public void VerifyThatCartIconLinkIsWorking()
        {
            commonPage.ClickCartIcon();
            Assert.IsTrue(Driver.Url.Contains("viewCart"));
        }

        [TestMethod]
        public void VerifyThatSignInLinkIsWorking()
        {
            commonPage.ClickSignInLink();
            Assert.IsTrue(Driver.Url.Contains("signonForm"));
        }

        [TestMethod]
        public void VerifyThatHelpIconLinkIsWorking()
        {
            commonPage.Clickhelp();
            Assert.IsTrue(Driver.Url.Contains("help"));
        }

        [TestMethod]
        public void VerifyThatFishNavbarLinkIsWorking()
        {
            commonPage.ClickFishNavbar();
            Assert.IsTrue(Driver.Url.Contains("FISH"));
        }

        [TestMethod]
        public void VerifyThatDogNavbarLinkIsWorking()
        {
            commonPage.ClickDogNavbar();
            Assert.IsTrue(Driver.Url.Contains("DOG"));
        }

        [TestMethod]
        public void VerifyThatReptilesNavbarLinkIsWorking()
        {
            commonPage.ClickReptilesNavbar();
            Assert.IsTrue(Driver.Url.Contains("REPTILES"));
        }

        [TestMethod]
        public void VerifyThatCatsNavbarLinkIsWorking()
        {
            commonPage.ClickCatNavbar();
            Assert.IsTrue(Driver.Url.Contains("CAT"));
        }

        [TestMethod]
        public void VerifyThatBirdsNavbarLinkIsWorking()
        {
            commonPage.ClickBirdsNavbar();
            Assert.IsTrue(Driver.Url.Contains("BIRDS"));
        }

        [TestMethod]
        public void VerifyThatOctoPerfLinkIsWorking()
        {
            commonPage.ClickOctoPerfLink();
            Assert.IsTrue(Driver.Url.Contains("octoperf"));
        }

        [TestMethod]
        public void VerifyThatOctoPerf2LinkIsWorking()
        {
            commonPage.ClickOctoPerf2Link();
            Assert.IsTrue(Driver.Url.Contains("octoperf"));
        }

        [TestMethod]
        public void VerifyThatMyBatIsLinkIsWorking()
        {
            commonPage.ClickMybatisLink();
            Assert.IsTrue(Driver.Url.Contains("mybatis"));
        }

        [TestMethod]
        public void VerifyThatLogoIsClickable()
        {
            commonPage.ClickLogo();
            Assert.IsTrue(Driver.Url.Contains("https://petstore.octoperf.com/actions/Catalog.action"));
        }

        [TestMethod]
        [DynamicData(nameof(CommonPO.GetAllPets), typeof(CommonPO), DynamicDataSourceType.Method)]
        public void VerifyThatSearchIsWorking(string search)
        {
            IWebElement elem = commonPage.Search(search);
            Assert.IsTrue(elem.Displayed);
        }
    }
}
