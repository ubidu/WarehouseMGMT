using AutoMapper;
using WarehouseMGMT.Models;
using WarehouseMGMT.Resources;
using WarehouseMGMT.Services;

namespace WarehouseMGMT.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Country, CountryResource>();
        CreateMap<City, CityResource>();
        CreateMap<Item, ItemResource>();
        CreateMap<Warehouse, WarehouseResource>();
        CreateMap<WarehouseContent, WarehouseContentResource>();
    }
}