using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Infrastructure.Services;
using AdvertisingServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTested.AspNetCore.Mvc;

namespace AdvertisingServer.UnitTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration) { }

        public void ConfigureTestService(IServiceCollection services)
        {
            base.ConfigureServices(services);
            
            services.Replace<IAdvertisingService, AdvertisingService>(ServiceLifetime.Transient);
            services.Replace<IChannelService, ChannelService>(ServiceLifetime.Transient);
            services.Replace<IPublishingService, PublishingService>(ServiceLifetime.Transient);

            services.AddDbContextPool<MarketingDbContext>(opt => opt.UseInMemoryDatabase("MarketingDb"));
        }
    }
}
