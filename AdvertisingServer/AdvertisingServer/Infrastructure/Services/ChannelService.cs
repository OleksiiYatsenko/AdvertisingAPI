using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Models;
using AdvertisingServer.Models.DbContext;
using AdvertisingServer.Models.Dto.Channel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingServer.Infrastructure.Services
{
    public class ChannelService : IChannelService
    {
        private readonly IMapper _mapper;
        private readonly MarketingDbContext _db;

        public ChannelService(IMapper mapper, MarketingDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }
        public async Task<IEnumerable<ChannelBase>> GetChannelsAsync()
        {
            var result =  await _db.Channels.AsNoTracking().Where(c => c.IsValid).ToArrayAsync();
            return _mapper.Map<ChannelBase[]>(result);
        }

        public async Task<ChannelBase> GetChannelByIdAsync(int id)
        {
            var result = await _db.Channels.AsNoTracking().FirstOrDefaultAsync(c => c.ChannelId == id && c.IsValid);
            return _mapper.Map<ChannelBase>(result);
        }

        public async Task<ChannelBase> AddChannelAsync(ChannelBase request)
        {
            var channel = _mapper.Map<Channel>(request);
            channel.CreatedDate = DateTime.Now;
            channel.UpdatedDate = channel.CreatedDate;

            _db.Channels.Add(channel);
            await _db.SaveChangesAsync();

            return _mapper.Map<ChannelBase>(channel);
        }

        public async Task UpdateChannelAsync(ChannelBase request)
        {
            var channel = await _db.Channels.FirstOrDefaultAsync(c =>
                c.ChannelId == request.ChannelId);

            if (channel != null)
            {
                _mapper.Map(request, channel);
            }

            await _db.SaveChangesAsync();
        }

        public async Task DeleteChannelAsync(int channelId)
        {
            var channel =
                await _db.Channels.FirstOrDefaultAsync(c => c.ChannelId == channelId);

            _db.Channels.Remove(channel);
            await _db.SaveChangesAsync();
        }
    }
}
