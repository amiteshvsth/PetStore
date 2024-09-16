using OpenQA.Selenium;
using PetStore.Base;
using PetStore.utilities;

namespace PetStore.Pages
{
    internal class ShipToDifferentAddressPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By firstName = By.Name("order.shipToFirstName");
        private readonly By lastName = By.Name("order.shipToLastName");
        private readonly By shipAddress1 = By.Name("order.shipAddress1");
        private readonly By shipAddress2 = By.Name("order.shipAddress2");
        private readonly By city = By.Name("order.shipCity");
        private readonly By state = By.Name("order.shipState");
        private readonly By zip = By.Name("order.shipZip");
        private readonly By country = By.Name("order.shipCountry");
        private readonly By continueButton = By.Name("newOrder");



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
