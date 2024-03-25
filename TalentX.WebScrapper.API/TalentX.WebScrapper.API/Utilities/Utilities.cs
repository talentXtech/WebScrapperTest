using OpenQA.Selenium.Chrome;

namespace TalentX.WebScrapper.API.Utilities
{
    public class ChromeDriverUtils
    {
        public static ChromeDriver CreateChromeDriver(string url)
        {
            var options = new ChromeOptions();
            options.AddArguments("--ignore-ssl-errors", "--headless", "--verbose", "--disable-dev-shm-usage");
            //        "--headless",
            //        "--verbose",
            //        "--disable-dev-shm-usage"
            var driver = new ChromeDriver(options);


            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);

            return driver;
        }
    }
}
