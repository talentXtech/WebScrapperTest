using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentX.WebScrapper.API.Entities;

namespace TalentX.WebScrapper.API.Interface
{
    public interface IScrapDataRepo
    {
       Task AddAsync(InitialScrapOutputData outputData);

       Task AddRangeAsync(List<InitialScrapOutputData> outputDatas);

       Task AddDetailedDataAsync(DetailedScrapOutputData outputData);
       Task DeleteDetailedDataAsync();
       Task DeleteAsync();
       Task<List<DetailedScrapOutputData>> FindAsync();
    }
}