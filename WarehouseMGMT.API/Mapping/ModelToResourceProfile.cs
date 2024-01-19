using AutoMapper;
using WarehouseMGMT.Models;
using WarehouseMGMT.Resources;

namespace WarehouseMGMT.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<City, CityResource>();
        CreateMap<Country, CountryResource>();
        CreateMap<Item, ItemResource>();
        CreateMap<Warehouse, WarehouseResource>();
        CreateMap<WarehouseContent, WarehouseContentResource>();
    }
}