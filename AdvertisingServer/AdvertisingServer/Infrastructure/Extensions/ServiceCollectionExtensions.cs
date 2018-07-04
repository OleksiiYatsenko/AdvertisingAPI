using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisingServer.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMarketingServices(this IServiceCollection services)
        {
            services.AddTransient<IAdvertisingService, IAdvertisingService>();
            services.AddTransient<IChannelService, ChannelService>();
            services.AddTransient<IPublishingService, PublishingService>();
        }
    }
}
