using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAuction.DataAccessLayer.EntityFramework.Validation;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Bid
    {
        public string TableName { get; set; } = nameof(WebAuctionDbContext.Bids);

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Auction))]
        public Guid AuctionId { get; set; }

        public virtual Auction Auction { get; set; }

        [ForeignKey(nameof(Buyer))]
        public Guid BuyerId { get; set; }

        public virtual User Buyer { get; set; }

        public DateTime Time { get; set; }

        [PositiveDecimal]
        public decimal BidAmount { get; set; }

        [PositiveDecimal]
        public decimal NewItemPrice { get; set; }
    }
}
