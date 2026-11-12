using MO.Store.Domain.Common;

namespace MO.Store.Domain.Catalog;

public sealed class Product : Entity
{
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int PriceAgorot { get; private set; }
    public int PrepTimeHours { get; private set; }
    public ProductStatus Status { get; private set; }

    private Product() { } // For EF Core

    public Product(Guid categoryId, string name, string slug, string description, int priceAgorot, int prepTimeHours)
    {
        MoveToCategory(categoryId);
        Rename(name);
        ChangeSlug(slug);
        ChangeDescription(description);
        ChangePrice(priceAgorot);
        ChangePrepTime(prepTimeHours);
        Status = ProductStatus.Draft;
    }

    public void Rename(string name)
    {
        EnsureNotArchived();
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name.Trim();
    }

    public void ChangeSlug(string slug)
    {
        EnsureNotArchived();
        Slug = Common.Slug.Validate(slug);
    }

    public void ChangeDescription(string description)
    {
        EnsureNotArchived();
        ArgumentNullException.ThrowIfNull(description);
        Description = description.Trim();
    }

    public void ChangePrice(int priceAgorot)
    {
        EnsureNotArchived();
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(priceAgorot);
        PriceAgorot = priceAgorot;
    }

    public void ChangePrepTime(int prepTimeHours)
    {
        EnsureNotArchived();
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(prepTimeHours);
        PrepTimeHours = prepTimeHours;
    }

    public void MoveToCategory(Guid categoryId)
    {
        EnsureNotArchived();

        if (categoryId == Guid.Empty)
            throw new ArgumentException("Category is required.", nameof(categoryId));
        
        CategoryId = categoryId;
    }

    public void Publish()
    {
        if (Status is ProductStatus.Draft or ProductStatus.Unavailable)
        {
            Status = ProductStatus.Active;
        }

        else
        {
            throw new InvalidOperationException($"Cannot publish product in status {Status}");
        }
    }

    public void MarkUnavailable()
    {
        if (Status == ProductStatus.Active)
        {
            Status = ProductStatus.Unavailable;
        }

        else
        {
            throw new InvalidOperationException($"Cannot mark product as unavailable in status {Status}");
        }
    }

    public void Archive() => Status = ProductStatus.Archived;

    private void EnsureNotArchived()
    {
        if (Status == ProductStatus.Archived)
            throw new InvalidOperationException("Archived product cannot be modified.");
    }
}