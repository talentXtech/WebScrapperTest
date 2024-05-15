using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using TalentX.WebScrapper.API.Entities;
using TalentX.WebScrapper.API.Extensions;
using TalentX.WebScrapper.API.Interface;
using TalentX.WebScrapper.API.Utilities;

namespace TalentX.WebScrapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class layoffWebScrapController : ControllerBase
    {
        private readonly IScrapDataRepo _scrapDataRepo;

        public layoffWebScrapController(IScrapDataRepo scrapDataRepo)
        {
            _scrapDataRepo = scrapDataRepo;
        }

        [HttpGet("LayOffScrapInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("text/csv")]
        public async Task<FileResult> LayOffScrapInfo()
        {
            var firstdriver = ChromeDriverUtils.CreateChromeDriver("https://layoffs.fyi/");
            var url = firstdriver.FindElement(By.TagName("iframe")).GetAttribute("src");
            var driver = ChromeDriverUtils.CreateChromeDriver(url);
            firstdriver.Quit();
            await _scrapDataRepo.DeleteLayOffDataAsync();


            // Deal with compliance overlay
            Thread.Sleep(2000);
            try
            {
                var complianceOverlayElement = driver.FindElements(By.Id("onetrust-button-group"));
                if (complianceOverlayElement.Count() > 0) { complianceOverlayElement[0].ClickButtonById("onetrust-accept-btn-handler"); }
            }
            catch (Exception)
            {

                Console.WriteLine("Stale error skipped");
            }


            var leftPaneParentElement = driver.FindElementByClass("dataLeftPaneInnerContent");
            var rightPaneParentElement = driver.FindElementByClass("dataRightPaneInnerContent");



            var dataCount = driver.FindByClass("selectionCount");

            string[] countOfData = dataCount.Split(" ");
            int totalNoOfData = int.Parse(countOfData[0], System.Globalization.NumberStyles.AllowThousands);

            EventFiringWebDriver eventFiringWebDriver = new EventFiringWebDriver(driver);

            eventFiringWebDriver.Manage().Window.Maximize();
            var outputDataList = new List<LayOffScrapInfo>();
            var i = 0;
            var j = 0;
            while (i <= totalNoOfData)
            {
                eventFiringWebDriver.ExecuteScript($"document.querySelector('.antiscroll-inner').scrollTop={j * 400};");
                Thread.Sleep(5000);

                var leftPaneRowElements = leftPaneParentElement.FindAllByClass("dataRow");
                var rightPaneRowElements = rightPaneParentElement.FindAllByClass("dataRow");


                foreach (var leftPaneRowElement in leftPaneRowElements)
                {
                    var companyNameElement = leftPaneRowElement.FindByClass("truncate");
                    var rowNumber = leftPaneRowElement.FindByClass("numberText");

                    var rowId = leftPaneRowElement.GetAttribute("data-rowid");

                    try
                    {
                        var rightPaneRowElement = rightPaneRowElements.Where((x) => x.GetAttribute("data-rowid") == rowId).FirstOrDefault();
                        var location = rightPaneRowElement.FindBySelector("div:nth-child(1) > div > span > div");
                        var laidOff = rightPaneRowElement.FindBySelectorWithChildDivElement("div:nth-child(2)");
                        var date = rightPaneRowElement.FindBySelector("div:nth-child(3)");
                        var percentage = rightPaneRowElement.FindBySelectorWithChildDivElement("div:nth-child(4)");
                        var industry = rightPaneRowElement.FindBySelectorWithChildDivElement("div:nth-child(5)");
                        var source = rightPaneRowElement.FindBySelectorWithChildDivElement("div:nth-child(6)");
                        var employees = rightPaneRowElement.FindBySelectorWithChildDivElement("div:nth-child(7)");
                        var stage = rightPaneRowElement.FindBySelectorWithChildDivElement("div:nth-child(8)");
                        var raised = rightPaneRowElement.FindBySelector("div:nth-child(9)");
                        var country = rightPaneRowElement.FindBySelector("div:nth-child(10)");
                        var dateAdded = rightPaneRowElement.FindBySelector("div:nth-child(11)");

                        if (!string.IsNullOrWhiteSpace(rowId) && !string.IsNullOrWhiteSpace(location) && !string.IsNullOrWhiteSpace(companyNameElement))
                        {
                            var info = new LayOffScrapInfo
                            {
                                elementName = rowId,
                                numberText = rowNumber,
                                CompanyName = companyNameElement,
                                LocationHQ = location,
                                LaidOff = laidOff,
                                Date = date,
                                Percentage = percentage,
                                Industry = industry,
                                SourceUrl = source,
                                listOfLaidOffEmployeesUrl = employees,
                                Stage = stage,
                                Raised = raised,
                                Country = country,
                                DateAdded = dateAdded
                            };

                            if (!outputDataList.Any(o => o.elementName == info.elementName))
                            {
                                outputDataList.Add(info);
                            }

                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unable to retrive info");
                    }

                }
                var ids = leftPaneParentElement.FindAllByClass("numberText");
                var validIds = ids.Where((x) => !string.IsNullOrWhiteSpace(x.Text)).ToList();
                


                i = int.Parse(validIds.LastOrDefault().Text) + 1;
                Console.WriteLine(i);
                j++;
            }

            await _scrapDataRepo.AddRangeLayOffDataAsync(outputDataList);

            driver.Quit();

            var data = await _scrapDataRepo.FindLayOffDataAsync();

            using (var memoryStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new(memoryStream))
                using (CsvWriter csvWriter = new(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(data);
                }

                return File(memoryStream.ToArray(), "text/csv", $"LayOffScrapper-{DateTime.Now.ToString("s")}.csv");
            }
        }

    }

}
