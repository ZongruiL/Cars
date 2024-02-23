using AuctionService.Data;
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService;

public class DbInitializer{

    public static void InitDb(WebApplication app){
        using var scope = app.Services.CreateScope();
        SeedData(scope.ServiceProvider.GetService<AuctionDbContext>());
    }

    private static void SeedData(AuctionDbContext context)
    {
        context.Database.Migrate();

        if(context.Auctions.Any()){
            Console.WriteLine("Already have data");
            return;
        }

        var auctions = new List<Auction>()
        {
            new Auction{
                Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                Status = Status.Live,
                ReservePrice = 2000,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(10),
                Item = new Item{
                    Make = "Ford",
                    Model = "GT",
                    Color = "White",
                    Mileage = 5000,
                    Year = 2020,
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/05/06/16/32/car-1376190_960_720.jpg"
                }
                
            }
        };

        context.AddRange(auctions);
        context.SaveChanges();
    }
}