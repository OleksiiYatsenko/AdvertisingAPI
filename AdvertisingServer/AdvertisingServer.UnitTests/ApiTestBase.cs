using AdvertisingServer.Controllers.V1;
using AdvertisingServer.Infrastructure.Extensions;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Infrastructure.Services;
using AdvertisingServer.Models;
using AdvertisingServer.UnitTests.DataFixture;
using LightInject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingServer.UnitTests
{
    public abstract class ApiTestBase<T> where T : ControllerBase
    {
        protected static IServiceContainer Container;
        protected T Controller;
        protected ApiTestBase()
        {
            Controller = Container.GetInstance<T>();
        }

        protected void InsertTestData(AdvertisingDataFixture advertisingData)
        {
            var db = Container.GetInstance<MarketingDbContext>();
            advertisingData.InsertTestDataToDb(db);
        }

        public static void Configure(IServiceContainer container)
        {
            container.RegisterMapperConfiguration();

            container.Register<IAdvertisingService, AdvertisingService>();
            container.Register<IChannelService, ChannelService>();
            container.Register<IPublishingService, PublishingService>();
            container.Register<AdvertisingController, AdvertisingController>();
            container.Register<ChannelController, ChannelController>();
            container.Register<PushController, PushController>();

            container.Register(_ =>
            {
                var builder = new DbContextOptionsBuilder<MarketingDbContext>();
                builder.UseInMemoryDatabase("MarketingServer");
                return new MarketingDbContext(builder.Options);
            });

            Container = container;
        }
    }
}
