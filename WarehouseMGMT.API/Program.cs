using Microsoft.EntityFrameworkCore;
using WarehouseMGMT.Domain.Repositories;
using WarehouseMGMT.Persistence;
using WarehouseMGMT.Persistence.Repository;
using WarehouseMGMT.Services;
using AutoMapper;
using WarehouseMGMT.Mapping;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<WarehouseMGMTDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("WarehouseMGMTDb")));
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<ICityRepository, CityRepository>();
    builder.Services.AddScoped<ICityService, CityService>();
    builder.Services.AddScoped<ICountryRepository, CountryRepository>();
    builder.Services.AddScoped<ICountryService, CountryService>();
    builder.Services.AddScoped<IItemRepository, ItemRepository>();
    builder.Services.AddScoped<IItemService, ItemService>();
    builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
    builder.Services.AddScoped<IWarehouseService, WarehouseService>();
    builder.Services.AddScoped<IWarehouseContentRepository, WarehouseContentRepository>();
    builder.Services.AddScoped<IWarehouseContentService, WarehouseContentService>();
}



var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.MapControllers();
    
    app.Run();
}


