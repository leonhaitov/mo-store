using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MO.Store.Domain.Catalog;

namespace MO.Store.Infrastructure.Persistence.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Slug)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(p => p.Slug).IsUnique();

        builder.Property(p => p.Description)
            .HasMaxLength(4000)
            .IsRequired();

        builder.Property(p => p.Status)
            .HasMaxLength(20)
            .HasConversion<string>();
    }
}
