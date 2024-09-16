using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PetStore.utilities
{
    static class ElementExtensions
    {
        public static void EnterText(this IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        public static string GetText(this IWebElement element)
        {
            return element.Text.Trim();
        }

        public static bool IsDisplayed(this IWebElement element)
        {
            bool result;
            try
            {
                result = element.Displayed;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public static void ClickOn(this IWebElement element)
        {
            element.Click();
        }

        public static void SelectDropdownValueByText(this IWebElement element, string text)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(text);
        }

        public static void SelectDropdownValueByIndex(this IWebElement element, int index)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByIndex(index);
        }

        public static void SelectDropdownValueByValue(this IWebElement element, string text)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByValue(text);
        }

        public static string SelectDropdownGetSelectedValue(this IWebElement element)
        {
            SelectElement oSelect = new SelectElement(element);
            return oSelect.SelectedOption.Text.Trim();
        }

        public static void Checkbox(this IWebElement element, bool select)
        {
            if (select)
            {
                if (!element.Selected)
                {
                    element.ClickOn();
                }
            }
            else
            {
                if (element.Selected)
                {
                    element.ClickOn();
                }
            }
        }

        public static bool IsCheckboxSelected(this IWebElement element)
        {
            return element.Selected;
        }

        public static void JavaScriptSetValue(this IWebDriver driver, IWebElement element, string value)
        {
            var script = $"arguments[0].value='{value}';";
            driver.JsExecutor().ExecuteScript(script, element);
        }
        public static IJavaScriptExecutor JsExecutor(this IWebDriver driver)
        {
            return driver as IJavaScriptExecutor;
        }

        public static IWebElement JavaScriptScrollToElement(this IWebDriver driver, IWebElement element, bool top = true)
        {
            driver.JsExecutor().ExecuteScript($"arguments[0].scrollIntoView({top.ToString().ToLower()});", element);

            return element;
        }
    }
}
