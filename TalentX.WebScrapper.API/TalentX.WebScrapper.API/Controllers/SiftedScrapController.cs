using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TalentX.WebScrapper.API.Entities;
using TalentX.WebScrapper.API.Extensions;
using TalentX.WebScrapper.API.Interface;
using TalentX.WebScrapper.API.Utilities;

namespace TalentX.WebScrapper.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SiftedScrapController : Controller
    {

        private readonly IScrapDataRepo _scrapDataRepo;

        public SiftedScrapController(IScrapDataRepo scrapDataRepo)
        {
            _scrapDataRepo = scrapDataRepo;
        }

        [HttpGet("SiftedScrapInfo")]
        public async Task<ActionResult<List<SiftedFinalScrapInfo>>> SiftedScrapInfo(string email = "abi1243@gmail.com", string password = "Sifted1234!")
        {
            var ListOfInitialScrapDataFromSifted = new List<SiftedInitialScrapInfo>();
            var ListOfFinalScrappedDataFromSifted = new List<SiftedFinalScrapInfo>();
            try
            {
                var driver = ChromeDriverUtils.CreateChromeDriver("https://sifted.eu/sectors");

                Thread.Sleep(2000);
                driver.CloseComplainceOverlayForSifted();

                driver.ClickButtonByClass("ga-nav-link-login");
                Thread.Sleep(2000);
                driver.UserLogin(email, password);

                Thread.Sleep(5000);
                driver.CloseComplainceOverlayForSifted();

                driver.ScrollToBottmOfPage();
                Thread.Sleep(2000);

                var parentElement = driver.FindElement(By.XPath("//*[@id=\"__next\"]/main/section/div"));
                var sectors = parentElement.FindElements(By.TagName("a"));

                foreach (var sector in sectors)
                {
                    var sectorUrl = sector.GetAttribute("href");
                    var sectorName = sectorUrl.Split("/");
                    var outputData = new SiftedInitialScrapInfo
                    {
                        Sectors = sectorName.LastOrDefault(),
                        SectorUrl = sectorUrl
                    };

                    if (!ListOfInitialScrapDataFromSifted.Any(o => o.SectorUrl == outputData.SectorUrl))
                    {
                        ListOfInitialScrapDataFromSifted.Add(outputData);
                    }

                }
                await _scrapDataRepo.AddRangeSiftedInitialDataAsync(ListOfInitialScrapDataFromSifted);



                //foreach (var outputData in ListOfInitialScrapDataFromSifted)
                {
                    var outputData = ListOfInitialScrapDataFromSifted[0];
                    driver.Navigate()
                        .GoToUrl(outputData.SectorUrl);
                    Console.WriteLine($"title: {driver.Title}");

                    Thread.Sleep(2000);
                    driver.CloseComplainceOverlayForSifted();
                    driver.ScrollToBottmOfPage();

                    var listOfArticlesParentElement = driver.FindElement(By.XPath("/html/body/div[2]/main/div[2]/div/ul"));
                    var childElement = listOfArticlesParentElement.FindElements(By.ClassName("articleListCard__link"));
                    var listOfArticleUrls = new List<string>();
                    foreach (var article in childElement)
                    {
                        var url = article.GetAttribute("href");
                        listOfArticleUrls.Add(url);
                        
                    }

                    var filteredUrls = _scrapDataRepo.ListOfurlsNotExistingInDb(listOfArticleUrls);


                    //    foreach (var url in filteredUrls)
                    //    {

                    //        driver.Navigate()
                    //            .GoToUrl(url);

                    //        Thread.Sleep(2000);
                    //        driver.CloseComplainceOverlayForSifted();

                    //        var articleType = driver.FindByXPath("//*[@id=\"__next\"]/main/div/div[1]/div/div/div[1]/div[2]/a/p");
                    //        var date = driver.FindByXPath("//*[@id=\"__next\"]/main/div/div[1]/div/div/div[1]/div[2]/p");
                    //        var subject = driver.FindByXPath("//*[@id=\"__next\"]/main/div/div[1]/div/div/h1/span");
                    //        var summary = driver.FindByXPath("//*[@id=\"__next\"]/main/div/div[1]/div/div/h2/span");
                    //        Thread.Sleep(2000);
                    //        driver.ScrollToBottmOfPage();
                    //        var TagsParentElement = driver.FindElement(By.XPath("//*[@id=\"__next\"]/main/div/div[2]/div[3]"));

                    //        var tagElements = TagsParentElement.FindAllByTag("a");
                    //        var tagList = new List<string>();
                    //        foreach (var item in tagElements)
                    //        {
                    //            var tag = item.FindBySelector("span > span:nth-child(2)");
                    //            tagList.Add(tag);
                    //        }
                    //        var tags = string.Join('|', tagList.Select((x) => x).ToArray());



                    //        var scrapInfoFromArticle = new SiftedFinalScrapInfo
                    //        {
                    //            Sector = outputData.Sectors,
                    //            Sectorurl = outputData.SectorUrl,
                    //            ContentType = articleType,
                    //            Date = date,
                    //            Subject = subject,
                    //            Summary = summary,
                    //            articleUrl = url,
                    //            Tags = tags
                    //        };
                    //        ListOfFinalScrappedDataFromSifted.Add(scrapInfoFromArticle);

                    //    }
                    //}
                    //await _scrapDataRepo.AddRangeSiftedDataAsync(ListOfFinalScrappedDataFromSifted);
                    
                }
                return Ok(ListOfInitialScrapDataFromSifted);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex);
            }


        }


    }
}