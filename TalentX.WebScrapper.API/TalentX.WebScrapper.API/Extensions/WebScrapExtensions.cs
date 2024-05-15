using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace TalentX.WebScrapper.API.Extensions
{
    public static class WebScrapExtensions
    {
        public static void ScrollToBottmOfPage(this ChromeDriver driver, int sleepMs = 2000)
        {
            // Keep track of the last height
            var lastHeight = (long)driver.ExecuteScript("return document.body.scrollHeight");

            while (true)
            {
                // Scroll down to the bottom of the page
                driver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");

                // Wait for the new content to load
                Thread.Sleep(sleepMs);

                // Check the new scroll height and compare it with the last scroll height
                var newHeight = (long)driver.ExecuteScript("return document.body.scrollHeight");

                if (newHeight == lastHeight)
                {
                    // End of page, break the loop
                    break;
                }
                lastHeight = newHeight;
            }
        }

        public static void ClickButtonById(this IWebElement parentElement, string str)
        {
            parentElement?.FindElement(By.Id(str)).Click();

        }
        public static void ClickButtonByClass(this ChromeDriver driver, string str)
        {
            driver.FindElement(By.ClassName(str)).Click();
        }

        public static void ClickButtonByClass(this IWebElement buttonElement, string str)
        {
            buttonElement?.FindElement(By.ClassName(str)).Click();
        }



        public static string FindById(this ChromeDriver driver, string id)
        {
            var element = driver.FindElements(By.Id(id));
            if (element.Count > 0)
            {
                var text = element[0].Text;
                return text;
            }
            return "";
        }

        public static string FindByClass(this ChromeDriver driver, string className)
        {
            var element = driver.FindElements(By.ClassName(className));
            if (element.Count > 0)
            {
                var text = element[0].Text;
                return text;
            }
            return "";
        }

        public static IWebElement FindElementByClass(this ChromeDriver driver, string className)
        {
            var element = driver.FindElement(By.ClassName(className));
            return element;
        }

        public static string FindByXPath(this ChromeDriver driver, string xpath)
        {
            var element = driver.FindElements(By.XPath(xpath));
            if (element.Count > 0)
            {
                var text = element[0].Text;
                return text;
            }
            return "";

        }

        public static string FindByTag(this ChromeDriver driver, string tag)
        {
            var element = driver.FindElements(By.TagName(tag));
            if (element.Count > 0)
            {
                var text = element[0].Text;
                return text;
            }
            return "";
        }

        public static string FindByTag(this IWebElement parentElement, string tag)
        {
            var element = parentElement.FindElements(By.TagName(tag));
            if (element.Count > 0)
            {
                var text = element[0].Text;
                return text;
            }
            return "";
        }

        public static string FindBySelectorWithChildDivElement(this IWebElement parentElement, string selector)
        {
            var childElement = parentElement.FindElement(By.CssSelector(selector));
            var element = childElement.FindElements(By.TagName("div"));
            if (element.Count > 0)
            {
                var text = element[0].Text;
                return text;
            }
            return "";
        }
        public static string FindBySelector(this IWebElement parentElement, string selector)
        {
            var element = parentElement.FindElement(By.CssSelector(selector));
            var text = element.Text;
            return text;
        }

        public static string FindByClass(this IWebElement parentElement, string className)
        {
            var element = parentElement.FindElement(By.ClassName(className));
            var text = element.Text;
            return text;
        }



        public static ReadOnlyCollection<IWebElement> FindAllByClass(this ChromeDriver driver, string className)
        {
            var element = driver.FindElements(By.ClassName(className));
            return element;
        }



        public static ReadOnlyCollection<IWebElement> FindAllByTag(this IWebElement parentElement, string tag)
        {
            var elements = parentElement.FindElements(By.TagName(tag));
            return elements;
        }

        public static ReadOnlyCollection<IWebElement> FindAllByClass(this IWebElement parentElement, string className)
        {
            var elements = parentElement.FindElements(By.ClassName(className));
            return elements;
        }

        public static void UserLogin(this ChromeDriver driver , string email , string password)
        {
            var loginEmailInput = driver.FindElement(By.Id("email"));
            loginEmailInput.Clear();
            loginEmailInput.SendKeys(email);

            var continueButton = driver.FindElement(By.XPath("/html/body/div[2]/div/main/div/div/div[2]/div/form/div/div[2]/div/button"));
            continueButton.Click();

            Thread.Sleep(5000);
            var passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.Clear();
            passwordInput.SendKeys(password );

            var continueButton2 = driver.FindElement(By.XPath("/html/body/div[2]/div/main/div/div/div[2]/div/form/div/div[4]/div/button"));
            continueButton2.Click();
        }

        public static void CloseComplainceOverlayForSifted(this ChromeDriver driver)
        {
            var complianceOverlay = driver.FindElements(By.Id("CybotCookiebotDialogBodyButtonsWrapper"));
            if (complianceOverlay.Count > 0)
            {
                complianceOverlay[0].ClickButtonById("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll");
            }
        }
    }
}
