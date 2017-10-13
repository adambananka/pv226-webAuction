using System.Data.Entity;
using WebAuction.DataAccessLayer.EntityFramework.Entities;

namespace WebAuction.DataAccessLayer.EntityFramework
{
    public class WebAuctionDbContext : DbContext
    {
        private const string ConnectionString = ""; //sem nieco

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }

        public WebAuctionDbContext() : base(ConnectionString)
        {
            Database.SetInitializer(new WebAuctionDbInitializer());
        }
    }
}
