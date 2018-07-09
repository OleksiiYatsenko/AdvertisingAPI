using AdvertisingServer.Controllers.V1;
using AdvertisingServer.Models;
using AdvertisingServer.Models.Dto.Advertising;
using AdvertisingServer.UnitTests.DataFixture;
using FluentAssertions;
using LightInject.xUnit2;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;
using AdvertisingServer.UnitTests.Extesions;

namespace AdvertisingServer.UnitTests
{
    public class AdvertisingTests : ApiTestBase<AdvertisingController>, IClassFixture<AdvertisingDataFixture>
    {
        private readonly AdvertisingDataFixture _advertisingData;

        public AdvertisingTests(AdvertisingDataFixture advertisingData)
        {
            _advertisingData = advertisingData;
            InsertTestData(advertisingData);
        }

        [Theory, InjectData]
        public async Task AdvertisingShouldGetEntitesByIdandToken(MarketingDbContext db)
        {
            //arrange
            var ad = _advertisingData.GetRandomAdvertising(db);

            //act
            var result = await Controller.Get(ad.AdvertisingId, ad.Token);

            //assert
            var advertising = result.ToModel<AdvertisingBase, OkObjectResult>();
            advertising.AdvertisingId.Should().Be(ad.AdvertisingId);
            advertising.Token.Should().Be(ad.Token);
        }

        [Theory, InjectData]
        public async Task AdvertisingNotFound(MarketingDbContext db)
        {
            //arrange
            var id = _advertisingData.GetNoneExistingAdvertisingId(db);
            var token = _advertisingData.GetRandomToken();

            //act
            var result = await Controller.Get(id, token);

            //assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task AdvertisingControllerGetReturnsBadRequest()
        {
            //arrange
            var token = _advertisingData.GetRandomToken();

            //act
            var result = await Controller.Get(0, token);

            //assert
            result.Result.Should().BeOfType<BadRequestObjectResult>();
            
        }
    }
}
