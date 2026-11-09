using MO.Store.Domain.Common;

namespace MO.Store.Domain.Catalog;

public sealed class Category : Entity
{
    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public int SortOrder { get; private set; }
    public bool IsActive { get; private set; }

    private Category() { } // For EF Core

    public Category(string name, string slug, int sortOrder)
    {
        Rename(name);
        ChangeSlug(slug);
        SortOrder = sortOrder;
        IsActive = true;
    }

    public void Rename(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name.Trim();
    }

    public void ChangeSlug(string slug) => Slug = Common.Slug.Validate(slug);

    public void ChangeSortOrder(int sortOrder) => SortOrder = sortOrder;

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;
}