using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TalentX.WebScrapper.API.Entities;

namespace TalentX.WebScrapper.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<InitialScrapOutputData> InitialScrapOutputData { get; set; }
        public DbSet<DetailedScrapOutputData> DetailedScrapOutputData { get; set; }

    }
}
