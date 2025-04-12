using AutoMapper;
using Module34.Contracts.Models.Home;
using Address = Module34.Models.Home.Address;
using HomeOptions = Module34.Models.Home.HomeOptions;

namespace Module34.Profiles;

/// <summary>
/// Настройки маппинга всех сущностей приложения
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// В конструкторе настроим соответствие сущностей при маппинге
    /// </summary>
    public MappingProfile()
    {
        CreateMap<Address, AddressInfo>();
        CreateMap<HomeOptions, InfoResponse>()
            .ForMember(m => m.AddressInfo,
                opt => opt.MapFrom(src => src.Address));
    }
} 
 