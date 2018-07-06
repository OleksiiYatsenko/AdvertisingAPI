using AdvertisingServer.Models.Dto.Channel;
using System.Threading.Tasks;

namespace AdvertisingServer.Infrastructure.Interfaces
{
    public interface IChannelService
    {
        Task<ChannelBase> GetChannelsAsync();
        Task<ChannelBase> GetChannelByIdAsync(int id);
        Task<ChannelBase> AddChannelAsync(ChannelBase request);
        Task UpdateChannelAsync(ChannelBase request);
        Task DeleteChannelAsync(int channelId);
    }
}
