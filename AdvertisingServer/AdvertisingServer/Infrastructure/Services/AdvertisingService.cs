using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Models;
using AdvertisingServer.Models.Dto.Advertising;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingServer.Infrastructure.Services
{
    public class AdvertisingService : IAdvertisingService
    {
        private readonly MarketingDbContext _context;
        private readonly IMapper _mapper;

        public AdvertisingService(IMapper mapper, MarketingDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AdvertisingBase> GetAdvertisingByIdAndTokenAsync(int id, string token)
        {
            var result = await _context.Advertisings.FirstOrDefaultAsync(a => a.AdvertisingId == id && a.Token == token);
            return _mapper.Map<AdvertisingBase>(result);
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
