using Microsoft.EntityFrameworkCore;
using MO.Store.Domain.Catalog;

namespace MO.Store.Infrastructure.Persistence.Seeding;

public static class DevDataSeeder
{
    public static async Task SeedAsync(StoreDbContext db, CancellationToken ct = default)
    {
        if (await db.Categories.AnyAsync(ct))
        {
            return;
        }

        var scented = new Category("נרות ריחניים", "scented-candles", 1);
        var decorative = new Category("נרות דקורטיביים", "decorative-candles", 2);

        List<Product> publishedProducts =
        [
            new Product(scented.Id, "נר לבנדר ווניל מרגיע", "lavender-vanilla-calm-candle", "ניחוח רך של לבנדר וניל, מושלם לערב רגוע.", 2500, 2),
        new Product(scented.Id, "נר עץ סנדל ותה לבן", "sandalwood-white-tea-candle", "ריח עצי וחמים עם נגיעה רעננה של תה לבן.", 4000, 4),
        new Product(decorative.Id, "נר עמוד שיש לבן", "white-marble-pillar-candle", "נר עמוד אלגנטי בדוגמת שיש, לעיצוב שולחן ומדפים.", 3500, 24),
        new Product(decorative.Id, "נר פסל גוף בגוון ניוד", "nude-body-sculpture-candle", "נר מעוצב בצורת פסל, שהוא גם פריט עיצוב לבית.", 2500, 6)
        ];

        foreach (var product in publishedProducts)
        {
            product.Publish();
        }

        var draftProduct = new Product(scented.Id, "נר פריחת הדרים", "citrus-blossom-candle", "ניחוח הדרים קליל ומרענן שממלא את הבית באנרגיה.", 3000, 2);

        db.Categories.AddRange(scented, decorative);
        db.Products.AddRange(publishedProducts);
        db.Products.Add(draftProduct);

        await db.SaveChangesAsync(ct);
    }
}
