using AdvertisingServer.Models.Dto.Advertising;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdvertisingServer.Infrastructure.Interfaces
{
    public interface IAdvertisingService
    {
        Task<AdvertisingBase> GetAdvertisingByIdAndTokenAsync(int id, string token);
        Task<IEnumerable<AdvertisingBase>> GetListOfAdvertisingByTokenAsync(string token);
        Task<AdvertisingBase> AddAdvertisingAsync(AdvertisingBase advertising);
        Task UpdateAdvertisingAsync(AdvertisingBase advertising);
        Task DeleteAdvertisingAsync(int id, string token);
    }
}
