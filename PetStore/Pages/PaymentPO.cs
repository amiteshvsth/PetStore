using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PetStore.Base;
using PetStore.utilities;

namespace PetStore.Pages
{
    internal class PaymentPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By cardType = By.Name("order.cardType");
        private readonly By cardNumber = By.Name("order.creditCard");
        private readonly By expiryDate = By.Name("order.expiryDate");
        private readonly By firstName = By.Name("order.billToFirstName");
        private readonly By lastName = By.Name("order.billToLastName");
        private readonly By address1 = By.Name("order.billAddress1");
        private readonly By address2 = By.Name("order.billAddress2");
        private readonly By city = By.Name("order.billCity");
        private readonly By state = By.Name("order.billState");
        private readonly By zip = By.Name("order.billZip");
        private readonly By country = By.Name("order.billCountry");
        private readonly By shipToDifferentAddress = By.Name("shippingAddressRequired");
        private readonly By continueButton = By.Name("newOrder");

        public void SelectCardType(string type)
        {
            IWebElement cardTypeElem = Wait.UntilElementVisible(cardType);
            SelectElement select = new(cardTypeElem);
            select.SelectByText(type);
        }

        public string GetCardNumber()
        {
            string cardNu = Wait.UntilElementExists(cardNumber).Text;
            return cardNu;
        }
        public void EnterCardNumber(string number)
        {
            Wait.UntilElementExists(cardNumber).EnterText(number);
        }

        public string GetExpiryDate()
        {
            string expiryDat = Wait.UntilElementExists(expiryDate).Text;
            return expiryDat;
        }

        public void EnterExpiryDate(string expiryDat)
        {
            Wait.UntilElementExists(expiryDate).EnterText(expiryDat);
        }

        public string GetFirstName()
        {
            string name = Wait.UntilElementExists(firstName).Text;
            return name;
        }

        public void EnterFirstName(string name)
        {
            Wait.UntilElementExists(firstName).EnterText(name);
        }

        public string GetLastName()
        {
            string last = Wait.UntilElementExists(lastName).Text;
            return last;
        }

        public void EnterLastName(string last)
        {
            Wait.UntilElementExists(lastName).EnterText(last);
        }

        public string GetAddress1()
        {
            string adres1 = Wait.UntilElementExists(address1).Text;
            return adres1;
        }

        public void EnterAddress1(string adres1)
        {
            Wait.UntilElementExists(address1).EnterText(adres1);
        }

        public string GetAddress2()
        {
            string adres2 = Wait.UntilElementExists(address2).Text;
            return adres2;
        }
        public void EnterAddress2(string adres2)
        {
            Wait.UntilElementExists(address2).EnterText(adres2);
        }

        public string GetCity()
        {
            string citi = Wait.UntilElementExists(city).Text;
            return citi;
        }

        public void EnterCity(string citi)
        {
            Wait.UntilElementExists(city).EnterText(citi);
        }

        public string GetState()
        {
            string stat = Wait.UntilElementExists(state).Text;
            return stat;
        }
        public void EnterState(string stat)
        {
            Wait.UntilElementExists(state).EnterText(stat);
        }

        public string GetZip()
        {
            string zeep = Wait.UntilElementExists(zip).Text;
            return zeep;
        }

        public void EnterZip(string zeep)
        {
            Wait.UntilElementExists(zip).EnterText(zeep);
        }

        public string GetCountry()
        {
            string contry = Wait.UntilElementExists(country).Text;
            return contry;
        }
        public void EnterCountry(string contry)
        {
            Wait.UntilElementExists(country).EnterText(contry);
        }

        public void ClickOnShipToDifferentAddress()
        {
            Wait.UntilElementClickable(shipToDifferentAddress).Click();
        }

        public void ClickOnContinueButton()
        {
            Wait.UntilElementClickable(continueButton).Click();
        }
    }
}
