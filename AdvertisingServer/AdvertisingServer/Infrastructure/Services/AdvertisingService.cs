using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Models;
using AdvertisingServer.Models.DbContext;
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
            var result = await _context.Advertisings.Where(a => a.Token == token).ToArrayAsync();
            return _mapper.Map<AdvertisingBase[]>(result);
        }

        public async Task<AdvertisingBase> AddAdvertisingAsync(AdvertisingBase advertisingRequest)
        {
            var advertising = _mapper.Map<Advertising>(advertisingRequest);
            advertising.CreatedDate = DateTime.Now;
            advertising.UpdatedDate = advertising.CreatedDate;

            _context.Advertisings.Add(advertising);
            await _context.SaveChangesAsync();

            return _mapper.Map<AdvertisingBase>(advertising);
        }

        public async Task UpdateAdvertisingAsync(AdvertisingBase advertisingRequest)
        {
            var advertising = await _context.Advertisings.FirstOrDefaultAsync(a =>
                a.AdvertisingId == advertisingRequest.AdvertisingId && a.Token == advertisingRequest.Token);

            if (advertising != null)
            {
                _mapper.Map(advertisingRequest, advertising);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAdvertisingAsync(int id, string token)
        {
            var advertising =
                await _context.Advertisings.FirstOrDefaultAsync(a => a.AdvertisingId == id && a.Token == token);

            _context.Advertisings.Remove(advertising);
            await _context.SaveChangesAsync();
        }
    }
}
