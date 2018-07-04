using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace AdvertisingServer.Infrastructure.Extensions
{
    public static class SwaggerExtensions
    {
        public static void RegisterSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(sg =>
            {
                sg.SwaggerDoc("Advertising API server", new Info
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
    }
}
