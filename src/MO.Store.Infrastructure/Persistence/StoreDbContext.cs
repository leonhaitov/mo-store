using Microsoft.EntityFrameworkCore;
using MO.Store.Domain.Catalog;

namespace MO.Store.Infrastructure.Persistence;

public sealed class StoreDbContext(DbContextOptions<StoreDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
    }
}
