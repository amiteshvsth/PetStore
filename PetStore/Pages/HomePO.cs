using OpenQA.Selenium;
using PetStore.Base;

namespace PetStore.Pages
{
    internal class HomePO(IWebDriver driver) : BasePO(driver)
    {
        //sidebar
        private readonly By fishSideBar = By.XPath("//div[@id='SidebarContent']/a/img[contains(@src, 'fish')]");
        private readonly By dogSideBar = By.XPath("//div[@id='SidebarContent']/a/img[contains(@src, 'dog')]");
        private readonly By reptilesSideBar = By.XPath("//div[@id='SidebarContent']/a/img[contains(@src, 'reptiles')]");
        private readonly By catsSideBar = By.XPath("//div[@id='SidebarContent']/a/img[contains(@src, 'cats')]");
        private readonly By birdsSideBar = By.XPath("//div[@id='SidebarContent']/a/img[contains(@src, 'birds')]");


        //images
        private readonly By birdsBigImage = By.XPath("//area[contains(@href, 'BIRDS')][1]");
        private readonly By fishImage = By.XPath("//area[contains(@href, 'FISH')]");
        private readonly By dogImage = By.XPath("//area[contains(@href, 'DOG')]");
        private readonly By reptilesImage = By.XPath("//area[contains(@href, 'REPTILES')]");
        private readonly By catsImage = By.XPath("//area[contains(@href, 'CATS')]");
        private readonly By birdsImage = By.XPath("//area[contains(@href, 'BIRDS')][2]");
        private readonly By pet = By.XPath("//tr/td/following-sibling::td");


        public string GetTitle()
        {
            return Driver.Title;
        }

        public void ClickFishSidebar()
        {
            Wait.UntilElementClickable(fishSideBar).Click();
        }

        public void ClickFishImage()
        {
            Wait.UntilElementClickable(fishImage).Click();
        }

        public void ClickBirdsSidebar()
        {
            Wait.UntilElementClickable(birdsSideBar).Click();
        }

        public void ClickBirdImage()
        {
            Wait.UntilElementClickable(birdsImage).Click();
        }

        public void ClickBirdBigImage()
        {
            Wait.UntilElementClickable(birdsBigImage).Click();
        }



        public void ClickReptilesSidebar()
        {
            Wait.UntilElementClickable(reptilesSideBar).Click();
        }

        public void ClickReptilesImage()
        {
            Wait.UntilElementClickable(reptilesImage).Click();
        }


        public void ClickDogSidebar()
        {
            Wait.UntilElementClickable(dogSideBar).Click();
        }

        public void ClickDogImage()
        {
            Wait.UntilElementClickable(dogImage).Click();
        }



        public void ClickCatSidebar()
        {
            Wait.UntilElementClickable(catsSideBar).Click();
        }

        public void ClickCatImage()
        {
            Wait.UntilElementClickable(catsImage).Click();
        }



        public List<String> GetAllPetNames()
        {
            // Find all elements using the specified XPath
            IList<IWebElement> elements = Driver.FindElements(pet);

            // Use LINQ to select the text of each element and convert it into a list of strings
            List<string> pets = elements.Select(element => element.Text).ToList();

            // Display the list contents (optional)
            pets.ForEach(Console.WriteLine);
            return pets;

        }

        public List<String> GetAllFishNames()
        {
            ClickFishImage();
            List<string> fishes = GetAllPetNames();
            return fishes;

        }

        public List<String> GetAllReptileNames()
        {
            ClickReptilesImage();
            List<string> reptiles = GetAllPetNames();
            return reptiles;

        }

        public List<String> GetAllDogNames()
        {
            ClickDogImage();
            List<string> dogs = GetAllPetNames();
            return dogs;

        }

        public List<String> GetAllCatNames()
        {
            ClickCatImage();
            List<string> cats = GetAllPetNames();
            return cats;

        }

        public List<String> GetAllBirdNames()
        {
            ClickBirdImage();
            List<string> birds = GetAllPetNames();
            return birds;
        }

        public void ClickCategoryImage(string category)
        {
            switch (category.ToUpper())
            {
                case "FISH":ClickFishImage();
                    break;
                case "REPTILES":ClickReptilesSidebar();
                    break;
                case "DOGS":ClickDogImage();
                    break;
                case "CATS":ClickCatImage();
                    break;
                case "BIRDS":ClickBirdBigImage();
                    break;
                default:
                    throw new ArgumentException("Invalid category: " + category);
            }
        }

        }
}
