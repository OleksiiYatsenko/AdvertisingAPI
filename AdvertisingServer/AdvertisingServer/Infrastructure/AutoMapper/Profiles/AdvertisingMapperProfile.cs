using AdvertisingServer.Models.DbContext;
using AdvertisingServer.Models.Dto.Advertising;
using AutoMapper;

namespace AdvertisingServer.Infrastructure.AutoMapper.Profiles
{
    public class AdvertisingMapperProfile : Profile
    {
        public AdvertisingMapperProfile()
        {
            CreateMap<Advertising, AdvertisingBase>()
                .ForMember(d => d.AdvertisingId, opt => opt.MapFrom(s => s.AdvertisingId))
                .ForMember(d => d.Token, opt => opt.MapFrom(s => s.Token));

            CreateMap<AdvertisingBase, Advertising>()
                .ForMember(d => d.AdvertisingId, opt => opt.MapFrom(s => s.AdvertisingId))
                .ForMember(d => d.Token, opt => opt.MapFrom(s => s.Token));
        }
    }
}
