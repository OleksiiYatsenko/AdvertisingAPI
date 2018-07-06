using System.Threading.Tasks;
using AdvertisingServer.Models.Dto.Publishing;

namespace AdvertisingServer.Infrastructure.Interfaces
{
    public interface IPublishingService
    {
        Task<PublishingBase> GetPublishingHistoryByTokenAsync(int adId, string token, int channelId);
        Task<PublishingBase> PublishAdAsync(PublishingBase request);
    }
}
