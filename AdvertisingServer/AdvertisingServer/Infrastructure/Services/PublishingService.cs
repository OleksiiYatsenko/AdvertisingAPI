using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Models;
using AdvertisingServer.Models.DbContext;
using AdvertisingServer.Models.Dto.Publishing;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingServer.Infrastructure.Services
{
    public class PublishingService : IPublishingService
    {
        private readonly IMapper _mapper;
        private readonly MarketingDbContext _db;

        public PublishingService(IMapper mapper, MarketingDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<IEnumerable<PublishingBase>> GetPublishingAsync(string token)
        {
            var result = await _db.Publishings.AsNoTracking().Where(p => p.Token == token).ToArrayAsync();
            return _mapper.Map<PublishingBase[]>(result);
        }

        public async Task<IEnumerable<PublishingBase>> GetPublishingByChannelIdAsync(string token, int channelId)
        {
            var result = await _db.Publishings.AsNoTracking().Where(p => p.Token == token && p.ChannelId == channelId)
                .ToArrayAsync();
            return _mapper.Map<PublishingBase[]>(result);
        }

        public async Task<IEnumerable<PublishingBase>> GetPublishingByAdvertisingIdAsync(string token, int advertisingId)
        {
            var result = await _db.Publishings.AsNoTracking()
                .Where(p => p.Token == token && p.AdvertisingId == advertisingId).ToArrayAsync();
            return _mapper.Map<PublishingBase[]>(result);
        }

        public async Task<PublishingBase> GetPublishingForAdvertisingByChannelIdAsync(int advertisingId, string token,
            int channelId)
        {
            var result = await _db.Publishings.AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.Token == token && p.AdvertisingId == advertisingId && p.ChannelId == channelId);
            return _mapper.Map<PublishingBase>(result);
        }

        public async Task<PublishingBase> GetPublishingById(int id, string token)
        {
            var result = await _db.Publishings.AsNoTracking()
                .FirstOrDefaultAsync(p => p.PublishingId == id && p.Token == token);

            return _mapper.Map<PublishingBase>(result);
        }

        public async Task<PublishingBase> PublishAdAsync(PublishingBase request)
        {
            var publishing = _mapper.Map<Publishing>(request);
            publishing.CreatedDate = DateTime.Now;

            _db.Publishings.Add(publishing);
            await _db.SaveChangesAsync();

            return _mapper.Map<PublishingBase>(publishing);
        }
    }
}
