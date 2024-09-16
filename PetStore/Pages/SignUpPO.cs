using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PetStore.Base;
using PetStore.utilities;

namespace PetStore.Pages
{

    internal class SignUpPO(IWebDriver driver) : BasePO(driver)
    {
        // Generate a user and save the credentials
        readonly DataObject.User generatedUser = GenerateRandomUser.GetUserDetails();

        private readonly By userIdField = By.Name("username");
        private readonly By passwordField = By.Name("password");
        private readonly By repeatedPasswordField = By.Name("repeatedPassword");
        private readonly By firstNameField = By.Name("account.firstName");
        private readonly By lastNameField = By.Name("account.lastName");
        private readonly By emailField = By.Name("account.email");
        private readonly By phoneField = By.Name("account.phone");
        private readonly By address1Field = By.Name("account.address1");
        private readonly By address2Field = By.Name("account.address2");
        private readonly By cityField = By.Name("account.city");
        private readonly By stateField = By.Name("account.state");
        private readonly By zipField = By.Name("account.zip");
        private readonly By countryField = By.Name("account.country");
        private readonly By languagePreferenceField = By.Name("account.languagePreference");
        private readonly By favouriteCategoryField = By.Name("account.favouriteCategoryId");
        private readonly By enableMyListField = By.Name("account.listOption");
        private readonly By enableMyBannerField = By.Name("account.bannerOption");
        private readonly By submitButtonField = By.Name("newAccount");

        public void SignUpUser()
        {
            EnterUserId(generatedUser.UserId);
            EnterPassword(generatedUser.Password);
            EnterRepeatedPassword(generatedUser.Password);
            EnterFirstName(generatedUser.FirstName);
            EnterLastName(generatedUser.LastName);
            EnterEmail(generatedUser.Email);
            EnterPhone(generatedUser.Phone);
            EnterAddress1(generatedUser.Address1);
            EnterAddress2(generatedUser.Address2);
            EnterCity(generatedUser.City);
            EnterState(generatedUser.State);
            EnterZip(generatedUser.Zip);
            SelectCountry(generatedUser.Country);
            SelectLanguagePreference(generatedUser.LanguagePreference);
            SelectFavouriteCategory(generatedUser.FavoriteCategory);
            SetEnableMyList(generatedUser.EnableMyList);
            SetEnableMyBanner(generatedUser.EnableMyBanner);
            ClickSubmitButton();
        }
        public void EnterUserId(string userId)
        {
            Wait.UntilElementExists(userIdField).EnterText(userId);
        }

        public void EnterPassword(string password)
        {
            Wait.UntilElementExists(passwordField).EnterText(password);
        }

        public void EnterRepeatedPassword(string repeatedPassword)
        {
            Wait.UntilElementExists(repeatedPasswordField).EnterText(repeatedPassword);
        }

        public void EnterFirstName(string firstName)
        {
            Wait.UntilElementExists(firstNameField).EnterText(firstName);
        }

        public void EnterLastName(string lastName)
        {
            Wait.UntilElementExists(lastNameField).EnterText(lastName);
        }

        public void EnterEmail(string email)
        {
            Wait.UntilElementExists(emailField).EnterText(email);
        }

        public void EnterPhone(string phone)
        {
            Wait.UntilElementExists(phoneField).EnterText(phone);
        }

        public void EnterAddress1(string address1)
        {
            Wait.UntilElementExists(address1Field).EnterText(address1);
        }

        public void EnterAddress2(string address2)
        {
            Wait.UntilElementExists(address2Field).EnterText(address2);
        }

        public void EnterCity(string city)
        {
            Wait.UntilElementExists(cityField).EnterText(city);
        }

        public void EnterState(string state)
        {
            Wait.UntilElementExists(stateField).EnterText(state);
        }

        public void EnterZip(string zip)
        {
            Wait.UntilElementExists(zipField).EnterText(zip);
        }

        public void SelectCountry(string country)
        {
            Wait.UntilElementExists(countryField).EnterText(country);
        }

        public void SelectLanguagePreference(string languagePreference)
        {
            IWebElement elem = Wait.UntilElementExists(languagePreferenceField);
            SelectElement select = new(elem);

            // Select by visible text
            select.SelectByText(languagePreference);
        }

        public void SelectFavouriteCategory(string favouriteCategory)
        {
            IWebElement elem = Wait.UntilElementExists(favouriteCategoryField);
            SelectElement select = new(elem);

            // Select by visible text
            select.SelectByText(favouriteCategory);
        }

        public void SetEnableMyList(bool enable)
        {
            if (enable != true)
            {
                Wait.UntilElementClickable(enableMyListField).Click();
            }
        }

        public void SetEnableMyBanner(bool enable)
        {
            if (enable != true)
            {
                Wait.UntilElementClickable(enableMyBannerField).Click();
            }
        }

        public void ClickSubmitButton()
        {
            Wait.UntilElementClickable(submitButtonField).Click();
        }


    }
}
