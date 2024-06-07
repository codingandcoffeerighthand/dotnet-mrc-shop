using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
        {
            return;
        }

        session.Store<Product>(GetPreconfiguredProducts());

        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>(){
        new Product(){
            Id = Guid.NewGuid(),
            Name = "IPhone 15",
            Category = new List<string>(){"Smartphone", "iPhone"},
            Description = "Best and newest iPhone",
            ImageFile = "product-1.png",
            Price = 1800.00M
        },
        new Product(){
            Id = Guid.NewGuid(),
            Name = "Samsung note 13 ultra",
            Category = new List<string>(){"Smartphone", "Samsung"},
            Description = "Best and newest Samsung",
            ImageFile = "product-2.png",
            Price = 1300.00M
        },
        new Product(){
            Id = Guid.NewGuid(),
            Name = "Xiaomi redmi note 11",
            Category = new List<string>(){"Smartphone", "Xiaomi"},
            Description = "Best and newest Xiaomi",
            ImageFile = "product-3.png",
            Price = 1200.00M
        },
        new Product(){
            Id = Guid.NewGuid(),
            Name = "Xiaomi redmi note 12",
            Category = new List<string>(){"Smartphone", "Xiaomi"},
            Description = "Best and newest Xiaomi",
            ImageFile = "product-4.png",
            Price = 1200.00M
        }
    };
}