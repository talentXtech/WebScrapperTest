using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        // [ProducesResponseType(StatusCodes.Status200OK)]
        //[Produces("text/csv")]
        public async Task<ActionResult<List<LayOffScrapInfo>>> LayOffScrapInfo()
        {
            var driver = ChromeDriverUtils.CreateChromeDriver("https://airtable.com/app1PaujS9zxVGUZ4/shrqYt5kSqMzHV9R5/tbl8c8kanuNB6bPYr?backgroundColor=green&viewControls=on");

            // Deal with compliance overlay
            Thread.Sleep(2000);
            var complianceOverlayElement = driver.FindElements(By.Id("onetrust-button-group"));
            if (complianceOverlayElement.Count() > 0) { complianceOverlayElement[0].ClickButton("onetrust-accept-btn-handler", "id"); }

            var leftPaneParentElement = driver.FindElementByClass("dataLeftPaneInnerContent");
            var rightPaneParentElement = driver.FindElementByClass("dataRightPaneInnerContent");
            var rightPaneRowElements = rightPaneParentElement.FindElements(By.ClassName("dataRow"));


            var dataCount = driver.FindByClass("selectionCount");

            string[] countOfData = dataCount.Split(" ");
            int totalNoOfData = int.Parse(countOfData[0], System.Globalization.NumberStyles.AllowThousands);

            EventFiringWebDriver eventFiringWebDriver = new EventFiringWebDriver(driver);

            eventFiringWebDriver.Manage().Window.Maximize();
            var outputDataList = new List<LayOffScrapInfo>();
            var i = 0;
            var j = 0;
            while (i < 20)
            {
                eventFiringWebDriver.ExecuteScript($"document.querySelector('.antiscroll-inner').scrollTop={j * 300};");
                Thread.Sleep(2000);

                var leftPaneRowElements = leftPaneParentElement.FindAllByClass("dataRow");


                foreach (var leftPaneRowElement in leftPaneRowElements)
                {
                    var companyNameElement = leftPaneRowElement.FindByClass("truncate");

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
                                CompanyName = companyNameElement,
                                // LocationHQ = location,
                                // LaidOff = laidOff,
                                // Date = date,
                                // Percentage = percentage,
                                // Industry = industry,
                                // SourceUrl = source,
                                // listOfLaidOffEmployeesUrl = employees,
                                // Stage = stage,
                                // Raised = raised,
                                // Country = country,
                                // DateAdded = dateAdded
                            };
                            outputDataList.Add(info);

                            await _scrapDataRepo.AddLayOffDataAsync(info);
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
                j++;
            }
            return Ok(outputDataList);
        }

    }

}
