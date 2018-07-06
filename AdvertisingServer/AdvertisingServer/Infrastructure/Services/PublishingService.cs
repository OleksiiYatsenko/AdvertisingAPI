using System.Threading.Tasks;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Models.Dto.Publishing;

namespace AdvertisingServer.Infrastructure.Services
{
    public class PublishingService : IPublishingService
    {
        public async Task<PublishingBase> GetPublishingHistoryByTokenAsync(int adId, string token, int channelId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PublishingBase> PublishAdAsync(PublishingBase request)
        {
            throw new System.NotImplementedException();
        }
    }
}
