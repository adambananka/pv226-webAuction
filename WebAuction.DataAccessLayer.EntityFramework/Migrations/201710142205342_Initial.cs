namespace WebAuction.DataAccessLayer.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Auctions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SellerId = c.Guid(nullable: false),
                        ItemId = c.Guid(nullable: false),
                        StartingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActualPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinimalBid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyoutPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        SellTime = c.DateTime(),
                        HandoverOptions = c.String(),
                        Sold = c.Boolean(nullable: false),
                        DisplayCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SellerId, cascadeDelete: true)
                .Index(t => t.SellerId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        CategoryId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Address = c.String(maxLength: 1024),
                        Email = c.String(),
                        Phone = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AuctionId = c.Guid(nullable: false),
                        BuyerId = c.Guid(nullable: false),
                        Time = c.DateTime(nullable: false),
                        BidAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NewItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auctions", t => t.AuctionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.BuyerId, cascadeDelete: true)
                .Index(t => t.AuctionId)
                .Index(t => t.BuyerId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AuctionId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ParentCommentId = c.Int(),
                        Time = c.DateTime(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auctions", t => t.AuctionId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.AuctionId)
                .Index(t => t.UserId)
                .Index(t => t.ParentCommentId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BuyerId = c.Guid(nullable: false),
                        AuctionId = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Time = c.DateTime(nullable: false),
                        CreditCardNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auctions", t => t.AuctionId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.BuyerId, cascadeDelete: true)
                .Index(t => t.BuyerId)
                .Index(t => t.AuctionId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SellerId = c.Guid(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Stars = c.Int(nullable: false),
                        Feedback = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SellerId, cascadeDelete: true)
                .Index(t => t.SellerId)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "SellerId", "dbo.Users");
            DropForeignKey("dbo.Ratings", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Payments", "BuyerId", "dbo.Users");
            DropForeignKey("dbo.Payments", "AuctionId", "dbo.Auctions");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "AuctionId", "dbo.Auctions");
            DropForeignKey("dbo.Bids", "BuyerId", "dbo.Users");
            DropForeignKey("dbo.Bids", "AuctionId", "dbo.Auctions");
            DropForeignKey("dbo.Auctions", "SellerId", "dbo.Users");
            DropForeignKey("dbo.Auctions", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Ratings", new[] { "AuthorId" });
            DropIndex("dbo.Ratings", new[] { "SellerId" });
            DropIndex("dbo.Payments", new[] { "AuctionId" });
            DropIndex("dbo.Payments", new[] { "BuyerId" });
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "AuctionId" });
            DropIndex("dbo.Bids", new[] { "BuyerId" });
            DropIndex("dbo.Bids", new[] { "AuctionId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropIndex("dbo.Auctions", new[] { "ItemId" });
            DropIndex("dbo.Auctions", new[] { "SellerId" });
            DropTable("dbo.Ratings");
            DropTable("dbo.Payments");
            DropTable("dbo.Comments");
            DropTable("dbo.Bids");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
            DropTable("dbo.Items");
            DropTable("dbo.Auctions");
            DropTable("dbo.Admins");
        }
    }
}
