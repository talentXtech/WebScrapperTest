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

        Task AddLayOffDataAsync(LayOffScrapInfo outputData);

        Task AddRangeLayOffDataAsync(List<LayOffScrapInfo> outputDatas);
        Task DeleteLayOffDataAsync();

        Task<List<LayOffScrapInfo>> FindLayOffDataAsync();

        Task AddSiftedDataAsync(SiftedFinalScrapInfo outputData);
        Task AddRangeSiftedDataAsync(List<SiftedFinalScrapInfo> outputDatas);
        Task DeleteSiftedDataAsync();
        Task<List<SiftedFinalScrapInfo>> FindSiftedDataAsync();

         Task AddRangeSiftedInitialDataAsync(List<SiftedInitialScrapInfo> outputDatas);
        Task DeleteSiftedInitialDataAsync();
        List<string> ListOfurlsNotExistingInDb(List<string> outputDatas);
    }
}