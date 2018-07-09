using AutoMapper;
using LightInject;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Reflection;

namespace AdvertisingServer.Infrastructure.Extensions
{
    public static class ServiceContainerExtensions
    {
        public static void RegisterMapperConfiguration(this IServiceContainer container)
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
            container.Register(_ => mapperConfiguration.CreateMapper(), new PerContainerLifetime());
        }
    }
}
