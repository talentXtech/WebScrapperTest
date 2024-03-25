using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using TalentX.WebScrapper.API.Entities;

namespace TalentX.WebScrapper.API.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context)
        {

            if (context.InitialScrapOutputData.Any()) return;

            var testData1 = new InitialScrapOutputData
            {
                Title = "test",
                Url = "testUrl"

            };

            context.InitialScrapOutputData.Add(testData1);
            context.SaveChanges();

            if (context.DetailedScrapOutputData.Any()) return;

            var testData = new DetailedScrapOutputData
            {
                CompanyName = "test",
                AllabolagUrl = "testUrl",
                OrgNo = "test",
                CEO = "test",
                Address = "test",
                Location = "test",
                YearOfEstablishment = "test",
                Revenue = "test",
                EmployeeNames = "test,test"
            };

            context.DetailedScrapOutputData.Add(testData);
            context.SaveChanges();

        }
    }
}
