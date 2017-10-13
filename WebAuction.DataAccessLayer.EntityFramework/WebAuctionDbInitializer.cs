using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using WebAuction.DataAccessLayer.EntityFramework.Entities;

namespace WebAuction.DataAccessLayer.EntityFramework
{
    public class WebAuctionDbInitializer : DropCreateDatabaseAlways<WebAuctionDbContext> //alebo nechavat data?
    {
        protected override void Seed(WebAuctionDbContext context)
        {
            var adam = new Admin
            {
                Id = Guid.Parse("66e82542-33e6-4cc1-b40b-61f7845a406b"),
                Name = "Adam",
                Login = "adam",
                Password = "AdamJePan"
            };
            var stevo = new Admin
            {
                Id = Guid.Parse("d5b64671-5687-4b44-817b-6a5ce0a5521a"),
                Name = "Stevo",
                Login = "stevo",
                Password = "StevoJePan"
            };
            context.Admins.AddOrUpdate(admin => admin.Id, adam, stevo);

            var electro = new Category
            {
                Id = Guid.Parse("399adde5-a968-4aaf-900e-247936416d23"),
                Name = "Electronics",
                Description = "Smartphones, laptops and other electronic devices."
            };
            var jewelry = new Category
            {
                Id = Guid.Parse("4a5aa07f-e2a0-4078-bab1-ac7c6287a3fe"),
                Name = "Jewelry",
                Description = "Rings, necklaces...all shiny stuff."
            };
            var art = new Category
            {
                Id = Guid.Parse("c704dd48-c53e-4a47-90e6-d025fb5b27cf"),
                Name = "Art",
                Description = "Paintings, statues and more strange things."
            };
            context.Categories.AddOrUpdate(category => category.Id, art, electro, jewelry);

            var jonSnow = new User
            {
                Id = Guid.Parse("65a9dcb3-d290-4876-b284-47c8dbc7cfc5"),
                Name = "Jon Snow",
                Address = "Winterfell",
                Email = "youknow@nothing.ws",
                Contact = "4527453652",
                Login = "lordCommander",
                Password = "bastard75684"
            };
            var waltWhite = new User
            {
                Id = Guid.Parse("23ba41d2-2f82-430d-87b3-9e2e145be7c7"),
                Name = "Walter White",
                Address = "Albaquerque",
                Email = "heisenberg@bluestuff.com",
                Contact = "7524943851",
                Login = "methBoss737",
                Password = "lungCancerSucks"
            };
            var barryAllen = new User
            {
                Id = Guid.Parse("5d1114de-4ede-4550-b6ae-a47923a8169f"),
                Name = "Barry Allen",
                Address = "Central City",
                Email = "flash@starlabs.com",
                Contact = "9763425842",
                Login = "redStreak77",
                Password = "runBarryRun"
            };
            context.Users.AddOrUpdate(user => user.Id, jonSnow, waltWhite, barryAllen);

            var longclaw = new Item
            {
                Id = Guid.Parse("c5ec7468-1d1f-4171-9f64-80dceec966b3"),
                Name = "Longclaw",
                Sellerid = jonSnow.Id,
                Seller = jonSnow,
                CategoryId = art.Id,
                Category = art,
                Description = "Valyrian steal sword"
            };
            var melsNeck = new Item
            {
                Id = Guid.Parse("e23c53e9-c7a0-44f4-a049-f62e195c9fdd"),
                Name = "Melisandre's necklace",
                Sellerid = jonSnow.Id,
                Seller = jonSnow,
                CategoryId = jewelry.Id,
                Category = jewelry,
                Description = "She forgot it near the bath and some strange veeery old lady wanted to stole it."
            };
            var blueStuff = new Item
            {
                Id = Guid.Parse("8767002b-449e-4dc3-b79d-fbf652b5c9bb"),
                Name = "Blue crystal",
                Sellerid = waltWhite.Id,
                Seller = waltWhite,
                CategoryId = art.Id,
                Category = art,
                Description = "Best crystal you ever try. Pure. Blue. Great."
            };
            var oldSuit = new Item
            {
                Id = Guid.Parse("88170f86-dec7-467b-91f1-1706c56a971a"),
                Name = "Flash's old suit",
                Sellerid = barryAllen.Id,
                Seller = barryAllen,
                CategoryId = art.Id,
                Category = art,
                Description = "Little bit older but preserved, red, no decorations, friction tolerance."
            };
            var tachyon = new Item
            {
                Id = Guid.Parse("d1d34e7b-b390-42e2-9b46-c661c65f8313"),
                Name = "Tachyon prototype",
                Sellerid = barryAllen.Id,
                Seller = barryAllen,
                CategoryId = electro.Id,
                Category = electro,
                Description = "Wearing it makes you run faster."
            };
            context.Items.AddOrUpdate(item => item.Id, longclaw, melsNeck, blueStuff, oldSuit, tachyon);

            var longclawAuction = new Auction
            {
                Id = Guid.Parse("0558b662-2aa8-438e-928c-571adb8b241b"),
                SellerId = jonSnow.Id,
                Seller = jonSnow,
                ItemId = longclaw.Id,
                Item = longclaw,
                StratingPrice = 100000,
                MinimalBid = 1000,
                BuyoutPrice = 10000000,
                StartTime = DateTime.Now,  //ake datumy?
                EndTime = DateTime.Now.AddDays(2),
                HandoverOptions = "Come and take it.(it is obviously too heavy for raven, don't you think?)",
                Sold = false,
                DisplayCount = 987
            };
            var melsNeckAuction = new Auction
            {
                Id = Guid.Parse("08f38899-9eb6-48e3-b177-2cb6907b58c1"),
                SellerId = jonSnow.Id,
                Seller = jonSnow,
                ItemId = melsNeck.Id,
                Item = melsNeck,
                StratingPrice = 1000,
                MinimalBid = 50,
                BuyoutPrice = 12500,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(6),
                HandoverOptions = "Can be delivered by raven.",
                Sold = false,
                DisplayCount = 354
            };
            var blueStuffAuction = new Auction
            {
                Id = Guid.Parse("92d7feb4-13e8-431d-9ca7-589e55d721ed"),
                SellerId = waltWhite.Id,
                Seller = waltWhite,
                ItemId = blueStuff.Id,
                Item = blueStuff,
                StratingPrice = 300000,
                MinimalBid = 1000,
                BuyoutPrice = 3000000,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
                HandoverOptions = "Definitely not by postal service.",
                Sold = false,
                DisplayCount = 412
            };
            var oldSuitAuction = new Auction
            {
                Id = Guid.Parse("d481c797-0e21-49a7-9672-6ec9a2b08c87"),
                SellerId = barryAllen.Id,
                Seller = barryAllen,
                ItemId = oldSuit.Id,
                Item = oldSuit,
                StratingPrice = 2000,
                MinimalBid = 100,
                BuyoutPrice = 700000,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(3),
                HandoverOptions = "Delivery in a blink of eye.",
                Sold = false,
                DisplayCount = 1572
            };
            var tachyonAuction = new Auction
            {
                Id = Guid.Parse("c7c8f702-5872-4200-bb78-269ae87336a7"),
                SellerId = barryAllen.Id,
                Seller = barryAllen,
                ItemId = tachyon.Id,
                Item = tachyon,
                StratingPrice = 100000,
                MinimalBid = 1000,
                BuyoutPrice = 550000,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(2),
                HandoverOptions = "Run for it.",
                Sold = false,
                DisplayCount = 529
            };
            context.Auctions.AddOrUpdate(auction => auction.Id, longclawAuction, melsNeckAuction, blueStuffAuction, oldSuitAuction, tachyonAuction);

            //bids

            //payments

            //ratings

            //comments

            context.SaveChanges();
            base.Seed(context);
        }
    }
}