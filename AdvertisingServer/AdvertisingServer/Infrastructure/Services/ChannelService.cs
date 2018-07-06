using System.Threading.Tasks;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Models.Dto.Channel;

namespace AdvertisingServer.Infrastructure.Services
{
    public class ChannelService : IChannelService
    {
        public async Task<ChannelBase> GetChannelsAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ChannelBase> GetChannelByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ChannelBase> AddChannelAsync(ChannelBase request)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateChannelAsync(ChannelBase request)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteChannelAsync(int channelId)
        {
            throw new System.NotImplementedException();
        }
    }
}
