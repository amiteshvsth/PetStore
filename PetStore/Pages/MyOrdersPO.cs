using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PetStore.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Pages
{
    internal class MyOrdersPO(IWebDriver driver) : BasePO(driver)
    {
        private readonly By orderId = By.XPath("//td/a");
        private static By OrderIdByText(string id) => By.XPath($"//td/a[text()='{id}']");
        private static By OrderIdByIndex(int idx) => By.XPath($"(//td/a)[{idx}]");
        private static By OrderTimeById(string id) => By.XPath($"//td/a[text()='{id}']/parent::td/following-sibling::td[1]");
        private static By OrderTimeByIndex(int idx) => By.XPath($"(//td/a/parent::td/following-sibling::td[1])[{idx}]");
        private static By OrderTotalById(string id) => By.XPath($"//td/a[text()='{id}']/parent::td/following-sibling::td[2]");
        private static By OrderTotalByIndex(int idx) => By.XPath($"(//td/a/parent::td/following-sibling::td[2])[{idx}]");

        public List<string> GetAllOrderIds()
        {
            var orderIds = new List<string>();
            var elements = Driver.FindElements(orderId);

            foreach (var element in elements)
            {
                orderIds.Add(element.Text);
            }

            return orderIds;
        }

        public void ClickOrderIdByText(string id)
        {
            Wait.UntilElementExists(OrderIdByText(id)).Click();
        }

        public void ClickOrderIdByIndex(int idx)
        {
            Wait.UntilElementExists(OrderIdByIndex(idx)).Click();
        }

        public string GetOrderIdByIndex(int idx)
        {
            string orderId = Wait.UntilElementExists(OrderIdByIndex(idx)).Text;
            return orderId;
        }

        public string GetOrderTimeById(string id)
        {
            string orderTime = Wait.UntilElementExists(OrderTimeById(id)).Text;
            return orderTime;
        }

        public string GetOrderTimeByIndex(int idx)
        {
            string orderTime = Wait.UntilElementExists(OrderTimeByIndex(idx)).Text;
            return orderTime;
        }

        public string GetOrderTotalById(string id)
        {
            string orderTotal = Wait.UntilElementExists(OrderTotalById(id)).Text;
            return orderTotal;
        }

        public string GetOrderTotalByIndex(int idx)
        {
            string orderTotal = Wait.UntilElementExists(OrderTotalByIndex(idx)).Text;
            return orderTotal;
        }

    }
}
