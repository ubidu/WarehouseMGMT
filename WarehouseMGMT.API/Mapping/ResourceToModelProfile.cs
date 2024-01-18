using AutoMapper;
using WarehouseMGMT.Models;
using WarehouseMGMT.Resources;

namespace WarehouseMGMT.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveCountryResource, Country>();
    }
}