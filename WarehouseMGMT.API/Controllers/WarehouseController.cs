using AutoMapper;
using WarehouseMGMT.Services;

namespace WarehouseMGMT.Controllers;

public class WarehouseController : ApiController
{
    private readonly IWarehouseService _warehouseService;
    private readonly IMapper _mapper;
    
    public WarehouseController(IWarehouseService warehouseService, IMapper mapper)
    {
        _warehouseService = warehouseService;
        _mapper = mapper;
    }
}