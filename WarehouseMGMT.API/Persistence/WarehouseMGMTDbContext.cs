using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WarehouseMGMT.Models;

namespace WarehouseMGMT.Persistence;

public class WarehouseMGMTDbContext : DbContext
{
    public WarehouseMGMTDbContext(DbContextOptions<WarehouseMGMTDbContext> options) : base(options)
    {
    }
    
    public DbSet<Warehouse> Warehouses { get; set; } = null!;
    public DbSet<WarehouseContent> WarehouseContents { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Warehouse>()
            .HasMany(e => e.WarehouseContents)
            .WithOne(e => e.Warehouse)
            .HasForeignKey(e => e.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);
        

        builder.Entity<WarehouseContent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Item)
                .WithMany()
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
    }
}