using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using WebAuction.DataAccessLayer.EntityFramework.Entities;

namespace WebAuction.DataAccessLayer.EntityFramework.Initializers
{
    public class WebAuctionDbInitializer : DropCreateDatabaseIfModelChanges<WebAuctionDbContext>
    {
        private TimeSpan GetRandomDayTime()
        {
            var rnd = new Random();

            return new TimeSpan(rnd.Next(6, 22), rnd.Next(0, 59), rnd.Next(0, 59));
        }

        protected override void Seed(WebAuctionDbContext context)
        {
            #region Admins

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

            #endregion

            #region Categories

            var electro = new Category
            {
                Id = 1,
                Name = "Electronics",
                Description = "Smartphones, laptops and other electronic devices."
            };
            var jewelry = new Category
            {
                Id = 2,
                Name = "Jewelry",
                Description = "Rings, necklaces...all shiny stuff."
            };
            var art = new Category
            {
                Id = 3,
                Name = "Art",
                Description = "Paintings, statues and more strange things."
            };
            context.Categories.AddOrUpdate(category => category.Id, art, electro, jewelry);

            #endregion

            #region Users

            var jonSnow = new User
            {
                Id = Guid.Parse("65a9dcb3-d290-4876-b284-47c8dbc7cfc5"),
                FirstName = "Jon Snow",
                Address = "Winterfell",
                Email = "youknow@nothing.ws",
                Phone = "4527453652",
                Login = "lordCommander",
                Password = "bastard75684"
            };
            var waltWhite = new User
            {
                Id = Guid.Parse("23ba41d2-2f82-430d-87b3-9e2e145be7c7"),
                FirstName = "Walter White",
                Address = "Albaquerque",
                Email = "heisenberg@bluestuff.com",
                Phone = "7524943851",
                Login = "methBoss737",
                Password = "lungCancerSucks"
            };
            var barryAllen = new User
            {
                Id = Guid.Parse("5d1114de-4ede-4550-b6ae-a47923a8169f"),
                FirstName = "Barry Allen",
                Address = "Central City",
                Email = "flash@starlabs.com",
                Phone = "9763425842",
                Login = "redStreak77",
                Password = "runBarryRun"
            };
            context.Users.AddOrUpdate(user => user.Id, jonSnow, waltWhite, barryAllen);

            #endregion

            #region Items

            var longclaw = new Item
            {
                Id = Guid.Parse("c5ec7468-1d1f-4171-9f64-80dceec966b3"),
                Name = "Longclaw",
                CategoryId = art.Id,
                Category = art,
                Description = "Valyrian steal sword"
            };
            var melsNeck = new Item
            {
                Id = Guid.Parse("e23c53e9-c7a0-44f4-a049-f62e195c9fdd"),
                Name = "Melisandre's necklace",
                CategoryId = jewelry.Id,
                Category = jewelry,
                Description = "She forgot it near the bath and some strange veeery old lady wanted to stole it."
            };
            var blueStuff = new Item
            {
                Id = Guid.Parse("8767002b-449e-4dc3-b79d-fbf652b5c9bb"),
                Name = "Blue crystal",
                CategoryId = art.Id,
                Category = art,
                Description = "Best crystal you ever try. Pure. Blue. Great."
            };
            var oldSuit = new Item
            {
                Id = Guid.Parse("88170f86-dec7-467b-91f1-1706c56a971a"),
                Name = "Flash's old suit",
                CategoryId = art.Id,
                Category = art,
                Description = "Little bit older but preserved, red, no decorations, friction tolerance."
            };
            var tachyon = new Item
            {
                Id = Guid.Parse("d1d34e7b-b390-42e2-9b46-c661c65f8313"),
                Name = "Tachyon prototype",
                CategoryId = electro.Id,
                Category = electro,
                Description = "Wearing it makes you run faster."
            };
            context.Items.AddOrUpdate(item => item.Id, longclaw, melsNeck, blueStuff, oldSuit, tachyon);

            #endregion

            #region Auctions

            var longClawStartTime = DateTime.Now.AddDays(-5).Date + GetRandomDayTime();
            var longclawAuction = new Auction
            {
                Id = Guid.Parse("0558b662-2aa8-438e-928c-571adb8b241b"),
                SellerId = jonSnow.Id,
                Seller = jonSnow,
                ItemId = longclaw.Id,
                Item = longclaw,
                StartingPrice = 1000,
                ActualPrice = 1000,
                MinimalBid = 100,
                BuyoutPrice = 100000,
                Bids = null,
                StartTime = DateTime.Now.AddDays(-5).Date + GetRandomDayTime(),
                EndTime = DateTime.Now.AddDays(2).Date + new TimeSpan(15, 0, 0),
                SellTime = null,
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
                StartingPrice = 1000,
                ActualPrice = 1000,
                MinimalBid = 50,
                BuyoutPrice = 12500,
                Bids = null,
                StartTime = DateTime.Now.AddDays(-1).Date + GetRandomDayTime(),
                EndTime = DateTime.Now.AddDays(6).Date + new TimeSpan(12, 0, 0),
                SellTime = null,
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
                StartingPrice = 3000,
                ActualPrice = 3500,
                MinimalBid = 10,
                BuyoutPrice = 30000,
                Bids = null,
                StartTime = DateTime.Now.AddDays(-2).Date + GetRandomDayTime(),
                EndTime = DateTime.Now.AddDays(5).Date + new TimeSpan(18, 0, 0),
                SellTime = null,
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
                StartingPrice = 2000,
                ActualPrice = 2000,
                MinimalBid = 100,
                BuyoutPrice = 700000,
                Bids = null,
                StartTime = DateTime.Now.AddDays(-3).Date + GetRandomDayTime(),
                EndTime = DateTime.Now.AddDays(1).Date + new TimeSpan(16, 0, 0),
                SellTime = null,
                HandoverOptions = "Delivery in a blink of eye.",
                Sold = false,
                DisplayCount = 1572
            };
            var tachyonSellTime = DateTime.Now.AddDays(-7) + GetRandomDayTime();
            var tachyonAuction = new Auction
            {
                Id = Guid.Parse("c7c8f702-5872-4200-bb78-269ae87336a7"),
                SellerId = barryAllen.Id,
                Seller = barryAllen,
                ItemId = tachyon.Id,
                Item = tachyon,
                StartingPrice = 1000,
                ActualPrice = 1000,
                MinimalBid = 30,
                BuyoutPrice = 55000,
                Bids = null,
                StartTime = DateTime.Now.AddDays(-8).Date + GetRandomDayTime(),
                EndTime = DateTime.Now.AddDays(-5).Date + new TimeSpan(15, 0, 0),
                SellTime = tachyonSellTime,
                HandoverOptions = "Run for it.",
                Sold = true,
                DisplayCount = 529
            };
            context.Auctions.AddOrUpdate(auction => auction.Id, longclawAuction, melsNeckAuction, blueStuffAuction,
                oldSuitAuction, tachyonAuction);

            #endregion

            #region Bids

            var bid1 = new Bid
            {
                Id = Guid.NewGuid(),
                Time = blueStuffAuction.StartTime.AddHours(15),
                BuyerId = barryAllen.Id,
                Buyer = barryAllen,
                BidAmount = blueStuffAuction.MinimalBid,
                NewItemPrice = blueStuffAuction.ActualPrice + blueStuffAuction.MinimalBid
            };
            context.Bids.AddOrUpdate(bid => bid.Id, bid1);
            var a = context.Auctions.Find(blueStuffAuction.Id);
            a?.Bids.Add(bid1);

            #endregion

            #region Payments

            var payment1 = new Payment
            {
                Id = Guid.NewGuid(),
                Time = tachyonSellTime,
                AuctionId = tachyonAuction.Id,
                Auction = tachyonAuction,
                Amount = tachyonAuction.BuyoutPrice,
                Buyer = jonSnow,
                BuyerId = jonSnow.Id,
                CreditCardNumber = 123465789
            };
            context.Payments.AddOrUpdate(payment => payment.Id, payment1);

            #endregion

            #region Ratings

            var rating1 = new Rating
            {
                Author = jonSnow,
                AuthorId = jonSnow.Id,
                Id = 1,
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
                Id = 1,
                Time = longClawStartTime.AddHours(2),
                Auction = longclawAuction,
                AuctionId = longclawAuction.Id,
                ParentComment = null,
                ParentCommentId = null,
                Text = "How many white walkers have you killed with this claw?",
                User = waltWhite,
                UserId = waltWhite.Id
            };
            var childComment1 = new Comment
            {
                Id = 2,
                Time = longClawStartTime.AddHours(3),
                Auction = longclawAuction,
                AuctionId = longclawAuction.Id,
                ParentComment = rootComment1,
                ParentCommentId = rootComment1.Id,
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