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

        public static void ClickButton(this IWebElement complianceOverlayElement, string className)
        {
           
                complianceOverlayElement?.FindElement(By.ClassName(className)).Click();
            
        }





        public static string FindById(this  ChromeDriver driver, string id) 
        {
            var element = driver.FindElements(By.Id(id));
            if (element.Count > 0)
            {
                var text = element[0].Text;
                return text;
            }
            return "Details Not Available";
        }

        public static string FindByClass(this ChromeDriver driver, string className)
        {
            var element = driver.FindElements(By.ClassName(className));
            if (element.Count > 0)
            {
                var text = element[0].Text;
                return text;
            }
            return "Details Not Available";
        }

        public static string FindByXPath(this ChromeDriver driver, string xpath)
        {
            var element = driver.FindElements(By.XPath(xpath));
            if (element.Count > 0) {
                var text = element[0].Text;
                return text;
            }  return "Details Not Available";
            
        }

        public static string FindByTag(this ChromeDriver driver, string tag)
        {
            var element = driver.FindElements(By.TagName(tag));
            if (element.Count > 0)
            {
                var text = element[0].Text;
                return text;
            }
            return "Details Not Available";
        }

        public static string FindByTag(this IWebElement parentElement, string tag)
        {
            var element = parentElement.FindElements(By.TagName(tag));
            if (element.Count > 0)
            {
                var text = element[0].Text;
                return text;
            }
            return "Details Not Available";
        }

        public static ReadOnlyCollection<IWebElement> FindAllByClass(this ChromeDriver driver, string className)
        {
            var element = driver.FindElements(By.ClassName(className));
            return element;
        }



        public static ReadOnlyCollection<IWebElement>  FindAllByTag(this IWebElement parentElement, string tag)
        {
            var elements = parentElement.FindElements(By.TagName(tag));
            return elements;
        }
    }
}
