using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Models;
using AdvertisingServer.Models.Dto.Advertising;

namespace AdvertisingServer.Infrastructure.Services
{
    public class AdvertisingService : IAdvertisingService
    {
        private readonly MarketingDbContext _context;

        public AdvertisingService(MarketingDbContext context)
        {
            _context = context;
        }

        public async Task<AdvertisingBase> GetAdvertisingByIdAndTokenAsync(int id, string token)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<AdvertisingBase>> GetListOfAdvertisingByTokenAsync(string token)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AdvertisingBase> AddAdvertisingAsync(AdvertisingBase advertising)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateAdvertisingAsync(AdvertisingBase advertising)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAdvertisingAsync(int id, string token)
        {
            throw new System.NotImplementedException();
        }
    }
}
