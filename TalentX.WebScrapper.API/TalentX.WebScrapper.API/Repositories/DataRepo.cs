using Microsoft.EntityFrameworkCore;
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
                _context.Database.ExecuteSqlRaw("DELETE FROM InitialScrapOutputData");
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
                _context.Database.ExecuteSqlRaw("DELETE FROM DetailedScrapOutputData");
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
              //  if (_context.LayOffScrapInfo.Any(o => o.elementName != outputData.elementName))
            //    {
                    await _context.LayOffScrapInfo.AddAsync(outputData);
              //  }
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
                _context.Database.ExecuteSqlRaw("DELETE FROM LayOffScrapInfo");
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

    }

}
