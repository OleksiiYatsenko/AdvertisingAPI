using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisingServer.Models.Dto.Publishing;

namespace AdvertisingServer.Infrastructure.Interfaces
{
    public interface IPublishingService
    {
        Task<IEnumerable<PublishingBase>> GetPublishingAsync(string token);
        Task<IEnumerable<PublishingBase>> GetPublishingByChannelIdAsync(string token, int channelId);
        Task<IEnumerable<PublishingBase>> GetPublishingByAdvertisingIdAsync(string token, int advertisingId);
        Task<PublishingBase> GetPublishingForAdvertisingByChannelIdAsync(int advertisingId, string token, int channelId);
        Task<PublishingBase> GetPublishingById(int id, string token);
        Task<PublishingBase> PublishAdAsync(PublishingBase request);
    }
}
