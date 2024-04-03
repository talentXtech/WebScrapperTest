using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.ConstrainedExecution;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TalentX.WebScrapper.API.Entities;
using TalentX.WebScrapper.API.Extensions;
using TalentX.WebScrapper.API.Interface;
using TalentX.WebScrapper.API.Utilities;

namespace TalentX.WebScrapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebScrapperController : ControllerBase
    {
        private readonly IScrapDataRepo _scrapDataRepo;

        public WebScrapperController(IScrapDataRepo scrapDataRepo)
        {
            _scrapDataRepo = scrapDataRepo;
        }


        [HttpGet("ScrapInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("text/csv")]
        public async Task<FileResult> ScrapInfo(string filterInput)
        {
            try
            {
                var driver = ChromeDriverUtils.CreateChromeDriverHeadless("https://www.allabolag.se/");

                // Deal with compliance overlay
                Thread.Sleep(5000);
                var complianceOverlayElement = driver.FindElements(By.Id("qc-cmp2-ui"));
                if (complianceOverlayElement.Count() > 0) { complianceOverlayElement[0].ClickButton("css-iq8lad", "class"); }


                // enter industry
                Thread.Sleep(2000);
                var input = driver.FindElement(By.Id("search-bar"));
                input.Clear();
                input.SendKeys(filterInput);


                // click search button
                var parentElement = driver.FindElement(By.ClassName("tw-max-w-lg"));
                var submitButton = parentElement.FindElement(By.TagName("button"));
                submitButton.Click();

                //scrapping the titles
                Thread.Sleep(5000);
                // driver.ScrollToBottmOfPage();

                Thread.Sleep(5000);
                var parentElementForTittle = driver.FindElement(By.ClassName("page__main"));
                var allTitles = parentElementForTittle.FindAllByTag("a");

                var scrappedData = new List<InitialScrapOutputData>();

                foreach (var title in allTitles)
                {
                    var initialOutputData = new InitialScrapOutputData
                    {
                        Title = title.Text,
                        Url = title.GetAttribute("href")
                    };
                    scrappedData.Add(initialOutputData);
                }
                driver.Quit();

                var finalScrappedDatas = scrappedData.Where((x) => x.Url != null && x.Title != null).ToList();
                await _scrapDataRepo.DeleteAsync();
                await _scrapDataRepo.AddRangeAsync(finalScrappedDatas);


                ScrapingDetailedDataFromEachLink(finalScrappedDatas);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
           
            var data = await _scrapDataRepo.FindAsync();

            using (var memoryStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new(memoryStream))
                using (CsvWriter csvWriter = new(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(data);
                }

                return File(memoryStream.ToArray(), "text/csv", $"AllabollagScrapper-{DateTime.Now.ToString("s")}.csv");
            }

        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteDatabase()
        {
            await _scrapDataRepo.DeleteAsync();
            await _scrapDataRepo.DeleteDetailedDataAsync();
            return Ok();
        }



            private async Task ScrapingDetailedDataFromEachLink(List<InitialScrapOutputData> finalScrappedDatas)
        {
            ChromeDriver newDriver = null;
            await _scrapDataRepo.DeleteDetailedDataAsync();
            foreach (var data in finalScrappedDatas)
            {
                try
                {
                    newDriver = ChromeDriverUtils.CreateChromeDriverHeadless(data.Url);
                    var newComplianceOverlayElement = newDriver.FindElements(By.Id("qc-cmp2-ui"));
                    if (newComplianceOverlayElement.Count() > 0)
                    {
                        newComplianceOverlayElement[0].ClickButton("css-iq8lad", "class");
                    }


                    Thread.Sleep(5000);
                    var orgnr = newDriver.FindByClass("orgnr");
                    var ceo = newDriver.FindByXPath("//*[@id=\"company-card_overview\"]/div[3]/div[1]/dl/dd[1]/a");
                    var address = newDriver.FindByXPath("//*[@id=\"company-card_overview\"]/div[3]/div[2]/dl/dd[2]/a[1]");
                    var location = newDriver.FindByXPath("//*[@id=\"company-card_overview\"]/div[3]/div[2]/dl/dd[3]");
                    var yearFounded = newDriver.FindByXPath("//*[@id=\"company-card_overview\"]/div[3]/div[1]/dl/dd[5]");
                    var revenue = newDriver.FindByXPath("//*[@id=\"company-card_overview\"]/div[2]/div[2]/div[1]/table/tbody/tr[1]/td[1]");



                    string[] characters = orgnr.Split(":");
                    string[] characters2 = characters[1].Trim().Split("-");

                    var urlForEmployeeDetails = $"https://www.allabolag.se/{characters2[0]}{characters2[1]}/befattningar";

                    newDriver.Navigate().GoToUrl(urlForEmployeeDetails);

                    var employees = newDriver.FindAllByClass("tw-text-allabolag-600");
                    var employeeNames = string.Join('|', employees.Select((x) => x.Text).ToArray());


                    var companyDetail = new DetailedScrapOutputData
                    {
                        CompanyName = data.Title,
                        AllabolagUrl = data.Url,
                        OrgNo = orgnr,
                        CEO = ceo,
                        Address = address,
                        Location = location,
                        YearOfEstablishment = yearFounded,
                        Revenue = revenue,
                        EmployeeNames = employeeNames

                    };
                    await _scrapDataRepo.AddDetailedDataAsync(companyDetail);


                }
                catch (Exception ex)
                {

                    Console.WriteLine("Issue with getting data");
                    var companyDetail = new DetailedScrapOutputData
                    {
                        CompanyName = data.Title,
                        AllabolagUrl = data.Url,
                        OrgNo = "Issue with getting data",
                        CEO = "Issue with getting data",
                        Address = "Issue with getting data",
                        Location = "Issue with getting data",
                        YearOfEstablishment = "Issue with getting data",
                        Revenue = "Issue with getting data",
                        EmployeeNames = "Issue with getting data"

                    };
                    await _scrapDataRepo.AddDetailedDataAsync(companyDetail);
                }
                finally
                {
                    if (newDriver != null)
                    {
                        newDriver.Quit();
                    }

                }
            }
        }
    }
}
