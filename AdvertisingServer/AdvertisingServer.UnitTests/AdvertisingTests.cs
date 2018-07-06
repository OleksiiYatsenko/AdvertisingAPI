using AdvertisingServer.Controllers.V1;
using AdvertisingServer.Models;
using AdvertisingServer.UnitTests.DataFixture;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace AdvertisingServer.UnitTests
{
    public class AdvertisingTests //: ApiTestBase<AdvertisingController>, IClassFixture<AdvertisingDataFixture>
    {
        private readonly AdvertisingDataFixture _advertisingData;

        public AdvertisingTests(AdvertisingDataFixture advertisingData)
        {
            _advertisingData = advertisingData;
        }

        [Fact]
        public void AdvertisingShouldGetEntitesByIdandToken()
        {
            //MarketingDbContext db = new MarketingDbContext();
            //var ad = _advertisingData.GetRandomAdvertising(db);
            //MyMvc.Controller<AdvertisingController>()
            //    .Calling(async c => await c.Get(ad.AdvertisingId, _advertisingData.GetRandomToken())).ShouldReturn()
            //    .Ok();
            //Controller.Calling(x => x.Get(_advertisingData.GetRandomToken())).ShouldReturn().Ok();
        }
    }
}
