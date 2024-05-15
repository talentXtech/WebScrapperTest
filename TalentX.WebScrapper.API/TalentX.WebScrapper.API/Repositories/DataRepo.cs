using Microsoft.EntityFrameworkCore;
using System;
using TalentX.WebScrapper.API.Data;
using TalentX.WebScrapper.API.Entities;
using TalentX.WebScrapper.API.Interface;

namespace TalentX.WebScrapper.API.Repositories
{
    public class DataRepo : IScrapDataRepo

    {
        private readonly DataContext _context;

        public DataRepo(DataContext context)
        {
            _context = context;

        }
        public async Task AddAsync(InitialScrapOutputData initialScrapOutput)
        {

            try
            {
                await _context.InitialScrapOutputData.AddAsync(initialScrapOutput);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task DeleteAsync()
        {

            try
            {
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE InitialScrapOutputData");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task AddRangeAsync(List<InitialScrapOutputData> outputDatas)
        {
            try
            {
                await _context.InitialScrapOutputData.AddRangeAsync(outputDatas);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task AddDetailedDataAsync(DetailedScrapOutputData outputData)
        {

            try
            {
                await _context.DetailedScrapOutputData.AddAsync(outputData);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public async Task DeleteDetailedDataAsync()
        {

            try
            {
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE DetailedScrapOutputData");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<DetailedScrapOutputData>> FindAsync()
        {

            try
            {
                var list = _context.DetailedScrapOutputData.ToList();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task AddLayOffDataAsync(LayOffScrapInfo outputData)
        {

            try
            {
                if (!_context.LayOffScrapInfo.Any(o => o.elementName == outputData.elementName))
                {
                    await _context.LayOffScrapInfo.AddAsync(outputData);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        public async Task AddRangeLayOffDataAsync(List<LayOffScrapInfo> outputDatas)
        {

            try
            {
                await _context.LayOffScrapInfo.AddRangeAsync(outputDatas);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        public async Task DeleteLayOffDataAsync()
        {

            try
            {
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE LayOffScrapInfo");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<List<LayOffScrapInfo>> FindLayOffDataAsync()
        {
            try
            {
                var list = _context.LayOffScrapInfo.ToList();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task AddSiftedDataAsync(SiftedFinalScrapInfo outputData)
        {
            try
            {

                {
                    await _context.SiftedFinalScrapInfo.AddAsync(outputData);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        public async Task AddRangeSiftedDataAsync(List<SiftedFinalScrapInfo> outputDatas)
        {
            try
            {
                await _context.SiftedFinalScrapInfo.AddRangeAsync(outputDatas);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }


        }
        public async Task DeleteSiftedDataAsync()
        {
            try
            {
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE SiftedFinalScrapInfo");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        public async Task<List<SiftedFinalScrapInfo>> FindSiftedDataAsync()
        {
            try
            {
                var list = _context.SiftedFinalScrapInfo.ToList();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<string> ListOfurlsNotExistingInDb(List<string> outputDatas)
        {
            try
            {
                List<string> filteredUrls = new();
                foreach (var item in outputDatas)
                {
                    if (!_context.SiftedFinalScrapInfo.Any(o => o.articleUrl == item))
                    {
                        filteredUrls.Add(item);
                        Console.WriteLine(item);
                    }
                   
                }
                return filteredUrls;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task AddRangeSiftedInitialDataAsync(List<SiftedInitialScrapInfo> outputDatas)
        {
            try
            {
                List<SiftedInitialScrapInfo> filteredDatas = new();
                foreach (var item in outputDatas)
                {
                    
                   if (_context.SiftedInitialScrapInfo.Any(o => o.SectorUrl == item.SectorUrl))
                    {
                        filteredDatas.Add(item);
                    }
                }
                await _context.SiftedInitialScrapInfo.AddRangeAsync(filteredDatas);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }


        }
        public async Task DeleteSiftedInitialDataAsync()
        {
            try
            {
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE SiftedInitialScrapInfo");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }

}
