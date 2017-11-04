using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAuction.DataAccessLayer.EntityFramework.Validation;
using WebAuction.Infrastructure;

namespace WebAuction.DataAccessLayer.EntityFramework.Entities
{
    public class Payment : IEntity
    {
        public string TableName { get; set; } = nameof(WebAuctionDbContext.Payments);

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Buyer))]
        public Guid BuyerId { get; set; }

        public virtual User Buyer { get; set; }

        [ForeignKey(nameof(Auction))]
        public Guid AuctionId { get; set; }

        public virtual Auction Auction { get; set; }

        [PositiveDecimal]
        public decimal Amount { get; set; }

        public DateTime Time { get; set; }

        [CreditCard]
        public string CreditCardNumber { get; set; }
    }
}
