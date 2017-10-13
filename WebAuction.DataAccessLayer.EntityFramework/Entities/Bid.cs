using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Bid
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Auction))]
        public Guid AuctionId { get; set; }

        public virtual Auction Auction { get; set; }

        [ForeignKey(nameof(Buyer))]
        public Guid BuyerId { get; set; }

        public virtual User Buyer { get; set; }

        public DateTime Time { get; set; }

        [Range(0, double.MaxValue)]
        public double BidAmount { get; set; }

        [Range(0, double.MaxValue)]
        public double NewItemPrice { get; set; }
    }
}
