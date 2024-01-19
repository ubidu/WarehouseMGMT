using AutoMapper;
using WarehouseMGMT.Models;
using WarehouseMGMT.Resources;

namespace WarehouseMGMT.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveCountryResource, Country>();
        CreateMap<SaveCityResource, City>();
        CreateMap<SaveWarehouseResource, Warehouse>();
        CreateMap<SaveItemResource, Item>();
        CreateMap<SaveWarehouseContentResource, WarehouseContent>();
    }
}