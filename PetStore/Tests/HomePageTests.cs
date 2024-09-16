using PetStore.Base;
using PetStore.Pages;
using PetStore.utilities;

namespace PetStore.Tests
{
    [TestClass]
    [TestCategory("HomePageTest")]
    public class HomePageTests : BaseTests
    {
        HomePO homepage;
        [TestInitialize]
        public void TestSetup()
        {
            homepage = new HomePO(Driver);
            Driver.NavigateTo(PetStoreUrl);
        }

        [TestMethod]
        public void VerifyThatFishSidebarLinkIsWorking()
        {
            homepage.ClickFishSidebar();
            Assert.IsTrue(Driver.Url.Contains("FISH"));
        }

        [TestMethod]
        public void VerifyThatFishImageLinkIsWorking()
        {
            homepage.ClickFishImage();
            Assert.IsTrue(Driver.Url.Contains("FISH"));
        }



        [TestMethod]        
        public void VerifyThatDogSidebarLinkIsWorking()
        {
            homepage.ClickDogSidebar();
            Assert.IsTrue(Driver.Url.Contains("DOG"));
        }

        [TestMethod]        
        public void VerifyThatDogImageLinkIsWorking()
        {
            homepage.ClickDogImage();
            Assert.IsTrue(Driver.Url.Contains("DOG"));
        }



        [TestMethod]        
        public void VerifyThatReptilesSidebarLinkIsWorking()
        {
            homepage.ClickReptilesSidebar();
            Assert.IsTrue(Driver.Url.Contains("REPTILES"));
        }

        [TestMethod]        
        public void VerifyThatReptilesImageLinkIsWorking()
        {
            homepage.ClickReptilesImage();
            Assert.IsTrue(Driver.Url.Contains("REPTILES"));
        }



        [TestMethod]        
        public void VerifyThatCatSidebarLinkIsWorking()
        {
            homepage.ClickCatSidebar();
            Assert.IsTrue(Driver.Url.Contains("CAT"));
        }

        [TestMethod]        
        public void VerifyThatCatImageLinkIsWorking()
        {
            homepage.ClickCatImage();
            Assert.IsTrue(Driver.Url.Contains("CAT"));
        }



        [TestMethod]        
        public void VerifyThatBirdsSidebarLinkIsWorking()
        {
            homepage.ClickBirdsSidebar();
            Assert.IsTrue(Driver.Url.Contains("BIRDS"));
        }

        [TestMethod]        
        public void VerifyThatBirdsImageLinkIsWorking()
        {
            homepage.ClickBirdImage();
            Assert.IsTrue(Driver.Url.Contains("BIRDS"));
        }

        [TestMethod]        
        public void VerifyThatBigBirdsImageLinkIsWorking()
        {
            homepage.ClickBirdBigImage();
            Assert.IsTrue(Driver.Url.Contains("BIRDS"));
        }
    }
}
