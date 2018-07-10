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
    /// <summary>
    /// Bussiness logic and data access layer for advertising
    /// </summary>
    /// <seealso cref="AdvertisingServer.Infrastructure.Interfaces.IAdvertisingService" />
    public class AdvertisingService : IAdvertisingService
    {
        private readonly MarketingDbContext _db;
        private readonly IMapper _mapper;

        public AdvertisingService(IMapper mapper, MarketingDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<AdvertisingBase> GetAdvertisingByIdAndTokenAsync(int id, string token)
        {
            var result = await _db.Advertisings.AsNoTracking().FirstOrDefaultAsync(a => a.AdvertisingId == id && a.Token == token);
            return _mapper.Map<AdvertisingBase>(result);
        }

        public async Task<IEnumerable<AdvertisingBase>> GetListOfAdvertisingByTokenAsync(string token)
        {
            var result = await _db.Advertisings.AsNoTracking().Where(a => a.Token == token).ToArrayAsync();
            return _mapper.Map<AdvertisingBase[]>(result);
        }

        public async Task<AdvertisingBase> AddAdvertisingAsync(AdvertisingBase advertisingRequest)
        {
            var advertising = _mapper.Map<Advertising>(advertisingRequest);
            advertising.CreatedDate = DateTime.Now;
            advertising.UpdatedDate = advertising.CreatedDate;

            _db.Advertisings.Add(advertising);
            await _db.SaveChangesAsync();

            return _mapper.Map<AdvertisingBase>(advertising);
        }

        public async Task UpdateAdvertisingAsync(AdvertisingBase advertisingRequest)
        {
            var advertising = await _db.Advertisings.FirstOrDefaultAsync(a =>
                a.AdvertisingId == advertisingRequest.AdvertisingId && a.Token == advertisingRequest.Token);

            if (advertising != null)
            {
                _mapper.Map(advertisingRequest, advertising);
            }

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAdvertisingAsync(int id, string token)
        {
            var advertising =
                await _db.Advertisings.FirstOrDefaultAsync(a => a.AdvertisingId == id && a.Token == token);

            _db.Advertisings.Remove(advertising);
            await _db.SaveChangesAsync();
        }
    }
}
