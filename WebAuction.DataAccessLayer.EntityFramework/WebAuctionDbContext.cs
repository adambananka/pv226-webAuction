using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebAuction.DataAccessLayer.EntityFramework.Config;
using WebAuction.DataAccessLayer.EntityFramework.Entities;
using WebAuction.DataAccessLayer.EntityFramework.Initializers;

namespace WebAuction.DataAccessLayer.EntityFramework
{
    public class WebAuctionDbContext : DbContext
    {
        public WebAuctionDbContext() : base(EntityFrameworkInstaller.ConnectionSting)
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer(new WebAuctionDbInitializer());
        }

        /// <summary>
        /// Ctor with db connection, required by data access layer tests
        /// </summary>
        /// <param name="connection">The database connection</param>
        public WebAuctionDbContext(DbConnection connection) : base(connection, true)
        {
            Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}