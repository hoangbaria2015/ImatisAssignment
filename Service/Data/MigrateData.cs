using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace Service.Data;

public static class MigrateData
{
    public static void Migrate(IApplicationBuilder app)
    {
        using( var serviceScope = app.ApplicationServices.CreateScope()) 
        {
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()).ConfigureAwait(true);
        }
    }
    
    private static async Task SeedData(AppDbContext context) 
    {
        if (!await context.Packages.AnyAsync())
        {
            Console.WriteLine("-----> Seeding packages");
            await context.Packages.AddRangeAsync(
                new Package { Id = Guid.Parse("99432e6d-d745-4036-bf80-cb6b12c01ab6"), Name = "Basic Package", Description = "10 health check-up items", Price = 99.99M },
                new Package { Id = Guid.Parse("b2ecfb9a-8486-4345-b33f-2762fed64c11"), Name = "Standard Package", Description = "15 health check-up items", Price = 129.99M },
                new Package { Id = Guid.Parse("ed8d8485-9a91-4fc8-9962-276e9e7c1515"), Name = "Advanced Package", Description = "20 health check-up items", Price = 149.99M }
                );

            await context.SaveChangesAsync();
        }
        
        if (!await context.Customers.AnyAsync())
        {
            Console.WriteLine("-----> Seeding customers");
            await context.Customers.AddRangeAsync(
                new Customer { Id = Guid.Parse("ba7a7be1-9fa0-4868-81d5-803cb5f75854"), Name = "IMT Company", Description = "Get a 6 of 4 for Basic Package"},
                new Customer { Id = Guid.Parse("e76a2ee1-02ec-413d-b7dd-5b570032a6c2"), Name = "FWH Company", Description = "On Advanced Package get the price drops to $139.99"},
                new Customer { Id = Guid.Parse("40c437a0-3312-4333-92cd-3f4367880e84"), Name = "DNV Company", Description = "Get a 10 of 5 for Standard Package"}
                );

            await context.SaveChangesAsync();
        }
    }
}