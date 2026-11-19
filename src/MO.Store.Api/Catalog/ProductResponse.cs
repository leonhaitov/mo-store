namespace MO.Store.Api.Catalog;

public sealed record ProductResponse(
    Guid Id,
    Guid CategoryId,
    string Name,
    string Slug,
    string Description,
    int PriceAgorot,
    int PrepTimeHours);
