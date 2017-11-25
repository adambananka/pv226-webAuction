using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using WebAuction.DataAccessLayer.EntityFramework.Entities;

namespace WebAuction.DataAccessLayer.EntityFramework.Initializers
{
    public class WebAuctionDbInitializer : DropCreateDatabaseAlways<WebAuctionDbContext>
    {
        private TimeSpan GetRandomDayTime()
        {
            var rnd = new Random();

            return new TimeSpan(rnd.Next(6, 22), rnd.Next(0, 59), rnd.Next(0, 59));
        }

        protected override void Seed(WebAuctionDbContext context)
        {
            #region Categories

            var electro = new Category
            {
                Id = Guid.Parse("2dab2905-a1de-42ca-ae58-0083fa9f0d7f"),
                Name = "Electronics",
                Description = "Smartphones, laptops and other electronic devices.",
                ParentId = null,
                Parent = null
            };
            var jewelry = new Category
            {
                Id = Guid.Parse("ca876008-7405-46b4-b491-8b275b349998"),
                Name = "Jewelry",
                Description = "Rings, necklaces...all shiny stuff.",
                ParentId = null,
                Parent = null
            };
            var art = new Category
            {
                Id = Guid.Parse("66d1e631-e12d-45a8-87ee-631965a1d6fb"),
                Name = "Art",
                Description = "Paintings, statues and more strange things.",
                ParentId = null,
                Parent = null
            };
            context.Categories.AddOrUpdate(category => category.Id, art, electro, jewelry);

            #endregion

            #region Users

            var admin = new User
            {
                Id = Guid.Parse("66e82542-33e6-4cc1-b40b-61f7845a406b"),
                Name = "I am the admin",
                Login = "admin",
                PasswordHash = "Password1",
                PasswordSal = "Pa$$w0rd1"
            };

            var jonSnow = new User
            {
                Id = Guid.Parse("65a9dcb3-d290-4876-b284-47c8dbc7cfc5"),
                Name = "Jon Snow",
                Address = "Winterfell",
                Email = "youknow@nothing.ws",
                Phone = "4527453652",
                Login = "lordCommander",
                PasswordHash = "bastard",
                PasswordSal = "123tard"
            };
            var waltWhite = new User
            {
                Id = Guid.Parse("23ba41d2-2f82-430d-87b3-9e2e145be7c7"),
                Name = "Walter White",
                Address = "Albaquerque",
                Email = "heisenberg@bluestuff.com",
                Phone = "7524943851",
                Login = "methBoss737",
                PasswordHash = "lungCancerSucks",
                PasswordSal = "lungCancer12345"
            };
            var barryAllen = new User
            {
                Id = Guid.Parse("5d1114de-4ede-4550-b6ae-a47923a8169f"),
                Name = "Barry Allen",
                Address = "Central City",
                Email = "flash@starlabs.com",
                Phone = "9763425842",
                Login = "redStreak77",
                PasswordHash = "runBarryRun",
                PasswordSal = "runBarry123"
            };
            context.Users.AddOrUpdate(user => user.Id, admin, jonSnow, waltWhite, barryAllen);

            #endregion

            #region Auctions

            var longclawStartTime = DateTime.Now.AddDays(-5).Date + GetRandomDayTime();
            var longclawAuction = new Auction
            {
                Id = Guid.Parse("0558b662-2aa8-438e-928c-571adb8b241b"),
                Name = "Longclaw - best auction ever",
                Description = "A big sword",
                PhotoUri = @"\Content\Images\Auctions\sword.jpg",
                CategoryId = art.Id,
                Category = art,
                SellerId = jonSnow.Id,
                Seller = jonSnow,
                StartingPrice = 1000,
                ActualPrice = 1000,
                MinimalBid = 100,
                BuyoutPrice = 100000,
                StartTime = DateTime.Now.AddDays(-5).Date + GetRandomDayTime(),
                EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(15, 0, 0),
                SellTime = null,
                HandoverOptions = "Come and take it.(it is obviously too heavy for raven, don't you think?)",
                Ended = false,
                DisplayCount = 987,
            };
            var melsNeckAuction = new Auction
            {
                Id = Guid.Parse("08f38899-9eb6-48e3-b177-2cb6907b58c1"),
                Name = "Melisandre's necklace",
                Description = "She forgot it near the bath and some strange veeery old lady wanted to stole it.",
                CategoryId = jewelry.Id,
                Category = jewelry,
                SellerId = jonSnow.Id,
                Seller = jonSnow,
                StartingPrice = 1000,
                ActualPrice = 1000,
                MinimalBid = 50,
                BuyoutPrice = 12500,
                StartTime = DateTime.Now.AddDays(-1).Date + GetRandomDayTime(),
                EndTime = DateTime.Now.AddDays(6).Date + new TimeSpan(12, 0, 0),
                SellTime = null,
                HandoverOptions = "Can be delivered by raven.",
                Ended = false,
                DisplayCount = 354
            };
            var blueStuffAuction = new Auction
            {
                Id = Guid.Parse("92d7feb4-13e8-431d-9ca7-589e55d721ed"),
                Name = "Blue crystal",
                Description = "Best crystal you ever try. Pure. Blue. Great.",
                CategoryId = art.Id,
                Category = art,
                SellerId = waltWhite.Id,
                Seller = waltWhite,
                StartingPrice = 3000,
                ActualPrice = 3500,
                MinimalBid = 10,
                BuyoutPrice = 30000,
                StartTime = DateTime.Now.AddDays(-2).Date + GetRandomDayTime(),
                EndTime = DateTime.Now.AddDays(5).Date + new TimeSpan(18, 0, 0),
                SellTime = null,
                HandoverOptions = "Definitely not by postal service.",
                Ended = false,
                DisplayCount = 412
            };
            var oldSuitAuction = new Auction
            {
                Id = Guid.Parse("d481c797-0e21-49a7-9672-6ec9a2b08c87"),
                Name = "Flash's old suit",
                Description = "Little bit older but preserved, red, no decorations, friction tolerance.",
                CategoryId = art.Id,
                Category = art,
                SellerId = barryAllen.Id,
                Seller = barryAllen,
                StartingPrice = 2000,
                ActualPrice = 2000,
                MinimalBid = 100,
                BuyoutPrice = 700000,
                StartTime = DateTime.Now.AddDays(-3).Date + GetRandomDayTime(),
                EndTime = DateTime.Now.AddDays(1).Date + new TimeSpan(16, 0, 0),
                SellTime = null,
                HandoverOptions = "Delivery in a blink of eye.",
                Ended = false,
                DisplayCount = 1572
            };
            var tachyonSellTime = DateTime.Now.AddDays(-7) + GetRandomDayTime();
            var tachyonAuction = new Auction
            {
                Id = Guid.Parse("c7c8f702-5872-4200-bb78-269ae87336a7"),
                Name = "Tachyon prototype",
                Description = "Wearing it makes you run faster.",
                CategoryId = electro.Id,
                Category = electro,
                SellerId = barryAllen.Id,
                Seller = barryAllen,
                StartingPrice = 1000,
                ActualPrice = 1000,
                MinimalBid = 30,
                BuyoutPrice = 55000,
                StartTime = DateTime.Now.AddDays(-8).Date + GetRandomDayTime(),
                EndTime = DateTime.Now.AddDays(-5).Date + new TimeSpan(15, 0, 0),
                SellTime = tachyonSellTime,
                HandoverOptions = "Run for it.",
                Ended = true,
                DisplayCount = 529
            };
            context.Auctions.AddOrUpdate(auction => auction.Id, longclawAuction, melsNeckAuction, blueStuffAuction,
                oldSuitAuction, tachyonAuction);

            #endregion

            #region Bids

            var bid0 = new Bid
            {
                Id = Guid.Parse("0e3ea918-360b-40c8-89d6-45bc718cf340"),
                Time = blueStuffAuction.StartTime.AddHours(15),
                AuctionId = blueStuffAuction.Id,
                Auction = blueStuffAuction,
                BuyerId = barryAllen.Id,
                Buyer = barryAllen,
                BidAmount = blueStuffAuction.MinimalBid,
                NewItemPrice = blueStuffAuction.ActualPrice + blueStuffAuction.MinimalBid
            };
            var bid1 = new Bid
            {
                Id = Guid.Parse("25ed6c98-4bd9-4a68-b428-77be652c385f"),
                Time = tachyonSellTime,
                AuctionId = tachyonAuction.Id,
                Auction = tachyonAuction,
                BuyerId = jonSnow.Id,
                Buyer = jonSnow,
                BidAmount = tachyonAuction.BuyoutPrice - tachyonAuction.StartingPrice,
                NewItemPrice = tachyonAuction.BuyoutPrice
            };
            context.Bids.AddOrUpdate(bid => bid.Id, bid0, bid1);

            #endregion

            #region Payments

            var payment1 = new Payment
            {
                Id = Guid.NewGuid(),
                Buyer = jonSnow,
                BuyerId = jonSnow.Id,
                AuctionId = tachyonAuction.Id,
                Auction = tachyonAuction,
                Amount = tachyonAuction.BuyoutPrice,
                Time = tachyonSellTime,
                CreditCardNumber = "4539943109248883" // visa
            };
            context.Payments.AddOrUpdate(payment => payment.Id, payment1);

            #endregion

            #region Ratings

            var rating1 = new Rating
            {
                Id = Guid.Parse("a22782c5-060e-4330-9381-68db4a7a5d65"),
                Feedback = "Proper seller, bro",
                Seller = barryAllen,
                SellerId = barryAllen.Id,
                Stars = 4,
                Time = tachyonSellTime.AddMinutes(15)
            };
            context.Ratings.AddOrUpdate(rating => rating.Id, rating1);

            #endregion

            #region Comments

            var rootComment1 = new Comment
            {
                Id = Guid.Parse("d7fe4d2f-afb8-48b5-b521-45fdae5a1c01"),
                Time = longclawStartTime.AddHours(2),
                Auction = longclawAuction,
                AuctionId = longclawAuction.Id,
                Text = "How many white walkers have you killed with this claw?",
                User = waltWhite,
                UserId = waltWhite.Id
            };
            var childComment1 = new Comment
            {
                Id = Guid.Parse("c87244d1-c2f7-4ad2-bd4a-560702fec4ba"),
                Time = longclawStartTime.AddHours(3),
                Auction = longclawAuction,
                AuctionId = longclawAuction.Id,
                Text = "Too many to count",
                User = jonSnow,
                UserId = jonSnow.Id
            };
            context.Comments.AddOrUpdate(comment => comment.Id, rootComment1, childComment1);

            #endregion

            context.SaveChanges();
            base.Seed(context);
        }
    }
}