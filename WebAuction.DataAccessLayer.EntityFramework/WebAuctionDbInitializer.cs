using System;
using System.Data.Entity;
using WebAuction.DataAccessLayer.EntityFramework.Entities;

namespace WebAuction.DataAccessLayer.EntityFramework
{
    public class WebAuctionDbInitializer : DropCreateDatabaseAlways<WebAuctionDbContext> //alebo nechavat data?
    {
        protected override void Seed(WebAuctionDbContext context)
        {
            context.Admins.Add(new Admin
            {
                Id = Guid.NewGuid(),
                Name = "Adam",
                Login = "adam",
                Password = "AdamJePan"
            });
            context.Admins.Add(new Admin
            {
                Id = Guid.NewGuid(),
                Name = "Stevo",
                Login = "stevo",
                Password = "StevoJePan"
            });

            

            context.SaveChanges();
            base.Seed(context);
        }
    }
}