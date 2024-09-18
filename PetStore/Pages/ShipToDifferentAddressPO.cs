using OpenQA.Selenium;
using PetStore.Base;
using PetStore.utilities;

namespace PetStore.Pages
{
    internal class ShipToDifferentAddressPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By heading = By.XPath("//th");
        private readonly By firstName = By.Name("order.shipToFirstName");
        private readonly By lastName = By.Name("order.shipToLastName");
        private readonly By shipAddress1 = By.Name("order.shipAddress1");
        private readonly By shipAddress2 = By.Name("order.shipAddress2");
        private readonly By city = By.Name("order.shipCity");
        private readonly By state = By.Name("order.shipState");
        private readonly By zip = By.Name("order.shipZip");
        private readonly By country = By.Name("order.shipCountry");
        private readonly By continueButton = By.Name("newOrder");

        public string GetFirstName()
        {
            // Locate the element for first name and retrieve its value attribute
            string firstNameValue = Wait.UntilElementExists(firstName).GetAttribute("value");
            return firstNameValue;
        }

        public string GetLastName()
        {
            // Locate the element for last name and retrieve its value attribute
            string lastNameValue = Wait.UntilElementExists(lastName).GetAttribute("value");
            return lastNameValue;
        }

        public string GetAddress1()
        {
            // Locate the element for ship address 1 and retrieve its value attribute
            string address1Value = Wait.UntilElementExists(shipAddress1).GetAttribute("value");
            return address1Value;
        }

        public string GetAddress2()
        {
            // Locate the element for ship address 2 and retrieve its value attribute
            string address2Value = Wait.UntilElementExists(shipAddress2).GetAttribute("value");
            return address2Value;
        }

        public string GetCity()
        {
            // Locate the element for city and retrieve its value attribute
            string cityValue = Wait.UntilElementExists(city).GetAttribute("value");
            return cityValue;
        }

        public string GetState()
        {
            // Locate the element for state and retrieve its value attribute
            string stateValue = Wait.UntilElementExists(state).GetAttribute("value");
            return stateValue;
        }

        public string GetZip()
        {
            // Locate the element for zip code and retrieve its value attribute
            string zipValue = Wait.UntilElementExists(zip).GetAttribute("value");
            return zipValue;
        }

        public string GetCountry()
        {
            // Locate the element for country and retrieve its value attribute
            string countryValue = Wait.UntilElementExists(country).GetAttribute("value");
            return countryValue;
        }


        public string GetHeading()
        {
            return Wait.UntilElementExists(heading).Text;
        }
        public void EnterFirstName(string name)
        {
            Wait.UntilElementExists(firstName).EnterText(name);
        }

        public void EnterLastName(string last)
        {
            Wait.UntilElementExists(lastName).EnterText(last);
        }

        public void EnterAddress1(string adres1)
        {
            Wait.UntilElementExists(shipAddress1).EnterText(adres1);
        }

        public void EnterAddress2(string adres2)
        {
            Wait.UntilElementExists(shipAddress2).EnterText(adres2);
        }

        public void EnterCity(string citi)
        {
            Wait.UntilElementExists(city).EnterText(citi);
        }

        public void EnterState(string stat)
        {
            Wait.UntilElementExists(state).EnterText(stat);
        }

        public void EnterZip(string zeep)
        {
            Wait.UntilElementExists(zip).EnterText(zeep);
        }

        public void EnterCountry(string contry)
        {
            Wait.UntilElementExists(country).EnterText(contry);
        }

        public void ClickOnContinueButton()
        {
            Wait.UntilElementClickable(continueButton).Click();
        }
    }
}
