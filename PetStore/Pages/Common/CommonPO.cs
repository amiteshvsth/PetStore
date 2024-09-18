using OpenQA.Selenium;
using PetStore.Base;
using PetStore.utilities;

namespace PetStore.Pages.Common
{
    internal class CommonPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly HomePO homePage = new(driver);
        readonly CSharpHelpers helper = new();

        //header
        private readonly By signInLink = By.LinkText("Sign In");
        private readonly By signOutLink = By.LinkText("Sign Out");
        private readonly By myAccountLink = By.LinkText("My Account");
        private readonly By cartIcon = By.XPath("//img[contains(@src, 'cart')]");
        private readonly By helpIcon = By.XPath("//a[contains(@href, 'help')]");
        private readonly By logo = By.XPath("//img[contains(@src, 'logo')]");
        private readonly By searchField = By.Name("keyword");
        private readonly By searchButton = By.Name("searchProducts");


        //navbar
        private readonly By fishNavbar = By.XPath("//div[@id='QuickLinks']/a/img[contains(@src,'fish')]");
        private readonly By dogNavbar = By.XPath("//div[@id='QuickLinks']/a/img[contains(@src,'dog')]");
        private readonly By reptilesNavbar = By.XPath("//div[@id='QuickLinks']/a/img[contains(@src,'reptiles')]");
        private readonly By catsNavbar = By.XPath("//div[@id='QuickLinks']/a/img[contains(@src,'cats')]");
        private readonly By birdsNavbar = By.XPath("//div[@id='QuickLinks']/a/img[contains(@src,'birds')]");

        //footer
        private readonly By octoPerf = By.LinkText("OctoPerf");
        private readonly By octoPerf2 = By.LinkText("https://octoperf.com");
        private readonly By mybatis = By.LinkText("www.mybatis.org");
        private static By SearchValue(string search) => By.XPath($"//td[contains(text(), '{search}')]");

        public void ClickSignOutLink()
        {
            // Wait until the 'Sign Out' link is available and click it
            Wait.UntilElementClickable(signOutLink).Click();
        }

        public void ClickMyAccountLink()
        {
            // Wait until the 'My Account' link is available and click it
            Wait.UntilElementClickable(myAccountLink).Click();
        }


        public void ClickFishNavbar()
        {
            Wait.UntilElementClickable(fishNavbar).Click();
        }

        public void ClickBirdsNavbar()
        {
            Wait.UntilElementClickable(birdsNavbar).Click();
        }

        public void ClickReptilesNavbar()
        {
            Wait.UntilElementClickable(reptilesNavbar).Click();
        }

        public void ClickCatNavbar()
        {
            Wait.UntilElementClickable(catsNavbar).Click();
        }

        public void ClickDogNavbar()
        {
            Wait.UntilElementClickable(dogNavbar).Click();
        }



        public IWebElement Search(string searchText)
        {
            Wait.UntilElementVisible(searchField).EnterText(searchText);
            Wait.UntilElementClickable(searchButton).Click();
            string search = helper.GetLastWord(searchText);
            IWebElement elem = Driver.FindElement(SearchValue(search));
            return elem;
        }

        public void ClickSignInLink()
        {
            Wait.UntilElementClickable(signInLink).Click();
        }

        public void ClickCartIcon()
        {
            Wait.UntilElementClickable(cartIcon).Click();
        }

        public void Clickhelp()
        {
            Wait.UntilElementClickable(helpIcon).Click();
        }

        public void ClickOctoPerfLink()
        {
            Wait.UntilElementClickable(octoPerf).Click();
        }

        public void ClickOctoPerf2Link()
        {
            Wait.UntilElementClickable(octoPerf2).Click();
        }

        public void ClickMybatisLink()
        {
            Wait.UntilElementClickable(mybatis).Click();
            Driver.SwitchToLastWindow();
        }

        public void ClickLogo()
        {
            Wait.UntilElementClickable(logo).Click();
            Driver.RefreshPage();
        }

        // Method to provide raw data
        private static Dictionary<string, List<string>> GetPetData()
        {
            return new Dictionary<string, List<string>>
        {
            { "FISH", new List<string>
                {
                    "Large Angelfish",
                    "Small Angelfish",
                    "Toothless Tiger Shark",
                    "Spotted Koi",
                    "Spotless Koi",
                    "Adult Male Goldfish",
                    "Adult Female Goldfish"
                }
            },
            { "DOGS", new List<string>
                {
                    "Male Adult Bulldog",
                    "Female Puppy Bulldog",
                    "Male Puppy Poodle",
                    "Spotless Male Puppy Dalmation",
                    "Spotted Adult Female Dalmation",
                    "Adult Female Golden Retriever",
                    "Adult Male Labrador Retriever",
                    "Adult Female Labrador Retriever",
                    "Adult Male Chihuahua",
                    "Adult Female Chihuahua"
                }
            },
            { "REPTILES", new List<string>
                {
                    "Venomless Rattlesnake",
                    "Rattleless Rattlesnake",
                    "Green Adult Iguana"
                }
            },
            { "CATS", new List<string>
                {
                    "With tail Manx",
                    "Tailless Manx",
                    "Adult Female Persian",
                    "Adult Male Persian"
                }
            },
            { "BIRDS", new List<string>
                {
                    "Adult Male Amazon Parrot",
                    "Adult Male Finch"
                }
            }
        };
        }

        // Method to get both categories and items
        public static IEnumerable<object[]> GetAllPetsWithCategories()
        {
            var allData = GetPetData();

            foreach (var category in allData)
            {
                foreach (var item in category.Value)
                {
                    yield return new object[] { category.Key, item };
                }
            }
        }

        // Method to get only categories
        public static IEnumerable<object[]> GetAllCategories()
        {
            var categories = GetPetData().Keys.ToList();

            foreach (var category in categories)
            {
                yield return new object[] { category };
            }
        }

        // Method to get only items
        public static IEnumerable<object[]> GetAllPets()
        {
            var allData = GetPetData();

            var items = new List<string>();
            foreach (var category in allData)
            {
                items.AddRange(category.Value);
            }

            foreach (var item in items)
            {
                yield return new object[] { item };
            }
        }

        private static readonly char[] Separator = [' '];

        public static string GetLastWord(string text)
        {
            var words = text.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            return words.Length > 0 ? words[^1] : string.Empty;
        }

        public static string GetLastTwoWords(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            var words = input.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            return words.Length < 2 ? input : $"{words[^2]} {words[^1]}";
        }

        public void RemoveTabsAndNewLineCharacters(By xpath)
        {
            var elements = Driver.FindElements(xpath).ToList();
            if (elements.Count == 0)
            {
                Console.WriteLine("No elements found with the given XPath.");
                return;
            }

            foreach (var element in elements)
            {
                string originalText = element.Text;
                string cleanedText = originalText.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ");
                Console.WriteLine("Original Text: " + originalText);
                Console.WriteLine("Cleaned Text: " + cleanedText);
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].textContent = arguments[1];", element, cleanedText);
            }
        }

        public List<string> GetPetNames(string category)
        {
            return category.ToUpper() switch
            {
                "FISH" => homePage.GetAllFishNames(),
                "REPTILES" => homePage.GetAllReptileNames(),
                "DOGS" => homePage.GetAllDogNames(),
                "CATS" => homePage.GetAllCatNames(),
                "BIRDS" => homePage.GetAllBirdNames(),
                _ => []
            };
        }

        public void NavigateToCategory(string category)
        {
            homePage.ClickCategoryImage(category);
            Assert.IsTrue(Driver.Url.Contains(category));
        }

    }
}
