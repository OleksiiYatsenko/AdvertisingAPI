using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Infrastructure.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;

namespace AdvertisingServer.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMarketingServices(this IServiceCollection services)
        {
            services.AddTransient<IAdvertisingService, AdvertisingService>();
            services.AddTransient<IChannelService, ChannelService>();
            services.AddTransient<IPublishingService, PublishingService>();
        }

        public static void RegisterSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(sg =>
            {
                sg.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Advertising server API",
                    Description = "RESTfull service for advertising lifecycle",
                    Contact = new Contact
                    {
                        Email = "creatorcompany@creatorcompany.com",
                        Name = "CreatorCompany",
                        Url = "creatorcompany.azurewebsite.com"
                    }
                });
            });
        }

        public static void RegisterMapperConfiguration(this IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<JObject, JObject>().ConvertUsing(value => value == null ? null : new JObject(value));

                var allTypes = typeof(ServiceCollectionExtensions).Assembly.ExportedTypes.ToArray();
                var profiles = allTypes
                    .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
                    .Where(t => !t.GetTypeInfo().IsAbstract);
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
            services.AddTransient(_ => mapperConfiguration.CreateMapper());
        }
    }
}
